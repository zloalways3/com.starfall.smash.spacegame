using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class TemporalRegulator : MonoBehaviour
{
    [FormerlySerializedAs("_timerText")] [SerializeField] private TextMeshProUGUI countdownDisplay; // Текстовое поле для отображения времени
    [FormerlySerializedAs("_complite")] [SerializeField] private GameObject completionFlag;
    [SerializeField] private GameObject GameObject;
    
    private float remainingTimeValue = 60f;
    private bool timerActive = false;

    private void Start()
    {
        UpdateTimerText(); // Установить начальное значение текста
        timerActive = true;
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (timerActive)
        {
            if (remainingTimeValue > 0)
            {
                remainingTimeValue -= Time.deltaTime;
                UpdateTimerText(); // Обновить текстовое поле
            }
            else
            {
                remainingTimeValue = 0;
                timerActive = false;
                completionFlag.SetActive(true);
                GameObject.SetActive(false);
                Time.timeScale = 0f;
            }
        }
    }
    
    private void UpdateTimerText()
    {
        int minuteCounter = Mathf.FloorToInt(remainingTimeValue / 60);
        int secondCounter = Mathf.FloorToInt(remainingTimeValue % 60);
        countdownDisplay.text = string.Format("Time: " + "{0:00}:{1:00}", minuteCounter, secondCounter);
    }
}