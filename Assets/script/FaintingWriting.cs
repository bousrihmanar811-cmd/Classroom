using UnityEngine;

public class FaintingWriting : MonoBehaviour
{
    public float firstAppearTime = 90f;      // Apparition 1
    public float firstDisappearTime = 350f;  // Disparition 1
    public float secondAppearTime = 450f;    // Apparition 2
    public float secondDisappearTime = 500f; // Disparition 2

    void Start()
    {
        // Commence caché
        gameObject.SetActive(false);

        // Planifie les événements
        Invoke(nameof(Appear), firstAppearTime);
        Invoke(nameof(Disappear), firstDisappearTime);
        Invoke(nameof(Appear), secondAppearTime);
        Invoke(nameof(Disappear), secondDisappearTime);
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
