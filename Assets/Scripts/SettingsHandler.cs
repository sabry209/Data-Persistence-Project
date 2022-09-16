using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SettingsHandler : MonoBehaviour
{
    public int difficulty = 1;
    public int layers = 4;
    public GameObject chooseDifficulty;
    public GameObject MainMenu;
    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void backToMain()
	{
        MainMenu.SetActive(true);
        gameObject.SetActive(false);
	}
    public void changeDifficulty()
	{
        difficulty += 1;
        if(difficulty > 3) { difficulty = 1; }

		switch (difficulty)
		{
            case 1:
                chooseDifficulty.GetComponentInChildren<TMP_Text>().text = "< EASY >";
                break;
            case 2:
                chooseDifficulty.GetComponentInChildren<TMP_Text>().text = "< NORM >";
                break;
            case 3:
                chooseDifficulty.GetComponentInChildren<TMP_Text>().text = "< HARD >";
                break;
            default:
                chooseDifficulty.GetComponent<TextMeshPro>().text = "EASY";
                break;
        }

        DataHandler.Instance.Difficulty = difficulty;
    }
}
