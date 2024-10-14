using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BackgroundPicker : MonoBehaviour
{
    [SerializeField] private Image previewImage;  // Image для отображения превью
    [SerializeField] private Image backgroundImage;  // Image для установки заднего фона на сцене
    [SerializeField] private Sprite[] previewOptions;  // Массив для картинок превью
    [SerializeField] private Sprite[] backgroundOptions;  // Массив для задних фонов
    [SerializeField] private Button nextButton;  // Кнопка "Next" для пролистывания вперед
    [SerializeField] private Button prevButton;  // Кнопка "Previous" для пролистывания назад
    [FormerlySerializedAs("saveButton")] [SerializeField] private Button gameSaveButton;  // Кнопка "Save" для подтверждения выбора

    private int currentBackgroundIndex = 0;  // Индекс текущего фона

    void Start()
    {
        // Загружаем последний сохраненный фон, если он есть
        if (PlayerPrefs.HasKey("SelectedBackground"))
        {
            currentBackgroundIndex = PlayerPrefs.GetInt("SelectedBackground");
        }

        UpdatePreview();  // Обновляем отображение превью

        // Привязываем методы к кнопкам
        nextButton.onClick.AddListener(NextBackground);
        prevButton.onClick.AddListener(PrevBackground);
        gameSaveButton.onClick.AddListener(SaveBackground);
    }

    // Метод для пролистывания фонов вперед
    private void NextBackground()
    {
        currentBackgroundIndex = (currentBackgroundIndex + 1) % previewOptions.Length;
        UpdatePreview();
    }

    // Метод для пролистывания фонов назад
    private void PrevBackground()
    {
        currentBackgroundIndex = (currentBackgroundIndex - 1 + previewOptions.Length) % previewOptions.Length;
        UpdatePreview();
    }

    // Обновляем previewImage для отображения выбранного превью
    private void UpdatePreview()
    {
        previewImage.sprite = previewOptions[currentBackgroundIndex];
    }

    // Сохраняем выбранный фон и обновляем фон на сцене
    private void SaveBackground()
    {
        PlayerPrefs.SetInt("SelectedBackground", currentBackgroundIndex);
        PlayerPrefs.Save();

        // Применяем соответствующее изображение из массива backgroundOptions как задний фон
        backgroundImage.sprite = backgroundOptions[currentBackgroundIndex];

        Debug.Log("Фон сохранен: " + currentBackgroundIndex);
    }
}
