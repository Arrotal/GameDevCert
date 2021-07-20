using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class GameDataManager : MonoBehaviour
{
    private string _saveFile;
    SaveObject gameData = new SaveObject();

    private void Awake()
    {
       
        _saveFile = Application.persistentDataPath + "/gamedata.json";
        jsonString = JsonUtility.ToJson(gameData);
    }
    private string jsonString;
    public void readFile()
    {
        if (File.Exists(_saveFile))
        {
            string fileContents = File.ReadAllText(_saveFile);
            gameData = JsonUtility.FromJson<SaveObject>(fileContents);
        }
    }

    public void writeFile()
    {
        File.WriteAllText(_saveFile, jsonString);
    }

    private class SaveObject
    {

    }
}
