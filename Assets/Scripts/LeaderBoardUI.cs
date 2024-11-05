// using System.Collections.Generic;
// using TMPro;
// using UnityEngine;
// using UnityEngine.UI;

// public class LeaderBoardUI : MonoBehaviour
// {
//     // public TextMeshPro titleText; // Assign the "HIGHSCORES" title Text component in Unity
//     public Text[] scoreTexts; // Assign Text components for each score (5 in total here)
//     public GameObject leaderboardPanel; // Panel containing the leaderboard UI to show/hide it

//     private GameData gameData;

//     void Start()
//     {
//         LoadGameData();
//         DisplayScores();
//     }

//     void LoadGameData()
//     {
//         // Load data from Infor instance or directly from file as needed
//         string saveLocation = Application.persistentDataPath + "/gameData.json";
//         if (System.IO.File.Exists(saveLocation))
//         {
//             string dataString = System.IO.File.ReadAllText(saveLocation);
//             gameData = JsonUtility.FromJson<GameData>(dataString);
//         }
//         else
//         {
//             gameData = new GameData(); // Initialize empty data if no file is found
//         }
//     }

//     void DisplayScores()
//     {
//         // Sort the scores in descending order and take the top 5
//         List<int> sortedScores = new List<int>(gameData.scores);
//         sortedScores.Sort((a, b) => b.CompareTo(a)); // Sort descending

//         // Display the top 5 scores
//         for (int i = 0; i < scoreTexts.Length; i++)
//         {
//             if (i < sortedScores.Count)
//                 scoreTexts[i].text = sortedScores[i].ToString();
//             else
//                 scoreTexts[i].text = ""; // Clear unused score slots
//         }
//     }

//       // Assign the LeaderboardPanel GameObject in the Inspector

//     public void ShowLeaderboard()
//     {
//         leaderboardPanel.SetActive(true); // Show the leaderboard panel
//         // Optionally, update the scores here if they need to be refreshed each time
//     }

//     public void HideLeaderboard()
//     {
//         leaderboardPanel.SetActive(false); // Hide the leaderboard panel
//     }
// }

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Use if you are using TextMeshPro for text elements

public class LeaderBoardUI : MonoBehaviour
{
    public GameObject leaderboardPanel; // Assign the LeaderboardPanel GameObject
    public GameObject scoreTemplate; // Assign the ScoreTemplate GameObject in Inspector
    public Transform scoreListContainer; // Assign the ScoreListContainer in Inspector

    private GameData gameData;

    private void Start()
    {
        LoadGameData();
    }

    public void ShowLeaderboard()
    {
        leaderboardPanel.SetActive(true); // Show the leaderboard panel
        DisplayScores(); // Populate the leaderboard with scores
    }

    public void HideLeaderboard()
    {
        leaderboardPanel.SetActive(false); // Hide the leaderboard panel
        // ClearScores(); // Clear the displayed scores to avoid duplication
    }

    private void LoadGameData()
    {
        // Load GameData from file (or retrieve from Infor.cs if it’s accessible here)
        string saveLocation = Application.persistentDataPath + "/gameData.json";
        if (System.IO.File.Exists(saveLocation))
        {
            string dataString = System.IO.File.ReadAllText(saveLocation);
            gameData = JsonUtility.FromJson<GameData>(dataString);
        }
        else
        {
            gameData = new GameData();
        }
    }

    private void DisplayScores()
    {
        // Sort scores in descending order
        List<int> sortedScores = new List<int>(gameData.scores);
        sortedScores.Sort((a, b) => b.CompareTo(a));

        // Display top 5 scores (or fewer if not enough scores)
        for (int i = 0; i < Mathf.Min(5, sortedScores.Count); i++)
        {
            // Instantiate a new score entry from the template
            GameObject scoreEntry = Instantiate(scoreTemplate, scoreListContainer);
            scoreEntry.SetActive(true); // Enable the entry

            // Set the score text
            Text scoreText = scoreEntry.GetComponent<Text>();
            if (scoreText != null)
            {
                scoreText.text = (i + 1) + ". " + sortedScores[i];
            }
            else
            {
                // Use TextMeshPro if you're using TextMeshPro
                TextMeshProUGUI scoreTMP = scoreEntry.GetComponent<TextMeshProUGUI>();
                if (scoreTMP != null)
                {
                    scoreTMP.text = (i + 1) + ". " + sortedScores[i];
                }
            }
        }
    }

    // private void ClearScores()
    // {
    //     // Destroy all child objects of scoreListContainer to clear the list
    //     foreach (Transform child in scoreListContainer)
    //     {
    //         Destroy(child.gameObject);
    //     }
    // }
}
