using UnityEngine;
public class DelayedAnimation : MonoBehaviour
{
    public Animator animator;         // L'Animator du personnage
    public string animationTrigger;   // Le nom du Trigger dans l'Animator
    public float delay = 30f;         // Temps d'attente (30 secondes par défaut)
    private void Start()
    {
        // Lancer la coroutine au démarrage
        StartCoroutine(PlayAnimationAfterDelay());
        if (animator != null)
            animator.enabled = false; // Empêcher de jouer dès le début

        StartCoroutine(PlayAnimationAfterDelay());
    }
    private System.Collections.IEnumerator PlayAnimationAfterDelay()
    {
        // Attendre X secondes
        yield return new WaitForSeconds(delay);

        // Lancer l'animation
        if (animator != null)
        {
            animator.enabled = true;             // Réactiver l’Animator
            animator.SetTrigger(animationTrigger); // Jouer l’anim
        }
    }
}