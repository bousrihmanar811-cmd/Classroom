using UnityEngine;

public class TableShowExam : MonoBehaviour
{
    public GameObject feuilleExam;
    public GameObject[] stylos; // 3 stylos
    public Transform xrOrigin;  // référence à ton XR Origin
    public float yOffset = -0.2f; // petite translation vers le bas (assis)

    public Vector3 fixedPosition = new Vector3(0.201567277f, 0.0355445258f, 0.00253486424f);
    public float lockDelay = 60f; // après 60 secondes
    private bool hasMoved = false;
    private bool isLocked = false;

    private float timer = 0f;

    void Start()
    {
        // cacher au début
        if (feuilleExam != null)
            feuilleExam.SetActive(false);

        foreach (GameObject stylo in stylos)
            stylo.SetActive(false);
    }

    void Update()
    {
        // Compte à rebours
        timer += Time.deltaTime;

        if (!isLocked && timer >= lockDelay)
        {
            LockPlayer();
        }

        // Si verrouillé, garder la position fixe
        if (isLocked && xrOrigin != null)
        {
            xrOrigin.position = fixedPosition;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // ton XR Rig touche
        {
            if (feuilleExam != null)
                feuilleExam.SetActive(true);

            foreach (GameObject stylo in stylos)
                stylo.SetActive(true);

            // Déplacer la caméra XR Origin une seule fois (assis)
            if (!hasMoved && xrOrigin != null)
            {
                xrOrigin.position = new Vector3(
                    xrOrigin.position.x,
                    xrOrigin.position.y + yOffset,
                    xrOrigin.position.z
                );

                hasMoved = true;
            }
        }
    }

    private void LockPlayer()
    {
        if (xrOrigin != null)
        {
            xrOrigin.position = fixedPosition;
            isLocked = true;
        }
    }
}
