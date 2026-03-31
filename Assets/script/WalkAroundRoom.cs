using UnityEngine;

public class WalkAroundRoom : MonoBehaviour
{
    public float speed = 1f;
    public Transform[] waypoints;
    private int currentIndex = 0;

    private Animator animator;
    private bool hasFinished = false;
    [Header("Rotation finale")]
    public Vector3 finalRotationEuler = new Vector3(0, 180, 0); // la rotation souhaitée
    public float rotationSpeed = 2f; // vitesse de rotation
    private bool rotatingAtEnd = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator != null)
            animator.SetBool("isWalking", true);
    }

    void Update()
    {
        if (waypoints.Length == 0 || hasFinished) return;

        Transform target = waypoints[currentIndex];
        Vector3 direction = target.position - transform.position;
        direction.y = 0;

        // Avancer
        transform.position += direction.normalized * speed * Time.deltaTime;

        // Rotation
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(direction),
                5f * Time.deltaTime

            );
            
        }
       

        // Vérifier si arrivé
        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            if (currentIndex < waypoints.Length - 1)
            {
                currentIndex++;
            }
            else
            {
                // On s’assure que le perso s’arrête pile sur le dernier waypoint
                transform.position = target.position;

                // Stop le mouvement
                hasFinished = true;

                // Jouer Idle
                if (animator != null)
                    //animator.SetBool("isWalking", false);
                    animator.enabled = false;

                rotatingAtEnd = true;
            }
        }
        // Rotation finale après avoir fini de marcher
        if (rotatingAtEnd)
        {
            Quaternion targetRotation = Quaternion.Euler(finalRotationEuler);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
            // Vérifier si la rotation est presque terminée
            if (Quaternion.Angle(transform.rotation, targetRotation) < 1f)
            {
                rotatingAtEnd = false; // fini
            }
        }
    }
    }
