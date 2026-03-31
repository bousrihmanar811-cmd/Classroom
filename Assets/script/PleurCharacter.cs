using UnityEngine;

public class PleurCharacter : MonoBehaviour
{
    public float disappearAfter = 80f; // Temps avant disparition (en secondes)

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
