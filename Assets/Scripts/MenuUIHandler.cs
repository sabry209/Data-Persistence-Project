using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]

public class MenuUIHandler : MonoBehaviour
{
    private DataHandler.SaveData newplayer;
    public GameObject LeaderBoard;
    public GameObject Settings;

    private void Start()
    {

    }

    public void StartNew()
    {
        string playernameinput = GetComponentsInChildren<TMP_InputField>()[0].text;

        newplayer = new DataHandler.SaveData();
        newplayer.PlayerName = playernameinput;
        newplayer.score = 0;
        newplayer.rank = 0;

        DataHandler.Instance.newplayer = newplayer;
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
	{
        SceneManager.LoadScene(0);

    }

    public void ShowScores()
    {
        LeaderBoard.SetActive(true);
        gameObject.SetActive(false);

    }

    public void ShowSettings()
    {
        Settings.SetActive(true);
        gameObject.SetActive(false);

    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
}