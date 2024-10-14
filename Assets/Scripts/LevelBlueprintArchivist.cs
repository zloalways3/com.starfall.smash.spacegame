using UnityEngine.Serialization;
using UnityEngine.UI;

[System.Serializable]
public class LevelBlueprintArchivist
{
    [FormerlySerializedAs("stars")] public Image[] stageStars;
}