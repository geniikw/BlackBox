using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerData : MonoBehaviour {

    static PlayerData m_ins;

    public PlayerDataContainer m_playerData;

    public static PlayerData instance
    {
        get
        {
            return m_ins;
        }
    }
    private void OnEnable()
    {
        m_ins = this;
        LoadData();
    }

    public void ClearStage(int stage, int score)
    {
        if(stage > m_playerData.clearStage)
        {
            m_playerData.clearStage = stage;
        }

        if (m_playerData.clearRecord.Count < stage + 1)
        {
            //한스테이지씩 높아지면 괜찮을듯?
            m_playerData.clearRecord.Add(score);
        }
        else
        {
            m_playerData.clearRecord[stage] = score;
        }
    }
    public int GetClearScore(int stage)
    {
        if (m_playerData.clearRecord.Count < stage + 1)
        {
            return -1;
        }
        else
        {
            return m_playerData.clearRecord[stage];
        }
    }
    
    public void LoadData()
    {
        string output;
        if (!File.Exists(Application.persistentDataPath + "/Player.sav"))
        {
            using (var wr = new StreamWriter(Application.persistentDataPath + "/Player.sav"))
            {
                var newData = new PlayerDataContainer();
                Debug.Log("write new player data.");
                output = JsonUtility.ToJson(newData);
                wr.Write(output);
            }
        }
        else
        {
            using (var sr = new StreamReader(Application.persistentDataPath + "/Player.sav"))
            {
                output = sr.ReadToEnd();
            }
        }

        m_playerData = JsonUtility.FromJson<PlayerDataContainer>(output);
    }


    private void OnDisable()
    {
        SaveData();
    }
    public void SaveData()
    {
        using (var wr = new StreamWriter(Application.persistentDataPath + "/Player.sav"))
        {
            var w = JsonUtility.ToJson(m_playerData);

            wr.Write(w);
        }
    }
}


[System.Serializable]
public class PlayerDataContainer
{
    public int clearStage;
    public List<int> clearRecord;

    public PlayerDataContainer()
    {
        clearStage = -1;
        clearRecord = new List<int>();
    }
}






