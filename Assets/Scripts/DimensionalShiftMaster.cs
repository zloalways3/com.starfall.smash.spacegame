using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DimensionalShiftMaster : MonoBehaviour
{ 
    private void Start()
    {
        StartCoroutine(BootMainRealm(OmniConfigGovernor.MainArena));
    }

    private IEnumerator BootMainRealm(string nameScene)
    {
        AsyncOperation backgroundTaskHandler = SceneManager.LoadSceneAsync(nameScene);

        backgroundTaskHandler.allowSceneActivation = false;

        while (!backgroundTaskHandler.isDone)
        {
                
            if (backgroundTaskHandler.progress >= 0.9f)
            {
                backgroundTaskHandler.allowSceneActivation = true;
            }

            yield return null;
        }
    }

        
}