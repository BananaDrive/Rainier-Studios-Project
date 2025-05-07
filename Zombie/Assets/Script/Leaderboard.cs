using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class ScoreEntry
{
    public float score;
    public string playerName;
}

public class ScoreList
{
    public List<ScoreEntry> scores;
}
public class Leaderboard : MonoBehaviour
{
    public Transform leaderBoard;
    public TMP_Text[] playerStats;

    string filePath;

    void Start()
    {
        filePath = Application.persistentDataPath + "/leaderboard.json";

        if (leaderBoard != null)
        {
            GetReferences();
        }
    }

    public void SaveFile(List<ScoreEntry> scores)
    {
        string json = JsonUtility.ToJson(new ScoreList {scores = scores} );
        File.WriteAllText(filePath, json);
    }

    public List<ScoreEntry> LoadFiles()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<ScoreList>(json).scores;
        }
        return new List<ScoreEntry>();
    }

    public void GetReferences()
    {
        for (int i = 0; i < leaderBoard.transform.childCount; i++)
            playerStats[i] = GetComponent<TMP_Text>();
        DisplayLeaderboard();
    }

    public void DisplayLeaderboard()
    {
        List<ScoreEntry> scores = LoadFiles();

        for (int i = 0; i < scores.Count; i++)
        {
            playerStats[i].SetText(scores[i].playerName + ": " + scores[i].score);
        }
    }
}
