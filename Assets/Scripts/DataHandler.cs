using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;


public class DataHandler : MonoBehaviour
{
    public static DataHandler Instance;
    public string PlayerName;
    public List<SaveData> scoreData;
    public SaveData newplayer;
    public int Difficulty;
    public int layers;

    private void Awake()
    {

        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        scoreData = new List<SaveData>();
        LoadScore();
    }

    [Serializable]
    public class SaveData
    {
        public int rank;
        public string PlayerName;
        public int score;

    }

    public void SaveScore()
    {

        //string json = JsonUtility.ToJson(data);
        string json = JsonHelper.ToJson(scoreData.ToArray(), true);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void UpdateRanking(SaveData newScore)
	{
        if (scoreData.Count > 0)
        {
            foreach (var item in scoreData)
            {
                if (newScore.score > item.score)
                {
                    scoreData.Insert(scoreData.FindIndex(x => x.Equals(item)), newScore);
                    break;
                }
            }

        }
        else
        {
            scoreData.Add(newScore);
        }

        if(newScore.score < scoreData.Last().score)
		{
            scoreData.Add(newScore);
		}

		for (int i = 0; i < scoreData.Count; i++)
		{
            scoreData[i].rank =i+1;
			if (i > 8) { scoreData.RemoveAt(i); }
        }

        SaveScore();
    }
    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            //scoreData = JsonUtility.FromJson<List<SaveData>>(json);
            string json = File.ReadAllText(path);
            SaveData[] data = JsonHelper.FromJson<SaveData>(json);
            scoreData = data.ToList();
        }
    }

    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }


}
