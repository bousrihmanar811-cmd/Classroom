using System.Collections;
using UnityEngine;

public class TicTicCharacter : MonoBehaviour
{
    public float disappearAfter = 180f; // Temps avant disparition (en secondes)

    void OnEnable()
    {
        // Quand ce personnage est activé, il se cache après un délai
        Invoke("Disappear", disappearAfter);
    }

    void OnDisable()
    {
        // Annule les invocations si jamais on désactive prématurément
        CancelInvoke("Disappear");
    }

    void Disappear()
    {
        gameObject.SetActive(false); // Cache le personnage
    }
}
