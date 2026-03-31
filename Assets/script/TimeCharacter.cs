using UnityEngine;

public class TimeCharacter : MonoBehaviour
{
    public float disappearAfter = 100f; // Temps avant disparition (en secondes)

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
