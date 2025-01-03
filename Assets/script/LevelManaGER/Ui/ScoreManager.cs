using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class ScoreManager : MonoBehaviour
{
    public List<GameObject> PlayerList;
    public List<TextMeshProUGUI> PlayerNameList,PlayerScoreList;
    public Gamestate gamestate;
    public GameObject Player1,Player2,Player3;
    public bool IsgameStart = false;
    public string PlayerName1, PlayerName2, PlayerName3;
    public TMP_InputField PlayerNameInput1, PlayerNameInput2, PlayerNameInput3;
    public int Player1Score = 0 , Player2Score = 0, Player3Score = 0;
    // Start is called before the first frame update
    void Start()
    {
        gamestate = GameObject.Find("LevelManager").GetComponent<Gamestate>();
        PlayerName1 = PlayerPrefs.GetString("Player1");
        PlayerName2 = PlayerPrefs.GetString("Player2");
        PlayerName3 = PlayerPrefs.GetString("Player3");
        
    }

    // Update is called once per frame
    void Update()
    {
        if(IsgameStart == true)
        {
            PlayerList = gamestate.PlayerList;
            Player1 = PlayerList[0];
            Player2 = PlayerList[1];
            Player3 = PlayerList[2];
            InputPlayerShowname();
            PlayerScoreList[0].text = Player1Score.ToString();
            PlayerScoreList[1].text = Player2Score.ToString();
            PlayerScoreList[2].text = Player3Score.ToString();

        }
    }
    public void StartScoring()
    {
        IsgameStart = true; 
        // PlayerName1 = PlayerNameInput1.text;
        // PlayerPrefs.SetString("Player1", PlayerNameInput1.text);
        // PlayerName2 = PlayerNameInput2.text;
        // PlayerPrefs.SetString("Player2", PlayerNameInput2.text);
        // PlayerName3 = PlayerNameInput3.text;
        // PlayerPrefs.SetString("Player3", PlayerNameInput3.text);

        // Check for Player 1 input
        if (string.IsNullOrWhiteSpace(PlayerNameInput1.text))
        {
            PlayerName1 = "Player1"; // Default name
        }
        else
        {
            PlayerName1 = PlayerNameInput1.text;
        }
        PlayerPrefs.SetString("Player1", PlayerName1);

        // Check for Player 2 input
        if (string.IsNullOrWhiteSpace(PlayerNameInput2.text))
        {
            PlayerName2 = "Player2";
        }
        else
        {
            PlayerName2 = PlayerNameInput2.text;
        }
        PlayerPrefs.SetString("Player2", PlayerName2);

        // Check for Player 3 input
        if (string.IsNullOrWhiteSpace(PlayerNameInput3.text))
        {
            PlayerName3 = "Player3";
        }
        else
        {
            PlayerName3 = PlayerNameInput3.text;
        }
        PlayerPrefs.SetString("Player3", PlayerName3);

        PlayerPrefs.Save();
    }
    public void InputPlayerShowname()
    {
        
            PlayerNameList[0].text = PlayerName1;
            PlayerNameList[1].text = PlayerName2;
            PlayerNameList[2].text = PlayerName3;
         
    }
}
