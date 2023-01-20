using System.IO;
using UnityEditor;
using UnityEngine;

public class EditorUtility {

#if UNITY_EDITOR
    [MenuItem("Zigzag/Delete Persistant Data")]
    static void DeletePersistantDatas()
    {
        string[] filePaths = Directory.GetFiles(Application.persistentDataPath);
        foreach (string filePath in filePaths)
        {
            Debug.Log("Persistant datas deleted at " + filePath);
            File.Delete(filePath);
        }
    }
#endif
}
