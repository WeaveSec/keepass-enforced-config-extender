/*
  KeePass Enforced Config Extender - A KeePass plugin that extends the Enforced Configuration feature
  Copyright (C) WeaveSec <weavesec@gmail.com>
*/

using KeePass.Forms;
using KeePass.Plugins;

using KeePassLib;
using KeePassLib.Cryptography.KeyDerivation;
using KeePassLib.Utility;
using KeePass.UI;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
// using KeePassLib.Serialization;

namespace KeePassEnforcedConfigExtender
{
	public sealed class KeePassEnforcedConfigExtenderExt : Plugin
	{
		// The plugin remembers its host in this variable
		private IPluginHost m_host = null;

		public override bool Initialize(IPluginHost host)
		{
			if(host == null) return false; // Fail; we need the host
			m_host = host;

			// We want a notification when the user tried to save
			// the current database
			m_host.MainWindow.FileSaved += this.OnFileSaved;

            // Get notification of information when file is created
            m_host.MainWindow.FileSaving += this.OnFileSaving;

            return true; // Initialization successful
		}

		/// <summary>
		/// The <c>Terminate</c> method is called by KeePass when
		/// you should free all resources, close files/streams,
		/// remove event handlers, etc.
		/// </summary>
		public override void Terminate()
		{
			// Remove event handler (important!)
			m_host.MainWindow.FileSaved -= this.OnFileSaved;
            m_host.MainWindow.FileSaving -= this.OnFileSaving;
        }

		private void OnFileSaving(object sender, FileSavingEventArgs e)
		{
            
            PwDatabase sourceDb = e.Database;

			bool standardMet = VerifyStandard(sourceDb);


            // If KDF standard not met, prompts user response
            if (standardMet == false)
            {

                // Ask user to continue saving or to cancel
                var warn_form = new Warn_Window();

                warn_form.ShowDialog();
                warn_form.Dispose();

                bool ContinueSave = warn_form.ContinueSave;

                MessageService.ShowInfo("continueSave: " + ContinueSave);

                // If user wants to save, apply minimum Kdf parameters
                if (ContinueSave == true)
                {
                    sourceDb.KdfParameters = (new Argon2Kdf()).GetDefaultParameters();

                    sourceDb.KdfParameters.SetUInt64(Argon2Kdf.ParamIterations, 15);
                    sourceDb.KdfParameters.SetUInt64(Argon2Kdf.ParamMemory, 20 * 1024 * 1024);
                    sourceDb.KdfParameters.SetUInt32(Argon2Kdf.ParamParallelism, 1);
                }
                // If user does not want to save, set Cancel signal
                else
                {
                    e.Cancel = true;
                }
            }
        }

		private bool VerifyStandard(PwDatabase sourceDb)
		{
            bool metStandard = false;
            bool Cancel;

            // Argon2 variable names
            string ArgonParamParallelism = "P"; // UInt32
            string ArgonParamMemory = "M"; // UInt64
            string ArgonParamIterations = "I";
            string AesParamRounds = "R"; // Ulong

            // Argon2 default parameters
            ulong DefaultIterations = 2;
            ulong DefaultMemory = 64 * 1024 * 1024; // 64 MB
            uint DefaultParallelism = 2;

            // AES default parameters
            ulong DefaultKeyEncryptionRounds = 60000;



            KdfEngine kdf = KdfPool.Get(sourceDb.KdfParameters.KdfUuid);

            /**
             * List Kdf options in Pool
            foreach (KdfEngine kdf in KdfPool.Engines)
            {
                MessageService.ShowInfo("kdfName: " + "\n" + kdf_this.Name);
            };
			*/


            // Get Database KDF values
            ulong AesRounds = sourceDb.KdfParameters.GetUInt64(AesParamRounds, DefaultKeyEncryptionRounds);

            ulong Argon2Iterations = sourceDb.KdfParameters.GetUInt64(ArgonParamIterations, DefaultIterations);
            ulong Argon2Memory = sourceDb.KdfParameters.GetUInt64(ArgonParamMemory, DefaultMemory);
            uint Argon2Parallelism = sourceDb.KdfParameters.GetUInt32(ArgonParamParallelism, DefaultParallelism);

            // Compare if Database Kdf Settings meets minimum standard
            if (kdf is Argon2Kdf && Argon2Iterations >= 15 && Argon2Memory >= 20971520 && Argon2Parallelism >= 1)
            {
                metStandard = true;
                Cancel = false;
            }
            else
            {
                metStandard = false;
                Cancel = true;
            }

            MessageService.ShowInfo("Show info of file BEFORE saving: " + "\n" +
                "Cipher: " + sourceDb.KdfParameters.Count + "\n" +
                "KdfUuid: " + sourceDb.KdfParameters.KdfUuid + "\n" +
                "KdfType: " + kdf.GetType() + "\n\n" +
                "---Argon Info---" + "\n" +
                "ArgonIterationsNum: " + Argon2Iterations + "\n" +
                "ArgonMemoryNum: " + Argon2Memory + "\n" +
                "ArgonParallelismNum: " + Argon2Parallelism + "\n\n" +
                "---AES Info---" + "\n" +
                "AesRoundsNum: " + AesRounds + "\n\n" +
                "Standard Met?: " + metStandard + "\n" +
                "Cancel?: " + Cancel + "\n"
                ) ;

            return metStandard;
		}

		private void OnFileSaved(object sender, FileSavedEventArgs e)
		{


            PwDatabase sourceDb = e.Database;

            MessageService.ShowInfo("SAVED");

            /**

            MessageService.ShowInfo("Show info of file BEFORE: " + "\n" +
			    "Cipher: " + e.Database.KdfParameters.Count + "\n" +
			    "Test: " + e.Database.KdfParameters + "\n" +
			    "KdfUuid: " + e.Database.KdfParameters.KdfUuid + "\n");
			
            sourceDb.KdfParameters = (new Argon2Kdf()).GetDefaultParameters();
			
			//KdfParameters lego = KdfParameters.DeserializeExt(KdfParameters.SerializeExt(sourceDb.KdfParameters));

			//int testint = sourceDb.KdfParameters.GetUInt64(e.Database.KdfParameters.GetInt64(KdfParameters.DeserializeExt(Keys));
			//sourceDb.KdfParameters.SetUInt64(AesKdf.ParamRounds, 620000);
			
			
			sourceDb.KdfParameters.SetUInt64(Argon2Kdf.ParamIterations, 15);
            sourceDb.KdfParameters.SetUInt64(Argon2Kdf.ParamMemory, 20 * 1024 * 1024);
            sourceDb.KdfParameters.SetUInt32(Argon2Kdf.ParamParallelism, 1);

            //XmlReader test = new XmlReader();

            //XmlNodeList node = test.ReadXmlFile("config.xml");
			
			MessageService.ShowInfo("Show info of file AFTER: " + "\n" +
				"Cipher: " + e.Database.KdfParameters.Count + "\n" +
				"Test: " + e.Database.KdfParameters + "\n" +
				"KdfUuid: " + e.Database.KdfParameters.KdfUuid + "\n");

			*/

        }
	}
}