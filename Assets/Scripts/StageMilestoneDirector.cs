using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class StageMilestoneDirector : MonoBehaviour
{
    public static StageMilestoneDirector globalInstance;

    [FormerlySerializedAs("levelOptionButtons")] [SerializeField] private GameObject[] stageSelectionButtons;
    [FormerlySerializedAs("_levels")] [SerializeField] private LevelBlueprintArchivist[] stageList;
    [FormerlySerializedAs("chooseButton")] [SerializeField] private Button selectionButton;
    [FormerlySerializedAs("earnedStarIcon")] [SerializeField] private Sprite earnedStarSprite;
    [FormerlySerializedAs("_emptyStarSprite")] [SerializeField] private Sprite emptyStarBadge;
    [SerializeField] private Sprite _lockedLevelIcon;  // Иконка для закрытого уровня
    [SerializeField] private Sprite _completedLevelIcon;  // Иконка для завершенного уровня
    [SerializeField] private TextMeshProUGUI[] levelTexts;  // Массив с текстами уровней
    [SerializeField] private Sprite openLevelIcon;

    private int totalStageCount = 18;
    private int selectedStageIndex;

    void Start()
    {
            
        if (globalInstance == null)
        {
            globalInstance = this;
            DontDestroyOnLoad(gameObject);
        }

        LaunchStageInitialization();
        RedrawStageControls(levelTexts);  // Передаем тексты уровней в метод
        selectionButton.interactable = false;
    }

    private void LaunchStageInitialization()
    {
        // Проверяем, была ли уже инициализация ранее
        if (!PlayerPrefs.HasKey(OmniConfigGovernor.ProgressionRank + 0))
        {
            // Если ключи не установлены, инициализируем прогресс
            for (int loopIteration = 1; loopIteration < totalStageCount; loopIteration++)
            {
                PlayerPrefs.SetInt(OmniConfigGovernor.ProgressionRank + loopIteration, 0);  // Все уровни заблокированы
                PlayerPrefs.SetInt("StageCompleted" + loopIteration, 0);
                PlayerPrefs.SetInt("StagePoints" + loopIteration, 0);
            }

            // Открываем только первый уровень, если инициализация происходит впервые
            PlayerPrefs.SetInt(OmniConfigGovernor.ProgressionRank + 0, 1);  // Первый уровень открыт
            PlayerPrefs.Save();
        }
    }


    // Передаем тексты уровней как параметр
    private void RedrawStageControls(TextMeshProUGUI[] levelTexts)
    {
        for (int stepCounter = 0; stepCounter < totalStageCount; stepCounter++)
        {
            if (stageSelectionButtons[stepCounter] != null)
            {
                Button levelButton = stageSelectionButtons[stepCounter].GetComponent<Button>();
                TextMeshProUGUI levelText = levelTexts[stepCounter];
                Image levelImage = stageSelectionButtons[stepCounter].GetComponent<Image>();

                // Проверяем, открыт ли уровень
                int levelStatus = PlayerPrefs.GetInt(OmniConfigGovernor.ProgressionRank + stepCounter, 0);

                if (levelStatus == 1)
                {
                    levelButton.interactable = true;
                    levelText.gameObject.SetActive(true);  // Показываем текст для открытого уровня

                    // Проверяем, завершен ли уровень
                    if (PlayerPrefs.GetInt("StageCompleted" + stepCounter, 0) == 1)
                    {
                        levelImage.sprite = _completedLevelIcon;  // Устанавливаем иконку завершенного уровня
                    }
                    else
                    {
                        levelImage.sprite = openLevelIcon;  // Иконка открытого уровня
                    }

                    int playerScore = PlayerPrefs.GetInt("StagePoints" + stepCounter, 0);
                    RebuildStarCounter(stepCounter, playerScore);
                }
                else
                {
                    // Уровень закрыт
                    levelButton.interactable = false;
                    levelText.gameObject.SetActive(false);  // Прячем текст для закрытого уровня
                    levelImage.sprite = _lockedLevelIcon;   // Устанавливаем иконку закрытого уровня
                }
            }
        }
    }



    private void RebuildStarCounter(int levelIndex, int score)
    {
        int awardedStars = 0;

        // Логика определения количества звезд для каждого уровня должна быть на основе его собственного результата
        if (score >= levelIndex*6)
        {
            awardedStars = 3;
        }
        else if (score >= Math.Round((decimal)(levelIndex * 6) / 2))
        {
            awardedStars = 2;
        }
        else if (score >= levelIndex)
        {
            awardedStars = 1;
        }

        // Обновляем звезды только для текущего уровня
        for (int currentStarIndex = 0; currentStarIndex < 3; currentStarIndex++)
        {
            if (stageList[levelIndex].stageStars[currentStarIndex] != null)
            {
                if (currentStarIndex < awardedStars)
                {
                    stageList[levelIndex].stageStars[currentStarIndex].sprite = earnedStarSprite;
                }
                else
                {
                    stageList[levelIndex].stageStars[currentStarIndex].sprite = emptyStarBadge;
                }
            }
        }
    }


    public void LoadingLevelsButton(int levelIndex)
    {
        // Проверяем, открыт ли выбранный уровень
        if (PlayerPrefs.GetInt(OmniConfigGovernor.ProgressionRank + levelIndex, 0) == 1)
        {
            selectedStageIndex = levelIndex;  // Устанавливаем выбранный уровень
            selectionButton.interactable = true;  // Активируем кнопку старта
        }
    }

    public void StartsButtonGame()
    {
        // Загружаем выбранный уровень, если он был корректно выбран
        if (selectedStageIndex != -1)
        {
            PlayerPrefs.SetInt(OmniConfigGovernor.ActiveQuestIndex, selectedStageIndex);
            PlayerPrefs.Save();
            SceneManager.LoadScene(OmniConfigGovernor.GameplayScene);
        }
    }

    public void WinLevelCompletion(int levelIndex, int score)
    {
        // Сохраняем информацию о завершении текущего уровня и его результатах
        PlayerPrefs.SetInt("StageCompleted" + levelIndex, 1);  // Уровень завершен
        PlayerPrefs.SetInt("StagePoints" + levelIndex, score);
        PlayerPrefs.Save();

        RebuildStarCounter(levelIndex, score);
            
        if (levelIndex < totalStageCount - 1)
        {
            int nextLevelIndex = levelIndex + 1;
                
            if (PlayerPrefs.GetInt(OmniConfigGovernor.ProgressionRank + nextLevelIndex, 0) == 0)
            {
                PlayerPrefs.SetInt(OmniConfigGovernor.ProgressionRank + nextLevelIndex, 1);

            }
        }
    }


}