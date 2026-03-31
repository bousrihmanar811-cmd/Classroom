using UnityEngine;

public class TimeWriting : MonoBehaviour
{
    [Header("Timings (secondes)")]
    public float firstAppearTime = 100f;       // Apparition 1
    public float firstDisappearTime = 290f;    // Disparition 1

    public float secondAppearTime = 300f;      // Apparition 2
    public float secondDisappearTime = 590f;   // Disparition 2

    public float thirdAppearTime = 600f;       // Apparition 3
    public float thirdDisappearTime = 890f;    // Disparition 3

    public float fourthAppearTime = 900f;      // Apparition 4
    //public float fourthDisappearTime = 1000f;  // Disparition 4

    void Start()
    {
        // Commence caché
        gameObject.SetActive(false);

        // Planifie les événements
        Invoke(nameof(Appear), firstAppearTime);
        Invoke(nameof(Disappear), firstDisappearTime);

        Invoke(nameof(Appear), secondAppearTime);
        Invoke(nameof(Disappear), secondDisappearTime);

        Invoke(nameof(Appear), thirdAppearTime);
        Invoke(nameof(Disappear), thirdDisappearTime);

        Invoke(nameof(Appear), fourthAppearTime);
        //Invoke(nameof(Disappear), fourthDisappearTime);
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
