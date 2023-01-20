using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
public static class StorageHandler
{
    private const string userFileName = "UserDatas";
    public static void SaveUserDatas()
    {
        var obj = ProjectController.Instance.UserDatas;
        // Add the File Path together with the files name and extension.
        // We will use .bin to represent that this is a Binary file.
        string FullFilePath = Application.persistentDataPath + "/" + userFileName + ".bin";
        // We must create a new Formattwr to Serialize with.
        BinaryFormatter Formatter = new BinaryFormatter();
        // Create a streaming path to our new file location.
        FileStream fileStream = new FileStream(FullFilePath, FileMode.Create);
        // Serialize the objedt to the File Stream
        Formatter.Serialize(fileStream, obj);
        // FInally Close the FileStream and let the rest wrap itself up.
        fileStream.Close();
    }
    public static UserDatas GetUser()
    {
        string FullFilePath = Application.persistentDataPath + "/" + userFileName + ".bin";
        // Check if our file exists, if it does not, just return a null object.
        if (File.Exists(FullFilePath))
        {
            BinaryFormatter Formatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(FullFilePath, FileMode.Open);
            object obj = Formatter.Deserialize(fileStream);
            fileStream.Close();
            // Return the uncast untyped object.
            return obj as UserDatas;
        }
        else
        {
            return null;
        }
    }
}
