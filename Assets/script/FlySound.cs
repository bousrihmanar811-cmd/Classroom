using UnityEngine;
using System.Collections;

public class FlySound : MonoBehaviour
{
    public AudioSource audioSource;
    public float playAtSecond = 60f; // 17 minutes

    void Start()
    {
        if (audioSource != null)
        {
            audioSource.playOnAwake = false; // S'assure que ça ne joue pas au démarrage
            StartCoroutine(PlaySoundAfterDelay(playAtSecond));
        }
    }

    IEnumerator PlaySoundAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
            Debug.Log("?? Son joué à " + delay + " secondes !");
        }
    }
}
