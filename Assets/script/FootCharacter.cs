using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootCharacter : MonoBehaviour
{
    public float disappearAfter = 120f; // Temps avant disparition (en secondes)

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
