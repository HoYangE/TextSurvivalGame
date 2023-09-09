using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Serialization;

[Serializable]
public class StatusData
{
    public int hunger;
    public int moisture;
    public int stress;
    public float temperature;
    public string position;
}

[Serializable]
public class TimeData
{
    public int year;
    public int month;
    public int day;
    public int hour;
    public int minute;
}

[Serializable]
public class PlayerData
{
    public Vector2 position;
}

[Serializable]
public class MovePopUpData
{
    public string name;
    public Vector2 position;
    public float time;
    public int timeLength;
}

public class DataManager : Singleton<DataManager>
{
    private string _statusDataPath;
    private string _timeDataPath;
    private string _playerDataPath;
    private string _movePopUpDataPath;
    
    public StatusData StatusData { get; set; } = new StatusData();
    public TimeData TimeData { get; set; } = new TimeData();
    public PlayerData PlayerData { get; set; } = new PlayerData();
    public MovePopUpData MovePopUpData { get; set; } = new MovePopUpData();
    
    private void Awake()
    {
        // 빌드용
        _statusDataPath = Application.persistentDataPath + "/StatusData.json";
        _timeDataPath = Application.persistentDataPath + "/TimeData.json";
        _playerDataPath = Application.persistentDataPath + "/PlayerData.json";
        _movePopUpDataPath = Application.persistentDataPath + "/MovePopUpData.json";

        // 테스트용
        //_statusDataPath = Application.dataPath + "/SaveData/StatusData.json";
        //_timeDataPath = Application.dataPath + "/SaveData/TimeData.json";
        //_playerDataPath = Application.dataPath + "/SaveData/PlayerData.json";
        //_movePopUpDataPath = Application.dataPath + "/SaveData/MovePopUpData.json";
    }

    #region SaveLoad

    public void SaveStatusData()
    {
        var jsonData = JsonUtility.ToJson(StatusData, true);
        File.WriteAllText(_statusDataPath, jsonData);
    }
    
    private StatusData LoadStatusData()
    {
        StatusData = JsonUtility.FromJson<StatusData>(File.ReadAllText(_statusDataPath));
        return StatusData;
    }
    
    public void SaveTimeData()
    {
        var jsonData = JsonUtility.ToJson(TimeData, true);
        File.WriteAllText(_timeDataPath, jsonData);
    }
    
    private TimeData LoadTimeData()
    {
        TimeData = JsonUtility.FromJson<TimeData>(File.ReadAllText(_timeDataPath));
        return TimeData;
    }
    
    public void SavePlayerData()
    {
        var jsonData = JsonUtility.ToJson(PlayerData, true);
        File.WriteAllText(_playerDataPath, jsonData);
    }
    
    private PlayerData LoadPlayerData()
    {
        PlayerData = JsonUtility.FromJson<PlayerData>(File.ReadAllText(_playerDataPath));
        return PlayerData;
    }
    
    public void SaveMovePopUpData()
    {
        var jsonData = JsonUtility.ToJson(MovePopUpData, true);
        File.WriteAllText(_movePopUpDataPath, jsonData);
    }
    
    private MovePopUpData LoadMovePopUpData()
    {
        MovePopUpData = JsonUtility.FromJson<MovePopUpData>(File.ReadAllText(_movePopUpDataPath));
        return MovePopUpData;
    }

    #endregion

    #region InitDelete

    private void InitStatusData()
    {
        StatusData.hunger = 100;
        StatusData.moisture = 100;
        StatusData.stress = 0;
        StatusData.temperature = 36.5f;
        StatusData.position = "베이스캠프";
    }

    private void InitTimeData()
    {
        TimeData.year = 1001;
        TimeData.month = 4;
        TimeData.day = 1;
        TimeData.hour = 10;
        TimeData.minute = 0;
    }
    
    private void InitPlayerData()
    {
        PlayerData.position = new Vector2(0, 0);
    }
    
    private void InitMovePopUpData()
    {
        MovePopUpData.name = null;
        MovePopUpData.position = new Vector2(0, 0);
        MovePopUpData.time = 0;
        MovePopUpData.timeLength = 0;
    }

    public void InitAllData()
    {
        InitStatusData();
        InitTimeData();
        InitPlayerData();
        InitMovePopUpData();
    }
    
    public void DeleteAllData()
    {
        File.Delete(_statusDataPath);
        File.Delete(_timeDataPath);
        File.Delete(_playerDataPath);
        File.Delete(_movePopUpDataPath);
    }
    
    #endregion

    #region DataCheck

    public void CheckStatusData()
    {
        if (!File.Exists(_statusDataPath))
        {
            InitStatusData();
            SaveStatusData();
        }
        else
        {
            LoadStatusData();
        }
    }
    
    public void CheckTimeData()
    {
        if (!File.Exists(_timeDataPath))
        {
            InitTimeData();
            SaveTimeData();
        }
        else
        {
            LoadTimeData();
        }
    }
    
    public void CheckPlayerData()
    {
        if (!File.Exists(_playerDataPath))
        {
            InitPlayerData();
            SavePlayerData();
        }
        else
        {
            LoadPlayerData();
        }
    }
    
    public void CheckMovePopUpData()
    {
        if (!File.Exists(_movePopUpDataPath))
        {
            InitMovePopUpData();
            SaveMovePopUpData();
        }
        else
        {
            LoadMovePopUpData();
        }
    }

    #endregion
    
    
}
