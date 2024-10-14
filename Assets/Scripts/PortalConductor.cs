using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalConductor : MonoBehaviour
{
    public void ShowStartupSequence()
    {
        SceneManager.LoadScene(OmniConfigGovernor.SceneLoadingText);
    }
    public void InitializeSceneTransition()
    {
        SceneManager.LoadScene(OmniConfigGovernor.GameplayScene);
    }
        
}