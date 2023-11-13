
using Framework;
using Newtonsoft.Json;
using System.Numerics;

namespace SpartaDungeon
{
    public class Manager_Data
    {
        #region Member Variables

        #endregion

        #region Player
        public bool IsLoadPlayerComplete()
        {
            var playerLoadFile = Manager.Instance.Resource.GetTextResource(ResourceKeys.Player);

            if (string.IsNullOrEmpty(playerLoadFile))
                return false;
            else
                return true;
        }

        public void SavePlayer(Player player, ResourceKeys key)
        {
            var textData = Manager.Instance.Resource.GetTextResource(key);
            var playerPath = Path.Combine(Utilities.GetResourceFolderPath(), Utilities.PLAYER_JSON_PATH); 

            if(string.IsNullOrEmpty(textData))
            {
                string json = JsonConvert.SerializeObject(player, Formatting.Indented);
                File.WriteAllText(playerPath, json);
            }
            else
            {
                string backupPath = key.ToString() + "_backup.json";
                if(File.Exists(playerPath))
                {
                    Console.ReadLine();
                    File.Copy(playerPath, backupPath, true);
                    File.Delete(playerPath);
                }

                string json = JsonConvert.SerializeObject(player, Formatting.Indented);
                File.WriteAllText(playerPath, json);
            }
        }
        #endregion

        #region Inventory
        public void SaveInventory(Inventory inventory, string filePath)
        {
            string json = JsonConvert.SerializeObject(inventory, Formatting.Indented);
            File.WriteAllText(Utilities.PLAYER_JSON_PATH, json);
        }
        #endregion
    }
}
