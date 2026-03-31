using UnityEngine;

public class CharacterAppearAfter16Min : MonoBehaviour
{
    public Animator animator;               // Assigne l'Animator ici
    public float appearDelay = 960f;        // 16 minutes = 960 secondes
    public float rotationDelay = 2f;        // Temps après apparition pour faire la rotation
    public float disappearDelay = 962f;     // Temps total avant de disparaître
    public Vector3 rotationEulerAngles;     // Rotation à appliquer (en degrés)
    public Vector3 translationOffset;       // Déplacement à appliquer (ex: new Vector3(0, 0, 2))

    void Start()
    {
        gameObject.SetActive(false);        // Masquer au démarrage
        Invoke("MakeAppearAndAnimate", appearDelay);
        Invoke("DisappearCharacter", disappearDelay);
    }

    void MakeAppearAndAnimate()
    {
        gameObject.SetActive(true);         // Affiche le personnage
        animator.enabled = true;            // Active l'animation

        // Lance la rotation après quelques secondes
        Invoke("RotateCharacter", rotationDelay);
    }

    void RotateCharacter()
    {
        transform.Rotate(rotationEulerAngles); // Applique la rotation
        transform.Translate(translationOffset); // Appliquer la translation
    }
    void DisappearCharacter()
    {
        gameObject.SetActive(false);        // Masque le personnage
    }
}
