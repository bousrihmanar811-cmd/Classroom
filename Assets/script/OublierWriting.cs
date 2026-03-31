using UnityEngine;

public class OublierWriting : MonoBehaviour
{
    public float firstAppearTime = 70f;    // Apparition 1
    public float firstDisappearTime = 580f; // Disparition
    public float secondAppearTime = 590f;   // Apparition 2

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
