using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using RPStudio.Core.Singleton;

public class SaveManager : Singleton<SaveManager>
{
    private SaveSetup _saveSetup;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        _saveSetup = new SaveSetup();
        _saveSetup.lastLevel = 0;
        _saveSetup.playerName = "Player1";
    }
    #region SAVE
    [NaughtyAttributes.Button]
    private void Save() 
    {
        string setupToJson = JsonUtility.ToJson(_saveSetup, true);
        Debug.Log(setupToJson);
        SaveFile(setupToJson);
    }

    private void SaveLastLevel(int level )
    {
        _saveSetup.lastLevel = level;
        Save();
    }
    #endregion

    private void SaveFile(string json) 
    {
        string path = Application.persistentDataPath +"/save.txt";

        Debug.Log(path);
        File.WriteAllText(path, json);
    }

    [NaughtyAttributes.Button]
    private void SaveLevelOne()
    {
        SaveLastLevel(1);
    }


}

[System.Serializable]
public class SaveSetup
{
    public int lastLevel;
    public string playerName;
}
