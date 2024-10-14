using UnityEngine;
using UnityEngine.Serialization;

public class GameMastermind : MonoBehaviour
{
    public static GameMastermind singletonInstance;

    private int lifeCount = 3;
        
    [FormerlySerializedAs("_livesSprites")] [SerializeField]
    private SpriteRenderer[] lifeSprites;
    [FormerlySerializedAs("_player")] [SerializeField]
    private GameObject characterController;
    [FormerlySerializedAs("_loseMenu")] [SerializeField]
    private GameObject defeatMenu;
    [FormerlySerializedAs("_gameMenu")] [SerializeField]
    private GameObject mainGameMenu;
    
    private void Awake()
    {
        singletonInstance = this;
    }

    public void DeductLifeUnit()
    {
        lifeCount--;
        lifeSprites[lifeCount].enabled = false;

        if (lifeCount <= 0)
        {
            ConcludeGameSession();
        }
    }
    
    private void ConcludeGameSession()
    {
        characterController.GetComponent<AvatarControlArchitect>().enabled = false;
        defeatMenu.SetActive(true);
        mainGameMenu.SetActive(false);
        Time.timeScale = 0;
    }
}