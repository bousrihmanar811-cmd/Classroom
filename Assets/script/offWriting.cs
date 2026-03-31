using UnityEngine;

public class offWriting : MonoBehaviour
{
    public float firstAppearTime = 80f;    // Apparition 1
    public float firstDisappearTime = 400f; // Disparition
    public float secondAppearTime = 410f;   // Apparition 2

    void Start()
    {
        // Commence caché
        gameObject.SetActive(false);

        // Planifie les événements
        Invoke(nameof(Appear), firstAppearTime);
        Invoke(nameof(Disappear), firstDisappearTime);
        Invoke(nameof(Appear), secondAppearTime);
    }

    void Appear()
    {
        gameObject.SetActive(true);
    }

    void Disappear()
    {
        gameObject.SetActive(false);
    }
}
