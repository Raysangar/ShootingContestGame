using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class LeaderboardManager
{
    public List<int> Leaderboard{ get; private set; }

    public LeaderboardManager(GameManager gameManager)
    {
        gameManager.OnGameFinished += OnGameFinished;
        LoadLeaderboard();
    }

    private void OnGameFinished(int finalScore)
    {
        int i = 0;
        while (i < Leaderboard.Count && Leaderboard[i] >= finalScore)
        {
            ++i;
        }
        if (i < Leaderboard.Count)
        {
            Leaderboard.Insert(i, finalScore);
            if (Leaderboard.Count > MaxSize)
                Leaderboard.RemoveAt(MaxSize);
            if (i == 0)
                AudioManager.Instance.PlayAudio(AudioManager.Instance.Library.newHighScore);
            SafeLeaderboard();
        }
        else if (Leaderboard.Count < MaxSize)
        {
            Leaderboard.Add(finalScore);
            if (Leaderboard.Count == 1)
                AudioManager.Instance.PlayAudio(AudioManager.Instance.Library.newHighScore);
            SafeLeaderboard();
        }    
    }


    private void LoadLeaderboard()
    {
        if (File.Exists(LeaderboarFilePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(LeaderboarFilePath, FileMode.Open);
            Leaderboard = (List<int>)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            Leaderboard = new List<int>();
        }
    }

    private void SafeLeaderboard()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(LeaderboarFilePath);
        bf.Serialize(file, Leaderboard);
        file.Close();
    }

    private readonly string LeaderboarFilePath = Path.Combine(Application.persistentDataPath, "leaderboard.save");

    private const int MaxSize = 5;
}
