using UnityEngine;
using UnityEngine.Serialization;

public class EntityManifestationDirector : MonoBehaviour
{
    [FormerlySerializedAs("_coinPrefab")] [SerializeField] private GameObject coinPrefabTemplate;
    [FormerlySerializedAs("_spherePrefab")] [SerializeField] private GameObject[] spherePrefabTemplate;
    private float objectSpawnDelay = 0.3f; 
    private int coinAppearanceChance = 60; 

    private void Start()
    {
        InvokeRepeating("GenerateEntity", 0f, objectSpawnDelay);
    }

    private void GenerateEntity()
    {
        int randomizerValue = Random.Range(0, 100);

        if (randomizerValue < coinAppearanceChance)
        {
            ManifestCoin();
        }
        else
        {
            ManifestSphere();
        }
    }

    private void ManifestCoin()
    {
        var currencyObject = Instantiate(coinPrefabTemplate, GenerateSpawnCoords(), Quaternion.identity);
        currencyObject.transform.parent = transform.parent;
    }

    private void ManifestSphere()
    {
        var orbObject = Instantiate(spherePrefabTemplate[Random.Range(0,spherePrefabTemplate.Length)], GenerateSpawnCoords(), Quaternion.identity);
        orbObject.transform.parent = transform.parent;
    }

    private Vector3 GenerateSpawnCoords()
    {
        float coordinateX = Random.Range(-8f, 8f);
        float coordinateY = Random.Range(-4f, 4f);
        return new Vector3(coordinateX, coordinateY, 0);
    }
}