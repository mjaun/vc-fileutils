using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using VCFileUtils.Model;

namespace VCFileUtils.Helpers
{
    static class SettingsManager
    {
        static Dictionary<VCProjectWrapper, ExtensionSettings> settingsDict = new Dictionary<VCProjectWrapper, ExtensionSettings>();

        public static ExtensionSettings GetDefaultSettings(VCProjectWrapper project)
        {
            ExtensionSettings settings = new ExtensionSettings();
            settings.ProjectRoot = Path.GetDirectoryName(project.ProjectFile);
            return settings;
        }

        public static ExtensionSettings GetSettings(VCProjectWrapper project)
        {
            ExtensionSettings settings;
            settingsDict.TryGetValue(project, out settings);

            if (settings == null)
            {
                string settingsFile = GetSettingsPath(project);

                if (!File.Exists(settingsFile))
                {
                    settings = GetDefaultSettings(project);
                }
                else
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ExtensionSettings));

                    try
                    {
                        TextReader fileStream = new StreamReader(settingsFile);

                        try
                        {
                            settings = (ExtensionSettings)serializer.Deserialize(fileStream);
                        }
                        finally
                        {
                            fileStream.Close();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Error: Could not load VC File Utils settings!", "VC File Utils", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        settings = GetDefaultSettings(project);
                    }
                }

                settingsDict.Add(project, settings);
            }

            return settings;
        }

        public static bool SaveSettings(VCProjectWrapper project)
        {
            ExtensionSettings settings = GetSettings(project);
            XmlSerializer serializer = new XmlSerializer(typeof(ExtensionSettings));
            string settingsFile = GetSettingsPath(project);

            try
            {
                TextWriter fileStream = new StreamWriter(settingsFile, false);

                try
                {
                    serializer.Serialize(fileStream, settings);
                }
                finally
                {
                    fileStream.Close();
                }
            }
            catch
            {
                MessageBox.Show("Error: Could not save VC File Utils settings!", "VC File Utils", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        static string GetSettingsPath(VCProjectWrapper project)
        {
            return Path.Combine(Path.GetDirectoryName(project.ProjectFile), "vc-fileutils.settings");
        }
    }
}
