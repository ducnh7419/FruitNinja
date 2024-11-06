using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{

    [SerializeField]private int scorePerFruit=10;
    private int currentScore = 0;
    private int highScore = 0;


    private void Start() {
        highScore = GetHighScore(); 
    }

    public void IncreseScore(int comboScore){
        if (comboScore <=1){
            currentScore+=scorePerFruit;
        }
        else{
            currentScore+=scorePerFruit*comboScore;
        }
    }

    public void DecreaseScore(){
        currentScore-=10;
    }

    public int GetCurrentScore() {
        return currentScore;
    }

    public int GetHighScore() {
        return PlayerPrefs.GetInt("HighScore", 0);
    }

    public void ResetScore() {
        currentScore = 0;
    }

}
