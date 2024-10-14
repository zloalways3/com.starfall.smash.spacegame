using UnityEngine;

public class FinalGameOverseer : MonoBehaviour
{ 
    public void TerminateGameFlow()
    {
#if UNITY_EDITOR
        
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}