
using Newtonsoft.Json;

namespace Framework
{
    #region Public Structure
    public struct COORD
    {
        int X { get; set; }
        int Y { get; set; }
    }
    #endregion

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

        #endregion

        #region Json
        // Generic Json으로 직렬화된 데이터 저장
        public static void SaveToJson<T>(T obj, string filePath)
        {
            filePath = Path.Combine(GetResourceFolderPath(), filePath);

            string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        // Generic Json 직렬화된 데이터 역직렬화
        public static T? LoadFromJson<T>(string filePath)
        {
            filePath = Path.Combine(GetResourceFolderPath(), filePath);

            string json = ReadTextFile(filePath);
            T? obj = JsonConvert.DeserializeObject<T>(json);

            return obj;
        }

        public static List<T> LoadFromJsonToList<T>(string filePath)
        {
            filePath = Path.Combine(GetResourceFolderPath(), filePath);

            List<T>? itemllist = null;

            try
            {
                using (StreamReader reader = File.OpenText(filePath))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    itemllist = serializer.Deserialize<List<T>>(new JsonTextReader(reader));
                }
            }
            catch (Exception ex)
            {
                Console.Write($"Error가 발생했습니다. JSON : {ex.Message}");
            }

            return itemllist ?? new List<T>();
        }
        #endregion

        #region File Stream
        // 한줄씩 읽어 String 배열로 반환하는 함수
        public static string[] ReadTextFileByLines(string filePath)
        {
            filePath = Path.Combine(GetResourceFolderPath(), filePath);

            // 파일이 존재하는지 확인
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"File Not Found : {filePath}");

            List<string> messages = new List<string>();

            using (StreamReader reader = new StreamReader(filePath))
            {
                string? messageLine;
                while ((messageLine = reader.ReadLine()) != null)
                    messages.Add(messageLine);
            }

            return messages.ToArray();
        }

        // 전체 텍스트를 읽어 하나에 String으로 반환하는 함수
        public static string ReadTextFile(string filePath)
        {
            filePath = Path.Combine(GetResourceFolderPath(), filePath);

            // 파일이 존재하는지 확인
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"File Not Found : {filePath}");

            string content = File.ReadAllText(filePath);

            return content;
        }
        #endregion

        #region Helper Methods
        // 기본 Resource Folder(상대 경로)를 찾기위한 함수
        public static string GetResourceFolderPath()
        {
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // 상위 폴더로 이동하고 "SpartaDungeon" 폴더로 이동.
            string spartaDungeonProjectDirectory = Path.Combine(currentDirectory, "..", "..", "..");
            string ResourcePath = Path.Combine(spartaDungeonProjectDirectory, "Resources");

            return ResourcePath;
        }
        #endregion
    }
}
