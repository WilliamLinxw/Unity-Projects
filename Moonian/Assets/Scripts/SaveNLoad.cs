using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveNLoad
{
    // Ref: https://www.youtube.com/watch?v=XOjd_qU2Ido
    
    public static void SavePlayer(Player player, PlayerProperty pp, int[] it, int rf, int[] pked)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.moon";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player, pp, it, rf, pked);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.moon";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save Not Found in " + path);
            return null;
        }
    }
}
