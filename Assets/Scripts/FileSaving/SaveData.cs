using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting;

public static class SaveData
{
    public static void SaveToFile()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string saveLocation = Application.persistentDataPath + "/userdata.txt";
        FileStream stream = new FileStream(saveLocation, FileMode.Create);
        UserData user = new UserData();

        formatter.Serialize(stream, user);
        stream.Close();
    }

    public static UserData LoadFromFile()
    {
        string saveLocation = Application.persistentDataPath + "/userdata.txt";
        if (File.Exists(saveLocation)) 
        {
            BinaryFormatter formatter = new BinaryFormatter(); //the user data is stored as binary for encryption.
            FileStream stream = new FileStream(saveLocation, FileMode.Open, FileAccess.Read);
            UserData user = formatter.Deserialize(stream) as UserData;
            stream.Close();
            return user;
        }
        else
        {
            Debug.Log("File not found!");
            return null;
        }
    }
}
