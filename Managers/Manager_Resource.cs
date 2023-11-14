
using Framework;
using System.Globalization;

namespace SpartaDungeon.Managers
{
    public class Manager_Resource
    {
        #region Member Variables

        // Json , txt 파일을 보관
        private Dictionary<string, string>? _textResources;

        private bool _isComplete;

        #endregion

        #region Properties
        public bool IsComplete { get { return _isComplete; } }
        #endregion

        #region Constructor & Initalize
        public Manager_Resource()
        {
            Initialize();
        }
        private void Initialize()
        {
            _textResources = new Dictionary<string, string>();
            _isComplete = false;
        }
        #endregion

        #region Main Methods
        public void LoadAllResources()
        {
            string folderPath = Utilities.GetResourceFolderPath();

            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine($"폴더 {folderPath}가 존재하지 않습니다.");
                throw new Exception("폴더가 존재하지 않습니다.");
            }
            else
            {
                string[] files = Directory.GetFiles(folderPath);

                foreach (string filePath in files)
                {
                    string fileName = Path.GetFileNameWithoutExtension(filePath);
                    string fileExtension = Path.GetExtension(filePath);

                    if (IsSupportedFileExtension(fileExtension) && _textResources != null)
                    {
                        foreach (ResourceKeys key in Enum.GetValues(typeof(ResourceKeys)))
                        {
                            string keyName = key.ToString();

                            if (fileName.Equals(keyName, StringComparison.OrdinalIgnoreCase))
                            {
                                string fileContents = File.ReadAllText(filePath);
                                _textResources[keyName] = fileContents;
                                break;
                            }
                        }
                    }
                }
            }

            _isComplete = true;
        }
        public void LoadTextResource(ResourceKeys key, string path)
        {
            string strKey = key.ToString();
            try
            {
                if (File.Exists(path) && _textResources != null)
                {
                    string textFile = File.ReadAllText(path);
                    _textResources[strKey] = textFile;
                }
                else
                    Console.WriteLine($"Text resources not found : {strKey}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading text resource : {ex.Message}");
            }
        }

        public string GetTextResource(ResourceKeys key)
        {
            string strKey = key.ToString();
            if (_textResources?.ContainsKey(strKey) == true)
                return _textResources[strKey];
            else
            {
                Console.WriteLine($"Text resources not found : {strKey}");
                return string.Empty;
            }
        }
        #endregion

        #region Extension Helper Methods
        private bool IsSupportedFileExtension(string fileExtension)
        {
            return fileExtension.Equals(".json", StringComparison.OrdinalIgnoreCase) ||
                fileExtension.Equals(".txt", StringComparison.OrdinalIgnoreCase);
        }
        #endregion
    }
}
