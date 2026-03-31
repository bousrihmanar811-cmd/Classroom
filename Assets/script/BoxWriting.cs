using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxWriting : MonoBehaviour
{
    public float firstAppearTime = 90f;    // Apparition 1
    public float firstDisappearTime = 300f; // Disparition
    public float secondAppearTime = 310f;   // Apparition 2

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
