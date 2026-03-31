using UnityEngine;

public class BoxCharacter : MonoBehaviour
{
    public float disappearAfter = 90f; // Temps avant disparition (en secondes)

    void Start()
    {
        // Planifie la disparition après le temps défini
        Invoke("Disappear", disappearAfter);
    }

    void Disappear()
    {
        gameObject.SetActive(false); // Cache le personnage
    }
}
