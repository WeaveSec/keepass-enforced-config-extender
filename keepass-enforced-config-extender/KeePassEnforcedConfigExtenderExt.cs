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
            m_host.MainWindow.FileCreated += this.OnFileCreated;

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

            m_host.MainWindow.FileCreated -= this.OnFileCreated;

            m_host.MainWindow.FileSaving -= this.OnFileSaving;
        }

		private void OnFileSaving(object sender, FileSavingEventArgs e)
		{
      PwDatabase sourceDb = e.Database;

			bool standardMet = VerifyStandard(sourceDb);


            // If KDF standard not met, promt user warning
            if (standardMet == false)
            {
                var warn_form = new Warn_Window();

                warn_form.ShowDialog();

                warn_form.Dispose();

                // Gets button response from user
                bool ContinueSave = warn_form.ContinueSave;

                MessageService.ShowInfo("continueSave: " + ContinueSave);

                if (ContinueSave == true)
                {
                    sourceDb.KdfParameters = (new Argon2Kdf()).GetDefaultParameters();

                    sourceDb.KdfParameters.SetUInt64(Argon2Kdf.ParamIterations, 15);
                    sourceDb.KdfParameters.SetUInt64(Argon2Kdf.ParamMemory, 20 * 1024 * 1024);
                    sourceDb.KdfParameters.SetUInt32(Argon2Kdf.ParamParallelism, 1);
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

		private bool VerifyStandard(PwDatabase sourceDb)
		{
            bool metStandard = false;

            KdfEngine kdf = KdfPool.Get(sourceDb.KdfParameters.KdfUuid);




            /**
             * List Kdf options in Pool
            foreach (KdfEngine kdf_this in KdfPool.Engines)
            {
                MessageService.ShowInfo("kdfName: " + "\n" + kdf_this.Name);
            };
			*/


            string ArgonParamParallelism = "P"; // UInt32
            string ArgonParamMemory = "M"; // UInt64
            string AesParamRounds = "R"; // Ulong
            string ArgonParamIterations = "I";

            ulong DefaultKeyEncryptionRounds = 0;
            ulong testRoundsNum = sourceDb.KdfParameters.GetUInt64(AesParamRounds, DefaultKeyEncryptionRounds);


            ulong DefaultIterations = 0;
            ulong testIterationsNum = sourceDb.KdfParameters.GetUInt64(ArgonParamIterations, DefaultIterations);


            ulong DefaultMemory = 0; //64 * 1024 * 1024; // 64 MB

            uint DefaultParallelism = 0; //2;


            bool Cancel = false;



            ulong testMemoryNum = sourceDb.KdfParameters.GetUInt64(ArgonParamMemory, DefaultMemory);
            uint testParallelismNum = sourceDb.KdfParameters.GetUInt32(ArgonParamParallelism, DefaultParallelism);


            if (kdf is Argon2Kdf && testIterationsNum >= 15 && testMemoryNum >= 20971520 && testParallelismNum >= 1)
            {
                metStandard = true;
                Cancel = false;
            }
            else
            {
                metStandard = false;
                Cancel = true;
            }

            MessageService.ShowInfo("Show info of file SAVING: " + "\n" +
                "Cipher: " + sourceDb.KdfParameters.Count + "\n" +
                "KdfUuid: " + sourceDb.KdfParameters.KdfUuid + "\n" +
                //              "warn_form.text: " + userInput + "\n" +
                "KdfType: " + kdf.GetType() + "\n\n" +
                "---Argon Info---" + "\n" +
                "ArgonIterationsNum: " + testIterationsNum + "\n" +
                "ArgonMemoryNum: " + testMemoryNum + "\n" +
                "ArgonParallelismNum: " + testParallelismNum + "\n\n" +
                "---AES Info---" + "\n" +
                "AesRoundsNum: " + testRoundsNum + "\n\n" +
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

		private void OnFileCreated(object sender, FileCreatedEventArgs e)
		{

			// KdfParameters p = e.Database.KdfParameters;

			//MessageService.ShowInfo("Compression: " + e.Database.Compression + "\n" +
			//	"Cipher: " + e.Database.KdfParameters.Count);

		}
	}
}