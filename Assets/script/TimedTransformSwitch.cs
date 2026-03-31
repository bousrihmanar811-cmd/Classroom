using UnityEngine;
using System.Collections;

public class TimedTransformSwitch : MonoBehaviour
{
    public Vector3 startPosition = new Vector3(-9.783139f, -6.862881f, 12.886294f);
    public Quaternion startRotation = new Quaternion(0f, -0.7071068f, 0f, 0.7071068f);

    public Vector3 targetPosition = new Vector3(-9.441130f, -6.862881f, 12.886294f);
    public Quaternion targetRotation = new Quaternion(0f, -0.7071068f, 0f, 0.7071068f);

    public float timeToMove = 580f; // seconde 580
    public float timeToReturn = 590f; // seconde 590

    private void Start()
    {
        // Assurer que l'objet démarre à la bonne position
        transform.position = startPosition;
        transform.rotation = startRotation;

        // Lancer les changements avec Invoke
        Invoke(nameof(MoveToTarget), timeToMove);
        Invoke(nameof(ReturnToStart), timeToReturn);
    }

    void MoveToTarget()
    {
        transform.position = targetPosition;
        transform.rotation = targetRotation;
    }

    void ReturnToStart()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
    }
}
