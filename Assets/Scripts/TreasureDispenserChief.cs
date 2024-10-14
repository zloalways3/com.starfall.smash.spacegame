using UnityEngine;
using UnityEngine.Serialization;

public class TreasureDispenserChief : MonoBehaviour
{
    [FormerlySerializedAs("_audioSource")] [SerializeField] private AudioSource audioChannel;
    
    private void Awake()
    {
        audioChannel = GetComponent<AudioSource>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(OmniConfigGovernor.PlayerCharacter))
        {
            audioChannel.Play();
            ScoreSynthesizer scoreTracker = collision.GetComponent<ScoreSynthesizer>();
            if (scoreTracker != null)
            {
                scoreTracker.AllocateRewardPoints(1);
                GetComponent<Renderer>().enabled = false; 
                GetComponent<Collider2D>().enabled = false;
                Destroy(gameObject, audioChannel.clip.length);
            }
        }
    }
}