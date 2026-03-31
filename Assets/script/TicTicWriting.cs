using UnityEngine;

public class TicTicWriting : MonoBehaviour
{
    [Header("Timings (secondes)")]

    public float firstAppearTime = 190f;       // Apparition 1
    public float firstDisappearTime = 300f;    // Disparition 1

    public float secondAppearTime = 310f;      // Apparition 2
    public float secondDisappearTime = 420f;   // Disparition 2

    public float thirdAppearTime = 430f;       // Apparition 3
    public float thirdDisappearTime = 540f;    // Disparition 3

    public float fourthAppearTime = 550f;      // Apparition 4
    public float fourthDisappearTime = 660f;   // Disparition 4

    public float fifthAppearTime = 670f;       // Apparition 5
    public float fifthDisappearTime = 780f;    // Disparition 5

    public float sixthAppearTime = 790f;       // Apparition 6
    public float sixthDisappearTime = 900f;    // Disparition 6

    public float seventhAppearTime = 910f;     // Apparition 7
    //public float seventhDisappearTime = 1020f; // Disparition 7


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
        Invoke(nameof(Disappear), fourthDisappearTime);

        Invoke(nameof(Appear), fifthAppearTime);
        Invoke(nameof(Disappear), fifthDisappearTime);

        Invoke(nameof(Appear), sixthAppearTime);
        Invoke(nameof(Disappear), sixthDisappearTime);

        Invoke(nameof(Appear), seventhAppearTime);
        //Invoke(nameof(Disappear), seventhDisappearTime);
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
