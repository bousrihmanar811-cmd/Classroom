using UnityEngine;

public class AnotherCharacterAppear : MonoBehaviour
{
    public Animator animator;               // Assigne ici l'Animator de ce personnage
    public float appearTime = 962f;         // Temps d'apparition = 965 secondes
    public Vector3 rotationEulerAngles;
    public float rotationDelay = 2f;
    public Vector3 translationOffset;       // Déplacement à appliquer (ex: new Vector3(0, 0, 2))
    public float disappearDelay = 965f;

    void Start()
    {
        gameObject.SetActive(false);        // Masquer au démarrage
        Invoke("AppearAndAnimate", appearTime);
        Invoke("DisappearCharacter", disappearDelay);
    }

    void AppearAndAnimate()
    {
        gameObject.SetActive(true);         // Affiche le personnage
        animator.enabled = true;            // Active l'animation
        Invoke("RotaterCharacter", rotationDelay);
    }
    void RotaterCharacter()
    {
        transform.Rotate(rotationEulerAngles); // Applique la rotation
        transform.Translate(translationOffset); // Appliquer la translation
    }
    void DisappearCharacter()
    {
        gameObject.SetActive(false);        // Masque le personnage
    }
}



