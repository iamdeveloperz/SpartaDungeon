
using Newtonsoft.Json;
using SpartaDungeon.Managers;

namespace Framework
{
    #region Public Enums
    public enum E_TITLE_MENU
    {
        GAME_START,
        GAME_CREDIT,
        GAME_EXIT
    }

    public enum E_SORTING_TYPE
    {
        NAME,
        EQUIP,
        TYPEATK,
        TYPEDEF
    }

    public enum ResourceKeys
    {
        TitleText,
        MainGameMenuText,
        StatusMenuText,
        InventoryMenuText,
        CreatePlayerText,
        Player,
        ItemList,
    }
    #endregion

    #region Public Structure
    public struct COORD
    {
        int X { get; set; }
        int Y { get; set; }
    }
    #endregion

    #region Public Static Class
    public static class ResourcePaths
    {
        public const string TITLE_TXT_PATH = "TitleText.txt";
        public const string MAIN_TXT_PATH = "MaimGameMenu.txt";
        public const string PLAYER_JSON_PATH = "Player.json";
        public const string INVENTORY_JSON_PATH = "Inventory.json";
        public const string ITEMLIST_JSON_PATH = "ItemList.json";
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
        //public static T? LoadFromJson<T>(string filePath)
        //{
        //    filePath = Path.Combine(GetResourceFolderPath(), filePath);

        //    string json = ReadTextFile(filePath);
        //    T? obj = JsonConvert.DeserializeObject<T>(json);

        //    return obj;
        //}

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

        public static int CheckValidInput(int min, int max)
        {
            int keyInput;
            bool result;

            do
            {
                Manager.Instance.UI.PrintTextBoxMessage(
                    "메뉴를 선택 해주세요. (엔터키를 누를 필요 없습니다)", 5, ConsoleColor.Yellow);
                result = int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out keyInput);
            } while (result == false || CheckIfValid(keyInput, min, max) == false);

            return keyInput;
        }

        private static bool CheckIfValid(int keyInput, int min, int max)
        {
            if (min <= keyInput && keyInput <= max) return true;
            return false;
        }
        #endregion
    }
}
