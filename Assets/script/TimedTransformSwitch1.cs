using UnityEngine;

public class TimedTransformSwitch1 : MonoBehaviour
{
    // Position et rotation de départ
    public Vector3 startPosition = new Vector3(-9.783139f, -6.862881f, -3.820457f);
    public Quaternion startRotation = new Quaternion(0f, -0.7071068f, 0f, 0.7071068f);

    // Position et rotation cible
    public Vector3 targetPosition = new Vector3(-9.460785f, -6.862881f, -3.820457f);
    public Quaternion targetRotation = new Quaternion(0f, -0.7071068f, 0f, 0.7071068f);

    // Temps de déclenchement
    public float timeToMove = 450f; // seconde 450
    public float timeToReturn = 500f; // seconde 500

    // Start is called before the first frame update
    void Start()
    {
        // Forcer la position de départ
        transform.position = startPosition;
        transform.rotation = startRotation;

        // Planifier les changements
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
