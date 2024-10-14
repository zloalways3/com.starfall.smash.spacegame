using UnityEngine;
using UnityEngine.UI;

public class ApplyBackground : MonoBehaviour
{
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Sprite[] backgroundOptions;

    void Start()
    {
        if (PlayerPrefs.HasKey("SelectedBackground"))
        {
            int savedBackgroundIndex = PlayerPrefs.GetInt("SelectedBackground");
            backgroundImage.sprite = backgroundOptions[savedBackgroundIndex];
        }
    }
}