using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SpartaDungeonProject
{
    public static class Utilities
    {
        #region LITERALS
        /* Scene IDX */
        public const int TITLE_SCENE_IDX = 0;
        public const int MAIN_SCENE_IDX = 1;
        public const int CREDIT_SCENE_IDX = 2;

        /* PATH */
        public const string PLAYER_JSON_PATH = "Player.json";
        public const string INVENTORY_JSON_PATH = "Inventory.json";
        public const string ITEM_JSON_PATH = "Item.json";
        public const string TITLE_PATH = "TitleText.txt";

        /* Item Name */
        public const string BEGINNER_WEAPON = "낡은 검";
        public const string BEGINNER_ARMOR = "무쇠 갑옷";
        #endregion

        #region Json
        public static void SaveToJson<T>(T obj, string filePath)
        {
            filePath = Path.Combine(GetResourceBasePath(), filePath);

            string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public static T? LoadFromJson<T>(string filePath)
        {
            filePath = Path.Combine(GetResourceBasePath(), filePath);

            string json = ReadTextFile(filePath);
            T? obj = JsonConvert.DeserializeObject<T>(json);

            return obj;
        }

        public static LinkedList<T> LoadFromJsonToList<T>(string filePath)
        {
            filePath = Path.Combine(GetResourceBasePath(), filePath);

            LinkedList<T>? itemllist = null;
            try
            {
                using (StreamReader reader = File.OpenText(filePath))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    itemllist = serializer.Deserialize<LinkedList<T>>(new JsonTextReader(reader));
                }
            }
            catch (Exception ex)
            {
                Console.Write($"Error가 발생했습니다. JSON : {ex.Message}");
            }

            return itemllist ?? new LinkedList<T>();
        }

        public static bool IsJsonFile(string filePath)
        {
            filePath = Path.Combine(GetResourceBasePath(), filePath);

            if (File.Exists(filePath))
                return true;
            else
                return false;
        }
        #endregion

        #region File Stream
        /* Functions */
        public static string[] ReadTextFileByLines(string filePath)
        {
            filePath = Path.Combine(GetResourceBasePath(), filePath);

            // 파일이 존재하는지 확인
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"File Not Found : {filePath}");

            List<string> messages = new List<string>();

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                    messages.Add(line);
            }

            return messages.ToArray();
        }

        public static string ReadTextFile(string filePath)
        {
            filePath = Path.Combine(GetResourceBasePath(), filePath);

            // 파일이 존재하는지 확인
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"File Not Found : {filePath}");

            string content = File.ReadAllText(filePath);

            return content;
        }

        /* Helper Methods */
        public static string GetResourceBasePath()
        {
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // 상위 폴더로 이동하고 "SpartaDungeonProject" 폴더로 이동.
            string spartaDungeonProjectDirectory = Path.Combine(currentDirectory, "..", "..", "..");
            string ResourcePath = Path.Combine(spartaDungeonProjectDirectory, "Resources");

            return ResourcePath;
        }

        #endregion
    }


    #region Singleton Base
    public class Singleton<T> where T : class, new()
    {
        private static readonly object lockObject = new object();
        private static T? instance;

        public static T Instance
        {
            get
            {
                // 혹시 모를 다중 스레드에서 안전하게 인스턴스를 생성
                lock (lockObject)
                {
                    if (instance == null)
                        instance = new T();
                }

                return instance;
            }
        }
    }

    #endregion
}
