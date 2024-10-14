using UnityEngine;

public class NemesisAIConductor: MonoBehaviour
{ 
    private AudioSource soundController;

    private void Awake()
    {
        soundController = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(OmniConfigGovernor.PlayerCharacter))
        {
            soundController.Play();
            GetComponent<Renderer>().enabled = false; 
            GetComponent<Collider2D>().enabled = false;
            GameMastermind.singletonInstance.DeductLifeUnit(); 
            Destroy(gameObject, soundController.clip.length);
        }
    }
}