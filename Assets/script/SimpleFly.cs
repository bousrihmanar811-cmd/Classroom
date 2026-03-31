using UnityEngine;

public class SimpleFly : MonoBehaviour
{
    public float flySpeed = 0.1f;         // vitesse d'avancement
    public float floatAmplitude = 0.5f;   // amplitude du mouvement haut/bas
    public float floatFrequency = 2f;     // vitesse du mouvement haut/bas
    public float startDelay = 60f;        // délai avant de commencer à voler
    public float lifeTime = 137f;         // durée avant disparition

    private Vector3 startPos;
    private bool canFly = false;

    void Start()
    {
        startPos = transform.position;

        // Commence à voler après startDelay secondes
        Invoke(nameof(StartFlying), startDelay);

        // Fait disparaître l'objet après lifeTime secondes
        Invoke(nameof(Disappear), lifeTime);
    }

    void StartFlying()
    {
        canFly = true;
    }

    void Disappear()
    {
        gameObject.SetActive(false); // L'objet disparaît
        // ou Destroy(gameObject); pour supprimer complètement
    }

    void Update()
    {
        if (!canFly) return;

        // Avance vers l’avant
        transform.Translate(Vector3.forward * flySpeed * Time.deltaTime);

        // Mouvement haut/bas
        float newY = startPos.y + Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
