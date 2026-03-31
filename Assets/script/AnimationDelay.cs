using UnityEngine;

public class AnimationDelay : MonoBehaviour
{
    public Animator animator; // Drag & drop the Animator component here
    public float delay = 27f;// Délai pour démarrer l'animation
    public float disappearAfter = 30f; // Temps pour faire disparaître le personnage


    void Start()
    {
        animator.enabled = false; // Stop animation at the start
        Invoke("StartAnimation", delay);// Lance l'animation après 'delay' secondes
        Invoke("DisappearCharacter", disappearAfter); // Cache le personnage après 'disappearAfter' secondes
    }

    void StartAnimation()
    {
        animator.enabled = true; // Resume animation after delay
    }
    void DisappearCharacter()
    {
        gameObject.SetActive(false); // Désactive complètement le GameObject
        // OU : pour juste le rendre invisible sans le désactiver complètement
        // GetComponent<Renderer>().enabled = false;
    }
}
