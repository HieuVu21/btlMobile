using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Infor : MonoBehaviour
{
    public static Infor instance;

    public Image fireRateSlider;
    public Text timerDisplay;
    public GameObject[] ammo;

    public Text scoreText;
    public Text highScoreText;

    private bool isNewHighscore;
    private string saveLocation;
    private string dataString;
    private GameData gameData;
    private bool hasRecordedScore; // Flag to ensure score is recorded once
    public bool IsNewHighscore { get => isNewHighscore; set => isNewHighscore = value; }

    public void Awake()
    {
        instance = this;
        IsNewHighscore = false;
        saveLocation = Application.persistentDataPath + "/gameData.json";

        if (File.Exists(saveLocation))
        {
            dataString = File.ReadAllText(saveLocation);
            gameData = JsonUtility.FromJson<GameData>(dataString);
        }
        else
        {
            gameData = new GameData();
            SaveGameData(); // Save initial data if file doesn't exist
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        highScoreText.text = gameData.highScore.ToString();
        hasRecordedScore = false; // Initialize the flag as false
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = GameManger.instance.Score.ToString();

        // Check for high score
        if (GameManger.instance.Score > gameData.highScore)
        {
            IsNewHighscore = true;
            gameData.isNewLoad = false;
            gameData.highScore = GameManger.instance.Score;
            highScoreText.text = gameData.highScore.ToString();
        }

        // Save the score once when the game is over
        if (GameManger.instance.IsGameover && !hasRecordedScore)
        {
            RecordScore(GameManger.instance.Score);
            hasRecordedScore = true; // Prevent duplicate score entries
        }
    }

    private void RecordScore(int score)
    {
        // Add the last score to the scores list
        gameData.scores.Add(score);
        SaveGameData(); // Save the updated data to the file
    }

    private void SaveGameData()
    {
        // Serialize the GameData object and save it to the file
        dataString = JsonUtility.ToJson(gameData);
        File.WriteAllText(saveLocation, dataString);
    }

    public void ReloadAmmo()
    {
        for (int i = 0; i < ammo.Length; i++)
        {
            ammo[i].SetActive(true);
        }
    }
}
