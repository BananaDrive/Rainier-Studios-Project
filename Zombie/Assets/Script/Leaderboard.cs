using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

[System.Serializable]
public class ScoreEntry
{
    public float score;
    public string playerName;
}

[System.Serializable]
public class ScoreList
{
    public List<ScoreEntry> scores = new();
}
public class Leaderboard : MonoBehaviour
{
    public ScoreList scoreList = new();
    public Transform leaderBoard;
    public List<TMP_Text> playerStats = new();

    string filePath;

    void Start()
    {
        filePath = Application.persistentDataPath + "/leaderboard.json";

        if (leaderBoard != null)
        {
            GetReferences();
        }
    }

    public void SaveFile(ScoreEntry scoreEntry)
    {
        List<ScoreEntry> currentFiles = LoadFiles();
        currentFiles.Add(scoreEntry);
        string json = JsonUtility.ToJson(new ScoreList {scores = scoreList.scores} );
        File.WriteAllText(filePath, json);
    }

    public List<ScoreEntry> LoadFiles()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            scoreList = JsonUtility.FromJson<ScoreList>(json);
            return scoreList.scores;
        }
        return new List<ScoreEntry>();
    }

    public void GetReferences()
    {
        for (int i = 0; i < leaderBoard.transform.childCount; i++)
            playerStats.Add(leaderBoard.transform.GetChild(i).GetComponent<TMP_Text>());
        DisplayLeaderboard();
    }

    public void DisplayLeaderboard()
    {
        List<ScoreEntry> scores = LoadFiles();

        SortLeaderboard();

        for (int i = 0; i < scores.Count && i < leaderBoard.childCount; i++)
            playerStats[i].SetText(scores[i].playerName + ": " + scores[i].score);
        
    }

    public void SortLeaderboard()
    {
        for (int i = 0; i < scoreList.scores.Count - 1; i++)
        {
            for (int j = 0; j < scoreList.scores.Count - 1; j++)
            {
                if (scoreList.scores[j].score < scoreList.scores[j + 1].score)
                {
                    (scoreList.scores[j + 1].score, scoreList.scores[j].score) = (scoreList.scores[j].score, scoreList.scores[j + 1].score);
                }
            }
        }
    }
}
