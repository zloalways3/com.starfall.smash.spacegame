using UnityEngine;
using UnityEngine.Serialization;


public class RealityFlowCoordinator : MonoBehaviour
{
    [FormerlySerializedAs("_scoreController")] [SerializeField] private ScoreSynthesizer scoreSynthesizer;
    private int currentStageIDGaming;
        
    private void Start()
    {
        currentStageIDGaming = PlayerPrefs.GetInt(OmniConfigGovernor.ActiveQuestIndex, 0);
    }

    public void GameOnSceleTime()
    {
        Time.timeScale = 1f;
    }
        
    public void GameOffSceleTime()
    {
        Time.timeScale = 0f;
    }

    public void ClaimVictory()
    {
            
        PlayerPrefs.SetInt(OmniConfigGovernor.ActiveQuestIndex, currentStageIDGaming+1);
        PlayerPrefs.Save();
        StageMilestoneDirector stageMilestoneDirector = FindObjectOfType<StageMilestoneDirector>();
        stageMilestoneDirector.WinLevelCompletion(currentStageIDGaming, scoreSynthesizer.GetPointsGame());
            
    }
}