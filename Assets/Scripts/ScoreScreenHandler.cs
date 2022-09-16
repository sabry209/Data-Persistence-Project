using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreScreenHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject RowPrefab;
    public Transform rowsParent;
    public GameObject StartScreen;

    void Start()
    {
        var scoreList = DataHandler.Instance.scoreData;
		foreach (var score in scoreList)
		{
            GameObject row = Instantiate(RowPrefab, rowsParent);
            TMP_Text[] texts = row.GetComponentsInChildren<TMP_Text>();
            texts[0].text = score.rank.ToString();
            texts[1].text = score.PlayerName;
            texts[2].text = score.score.ToString();

        }

      }
    public void backtoStart()
    {
        StartScreen.SetActive(true);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
