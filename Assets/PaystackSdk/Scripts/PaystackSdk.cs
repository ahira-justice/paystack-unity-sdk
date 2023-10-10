using System;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization.Formatters.Binary;
using PaystackSdk.Scripts.ApiService;
using PaystackSdk.Scripts.Exceptions;
using PaystackSdk.Scripts.Models;

namespace PaystackSdk.Scripts
{
    
    public static class PaystackSdk
    {
        
        public static PaystackApiService ApiService { get; } = new PaystackApiService(new HttpClient());

        private static string _prefsDirectory;
        private static string _prefsFile;

        private static string BaseDirectory { get; set; }
        private static string AppDirectory { get; set; }
        private static string PrefsFile
        {
            get => _prefsFile;
            set
            {
                if (!File.Exists(value))
                {
                    var prefs = new Prefs();

                    Stream saveFileStream = File.Create(value);
                    var serializer = new BinaryFormatter();

                    serializer.Serialize(saveFileStream, prefs);
                    saveFileStream.Close();
                }

                _prefsFile = value;
            }
        }
        private static string PrefsDirectory
        {
            get => _prefsDirectory;
            set
            {
                if (!Directory.Exists(value))
                    Directory.CreateDirectory(value);

                _prefsDirectory = value;
            }
        }

        private static void Initialize()
        {
            BaseDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            AppDirectory = @"\PaystackSdk";
            PrefsDirectory = BaseDirectory + AppDirectory + @"\Data";
            PrefsFile = PrefsDirectory + @"\PaystackSdkPrefs.bin";
        }
        
        #region Persistence
        
        public static Prefs GetCurrentPrefs()
        {
            Initialize();

            try
            {
                using (Stream openFileStream = File.OpenRead(PrefsFile))
                {
                    var deserializer = new BinaryFormatter();

                    var prefs = (Prefs)deserializer.Deserialize(openFileStream);
                    openFileStream.Close();

                    return prefs;
                }
            }
            catch (FileNotFoundException)
            {
                throw new PrefsDoesNotExistException();
            }
            catch (Exception)
            {
                throw new PrefsInvalidException();
            }
        }

        public static void SavePrefs(Prefs prefs)
        {
            Initialize();
            
            Stream saveFileStream = File.OpenWrite(PrefsFile);
            var serializer = new BinaryFormatter();

            serializer.Serialize(saveFileStream, prefs);
            saveFileStream.Close();
        }
        
        #endregion
        
    }
    
}
