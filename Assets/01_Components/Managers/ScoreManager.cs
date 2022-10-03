using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    

    public static int Score
    {
        get { return PlayerPrefs.GetInt("Score", 0); }
        set { PlayerPrefs.SetInt("Score", value); }
    }

    public static int HighScore
    {
        get { return PlayerPrefs.GetInt("HighScore", 0); }
        set { PlayerPrefs.SetInt("HighScore", value); }
    }

    public static void ScoreUpdate()
    {
        UIManager uiManager = UIManager.instance;
        Score = Player.Distance;

        if (Score>HighScore)
        {
            HighScore = Score;
            uiManager.newHighScoreText.SetActive(true);
            EventManager.Fire_OnConfettiPlay(2.3f);
            
        }
        else
            uiManager.newHighScoreText.SetActive(false);

        uiManager.scoreAmount.text = Score.ToString();
        uiManager.highScoreAmount.text = HighScore.ToString();

    }

}