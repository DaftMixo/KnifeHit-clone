using System;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private const string DataKey = "GameData";
    
    public static void SaveData(GameData data)
    {
        var jsonData = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(DataKey, jsonData);
    }

    public static GameData LoadData()
    {
        if (!PlayerPrefs.HasKey(DataKey))
            return new GameData();
        
        var jsonData = PlayerPrefs.GetString(DataKey);
        var _data = JsonUtility.FromJson<GameData>(jsonData);
        return _data;
    }
}

[Serializable]
public class GameData
{
    public int Score;
    public int Money;
    public int Stage;

    public SettingsData Settings = new SettingsData();

    [Serializable]
    public class SettingsData
    {
        public float Music = .7f;
        public float Sound = .7f;
        public bool Vibration;
    }

    public override string ToString()
    {
        string text = "Score: " + Score + "\tMoney: " + Money + "\tStage: " + Stage +
             "\nSettings:  Music: " + Settings.Music + "\tSound: " + Settings.Sound +
             "\tVibration: " + Settings.Vibration;
        return text;
    }
}
