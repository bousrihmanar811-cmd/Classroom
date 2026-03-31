using UnityEngine;

public class HorlogeSimulee : MonoBehaviour
{
    [Header("Aiguilles")]
    public Transform hourHand;    // Aiguille des heures
    public Transform minuteHand;  // Aiguille des minutes
    public Transform secondHand;  // Aiguille des secondes

    [Header("Durée simulée")]
    public float simulatedMinutes = 20f; // 20 minutes dans le jeu
    public float realTimeDuration = 120f; // 120 secondes réelles = 20 min en jeu (accélération)

    private float timeElapsed;

    void Update()
    {
        // Avancer le temps simulé
        timeElapsed += Time.deltaTime;

        // Conversion : combien de minutes de jeu sont passées
        float simulatedTime = (timeElapsed / realTimeDuration) * simulatedMinutes;

        // Calcul des positions
        float seconds = (simulatedTime * 60f) % 60f;
        float minutes = simulatedTime % 60f;
        float hours = (simulatedTime / 60f) % 12f;

        // Rotation des aiguilles (Z négatif = sens horaire)
        if (secondHand != null)
            secondHand.localRotation = Quaternion.Euler(0f, 0f, -seconds * 6f);

        if (minuteHand != null)
            minuteHand.localRotation = Quaternion.Euler(0f, 0f, -minutes * 6f);

        if (hourHand != null)
            hourHand.localRotation = Quaternion.Euler(0f, 0f, -hours * 30f);
    }
}
