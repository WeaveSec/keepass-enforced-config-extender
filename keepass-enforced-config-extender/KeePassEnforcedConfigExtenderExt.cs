/*
  KeePass Enforced Config Extender - A KeePass plugin that extends the Enforced Configuration feature
  Copyright (C) WeaveSec <weavesec@gmail.com>
*/

using KeePass.Forms;
using KeePass.Plugins;

using KeePassLib;
using KeePassLib.Cryptography.KeyDerivation;
using KeePassLib.Utility;
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

		private async void OnFileSaving(object sender, FileSavingEventArgs e)
		{
            PwDatabase sourceDb = e.Database;

			var warn_form = new Warn_Window();

			warn_form.Show();

			string userInput = await warn_form.UserSelectionTask.Task;

			//KdfParameters test = KdfParameters.DeserializeExt(KdfParameters.SerializeExt(sourceDb.KdfParameters));

            KdfEngine kdf = KdfPool.Get(e.Database.KdfParameters.KdfUuid);


			string kdftype = "None";

            if (kdf is AesKdf)
			{
				kdftype = "AesKdf";
			}
			else if(kdf is Argon2Kdf) {

		
				kdftype = "Argon2Kdf";

                /**ulong uIt = p.GetUInt64(Argon2Kdf.ParamIterations,
                    Argon2Kdf.DefaultIterations);
                ulong uMem = p.GetUInt64(Argon2Kdf.ParamMemory,
                    Argon2Kdf.DefaultMemory);
                uint uPar = p.GetUInt32(Argon2Kdf.ParamParallelism,
                    Argon2Kdf.DefaultParallelism);
				*/
            };

            /**
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






            ulong testMemoryNum = sourceDb.KdfParameters.GetUInt64(ArgonParamMemory, DefaultMemory);
            uint testParallelismNum = sourceDb.KdfParameters.GetUInt32(ArgonParamParallelism, DefaultParallelism);



            MessageService.ShowInfo("Show info of file SAVING: " + "\n" +
                "Cipher: " + e.Database.KdfParameters.Count + "\n" +
                "KdfUuid: " + e.Database.KdfParameters.KdfUuid + "\n" +
				"warn_form.text: " + userInput + "\n" + 
				"KdfType: " + kdftype + "\n" +
				"ArgonIterationsNum: " + testIterationsNum + "\n" +
				"AesRoundsNum: " + testRoundsNum + "\n" +
				"ArgonMemoryNum: " + testMemoryNum + "\n" +
				"ArgonParallelismNum: " + testParallelismNum + "\n" +
				"Cancel?: " + e.Cancel + "\n"
				);

			string metStandard = "unknown";

			if (kdf is Argon2Kdf && testIterationsNum >= 15 && testMemoryNum >= 20971520 && testParallelismNum >=1)
			{
				metStandard = "true";
				e.Cancel = false;
			}
			else
			{
				metStandard = "false";
				e.Cancel = true;
			}


			MessageService.ShowInfo("Does current settings meet standard? " + metStandard + "\n" +
				"Cancel?: " + e.Cancel);



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