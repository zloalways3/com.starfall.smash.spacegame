using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ScoreSynthesizer : MonoBehaviour
{
    [FormerlySerializedAs("_scoreText")] [SerializeField] private TextMeshProUGUI scoreDisplayTextGame;
    [FormerlySerializedAs("_complite")] [SerializeField] private GameObject taskCompletedFlag;
    [SerializeField] private GameObject GameObject;
    [SerializeField] private GameObject[] _starsGameComplate;
    private int rewardPoints = 0;
    
    
    private void Start()
    {
        SyncScoreDisplay();
    }

    public int GetPointsGame()
    {
        return rewardPoints;
    }
    
    public void AllocateRewardPoints(int amount)
    {
        rewardPoints += amount;
        SyncScoreDisplay();
    }

    private void SyncScoreDisplay()
    {
        var abc = PlayerPrefs.GetInt(OmniConfigGovernor.ActiveQuestIndex, 0);
        int pointsToAward = 0;
        if (rewardPoints >= abc*6)
        {
            pointsToAward = 3;
        }
        else if (rewardPoints >= Math.Round((decimal)(abc * 6) / 2))
        {
            pointsToAward = 2;
        }
        else if (rewardPoints >= abc)
        {
            pointsToAward = 1;
        }

        for (int counterGizmer = 0; counterGizmer < pointsToAward; counterGizmer++)
        {
            _starsGameComplate[counterGizmer].SetActive(true);
        }
        
        if (abc==0)
        {
            abc = 1;
        }
        if (scoreDisplayTextGame != null)
        {
            scoreDisplayTextGame.text = ($"COINS: {rewardPoints.ToString()}/{abc * 6}");
        }

        
        if (rewardPoints >= abc * 6)
        {
            taskCompletedFlag.SetActive(true);
            GameObject.SetActive(false);
            Time.timeScale = 0f;
        }
    }
}