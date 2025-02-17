using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class GameLogger
{
    [System.Serializable]
    public class GameLogEntry : IEquatable<GameLogEntry>
    {
        public string time;
        public int sceneNumber;
        public int points;
        public float lives;
        public Vector3 playerPosition;
        public bool isCompleted;

        public DateTime GetDateTime()
        {
            return DateTime.Parse(time);
        }

        public bool Equals(GameLogEntry other)
        {
            if (other == null) return false;
            return time == other.time &&
                   sceneNumber == other.sceneNumber &&
                   points == other.points &&
                   lives == other.lives &&
                   playerPosition == other.playerPosition &&
                   isCompleted == other.isCompleted;
        }

        public override int GetHashCode()
        {
            return time.GetHashCode() ^
                   sceneNumber.GetHashCode() ^
                   points.GetHashCode() ^
                   lives.GetHashCode() ^
                   playerPosition.GetHashCode() ^
                   isCompleted.GetHashCode();
        }
    }

    private string fileName = "logi.json";
    private HashSet<GameLogEntry> logEntries;

    public GameLogger()
    {
        logEntries = new HashSet<GameLogEntry>();
        LoadLog();
    }

    public void DeleteLogs()
    {
        logEntries.Clear();
        SaveLog();
    }

    public void AddLogEntry(int sceneNumber, int points, float lives, Vector3 playerPosition, bool isCompleted)
    {
        GameLogEntry entry = new GameLogEntry
        {
            time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            sceneNumber = sceneNumber,
            points = points,
            lives = lives,
            playerPosition = playerPosition,
            isCompleted = isCompleted
        };

        logEntries.Add(entry);
        SaveLog();
    }

    private void SaveLog()
    {
        string json = JsonUtility.ToJson(new GameLogWrapper { logEntries = logEntries.ToList() }, true);
        string path = Path.Combine(Application.persistentDataPath, fileName);
        File.WriteAllText(path, json);
        Debug.Log($"Log saved to: {path}");
    }

    private void LoadLog()
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            GameLogWrapper wrapper = JsonUtility.FromJson<GameLogWrapper>(json);
            logEntries = new HashSet<GameLogEntry>(wrapper.logEntries);
            Debug.Log($"Log loaded from: {path}");
        }
        else
        {
            logEntries = new HashSet<GameLogEntry>();
            Debug.LogWarning($"No log file found at: {path}");
        }
    }

    [System.Serializable]
    private class GameLogWrapper
    {
        public List<GameLogEntry> logEntries;
    }

    public GameLogEntry GetLatestLogForScene(int sceneNumber)
    {
        var entriesForScene = logEntries.Where(entry => entry.sceneNumber == sceneNumber).ToList();

        if (entriesForScene.Count == 0)
        {
            return null;
        }

        entriesForScene.Sort((entry1, entry2) => entry2.GetDateTime().CompareTo(entry1.GetDateTime()));

        var latestEntry = entriesForScene.FirstOrDefault();

        if (latestEntry != null && latestEntry.isCompleted)
        {
            return null;
        }

        return latestEntry;
    }

    public int getMaxCompletedScene()
    {
        int maxScene = 0;
        foreach (var entry in logEntries)
        {
            if (entry.sceneNumber > maxScene && entry.isCompleted)
            {
                maxScene = entry.sceneNumber;
            }
        }
        return maxScene;
    }

    public int getTotalMoney()
    {
        int sum = 0;
        foreach (var entry in logEntries)
        {
            if (entry.isCompleted)
            {
                sum += entry.points;
            }
        }
        return sum;
    }
}