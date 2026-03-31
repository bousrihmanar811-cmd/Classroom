using UnityEngine;

public class LaughingWriting : MonoBehaviour
{
    public float firstAppearTime = 70f;    // Apparition 1
    public float firstDisappearTime = 450f; // Disparition
    public float secondAppearTime = 500f;   // Apparition 2

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
