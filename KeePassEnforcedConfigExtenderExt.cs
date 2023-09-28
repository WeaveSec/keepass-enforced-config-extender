/*
  KeePass Enforced Config Extender - A KeePass plugin that extends the Enforced Configuration feature
  Copyright (C) WeaveSec <weavesec@gmail.com>
*/

using KeePass.Forms;
using KeePass.Plugins;

using KeePassLib;
using KeePassLib.Cryptography.KeyDerivation;
using KeePassLib.Utility;
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
        }

		private void OnFileSaved(object sender, FileSavedEventArgs e)
		{

            PwDatabase sourceDb = e.Database;

			sourceDb.KdfParameters = (new Argon2Kdf()).GetDefaultParameters();

			KdfParameters lego = KdfParameters.DeserializeExt(KdfParameters.SerializeExt(sourceDb.KdfParameters));

			//sourceDb.KdfParameters.GetUInt64(e.Database.KdfParameters.GetInt64(KdfParameters.DeserializeExt(Keys));
			//sourceDb.KdfParameters.SetUInt64(AesKdf.ParamRounds, 620000);
			sourceDb.KdfParameters.SetUInt64(Argon2Kdf.ParamIterations, 15);
            sourceDb.KdfParameters.SetUInt64(Argon2Kdf.ParamMemory, 20 * 1024 * 1024);
            sourceDb.KdfParameters.SetUInt32(Argon2Kdf.ParamParallelism, 1);


            MessageService.ShowInfo("Show info of file: " + "\n" +
				"Cipher: " + e.Database.KdfParameters.Count + "\n" +
				"Test: " + e.Database.KdfParameters + "\n" +
				"KdfUuid: " + e.Database.KdfParameters.KdfUuid + "\n");
		}

		private void OnFileCreated(object sender, FileCreatedEventArgs e)
		{

			// KdfParameters p = e.Database.KdfParameters;

			MessageService.ShowInfo("Compression: " + e.Database.Compression + "\n" +
				"Cipher: " + e.Database.KdfParameters.Count);

		}
	}
}