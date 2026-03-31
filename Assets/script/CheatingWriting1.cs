using UnityEngine;

public class CheatingWriting1 : MonoBehaviour
{
    public float firstAppearTime = 100f;    // Apparition 1
    public float firstDisappearTime = 200f; // Disparition
    public float secondAppearTime = 350f;   // Apparition 2

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
