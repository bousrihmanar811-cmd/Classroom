//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;
//using System.Collections;

//public class ModernBravoSystem : MonoBehaviour
//{
//    [Header("UI Elements")]
//    public Canvas worldSpaceCanvas;
//    public TextMeshProUGUI bravoText;
//    public ParticleSystem celebrationParticles;
//    public AudioSource audioSource;

//    [Header("Visual Effects")]
//    public Gradient textColorGradient;
//    public AnimationCurve scaleCurve;
//    public AnimationCurve glowCurve;

//    [Header("Audio")]
//    public AudioClip bravoSound;
//    public AudioClip successJingle;

//    [Header("Settings")]
//    public float animationDuration = 2f;
//    public float floatHeight = 0.5f;
//    public bool useHapticFeedback = true;

//    private Vector3 originalScale;
//    private Color originalColor;
//    private Camera playerCamera;

//    void Start()
//    {
//        // Trouver la caméra du joueur VR
//        playerCamera = Camera.main;
//        if (playerCamera == null)
//            playerCamera = FindObjectOfType<Camera>();

//        // Sauvegarder les valeurs originales
//        originalScale = bravoText.transform.localScale;
//        originalColor = bravoText.color;

//        // Cacher au début
//        SetVisibility(false);
//    }

//    public void ShowBravoMessage()
//    {
//        StartCoroutine(BravoSequence());
//    }

//    IEnumerator BravoSequence()
//    {
//        // 1. Positionner face au joueur
//        PositionTowardsPlayer();

//        // 2. Afficher et animer
//        SetVisibility(true);

//        // 3. Animation moderne avec plusieurs effets
//        yield return StartCoroutine(ModernBravoAnimation());

//        // 4. Fade out
//        yield return StartCoroutine(FadeOut());

//        SetVisibility(false);
//    }

//    void PositionTowardsPlayer()
//    {
//        if (playerCamera != null)
//        {
//            // Positionner devant le joueur
//            Vector3 playerPos = playerCamera.transform.position;
//            Vector3 playerForward = playerCamera.transform.forward;

//            transform.position = playerPos + playerForward * 3f + Vector3.up * floatHeight;
//            transform.LookAt(playerPos);
//            transform.Rotate(0, 180, 0); // Faire face au joueur
//        }
//    }

//    IEnumerator ModernBravoAnimation()
//    {
//        float elapsed = 0f;

//        // Messages rotatifs modernes
//        string[] messages = { "BRAVO!", "EXCELLENT!", "PARFAIT!", "INCROYABLE!" };
//        int messageIndex = 0;

//        while (elapsed < animationDuration)
//        {
//            float progress = elapsed / animationDuration;

//            // Animation d'échelle dynamique
//            float scaleMultiplier = scaleCurve.Evaluate(progress);
//            bravoText.transform.localScale = originalScale * scaleMultiplier;

//            // Gradient de couleur
//            Color currentColor = textColorGradient.Evaluate(progress);
//            bravoText.color = currentColor;

//            // Effet de lueur (outline glow)
//            float glowIntensity = glowCurve.Evaluate(progress);
//            if (bravoText.GetComponent<Outline>() != null)
//            {
//                bravoText.GetComponent<Outline>().effectColor =
//                    new Color(1f, 1f, 1f, glowIntensity);
//            }

//            // Rotation subtile
//            transform.Rotate(0, Time.deltaTime * 10f, 0);

//            // Changer de message périodiquement
//            if (Mathf.FloorToInt(progress * 4) != messageIndex && messageIndex < messages.Length - 1)
//            {
//                messageIndex++;
//                bravoText.text = messages[messageIndex];

//                // Effet de "pop" à chaque changement
//                StartCoroutine(PopEffect());

//                // Son à chaque changement
//                if (audioSource && bravoSound)
//                    audioSource.PlayOneShot(bravoSound);
//            }

//            elapsed += Time.deltaTime;
//            yield return null;
//        }

//        // Son final
//        if (audioSource && successJingle)
//            audioSource.PlayOneShot(successJingle);
//    }

//    IEnumerator PopEffect()
//    {
//        Vector3 targetScale = originalScale * 1.3f;
//        float popDuration = 0.15f;
//        float elapsed = 0f;

//        while (elapsed < popDuration)
//        {
//            float progress = elapsed / popDuration;
//            float scale = Mathf.Lerp(1f, 1.3f, Mathf.Sin(progress * Mathf.PI));
//            bravoText.transform.localScale = originalScale * scale;

//            elapsed += Time.deltaTime;
//            yield return null;
//        }

//        // Déclencher les particules
//        if (celebrationParticles)
//            celebrationParticles.Play();

//        // Feedback haptique (si disponible)
//        if (useHapticFeedback)
//            TriggerHapticFeedback();
//    }

//    IEnumerator FadeOut()
//    {
//        float fadeDuration = 0.5f;
//        float elapsed = 0f;
//        Color startColor = bravoText.color;

//        while (elapsed < fadeDuration)
//        {
//            float progress = elapsed / fadeDuration;
//            Color currentColor = Color.Lerp(startColor, Color.clear, progress);
//            bravoText.color = currentColor;

//            elapsed += Time.deltaTime;
//            yield return null;
//        }
//    }

//    void SetVisibility(bool visible)
//    {
//        worldSpaceCanvas.gameObject.SetActive(visible);
//        if (!visible)
//        {
//            // Reset values
//            bravoText.transform.localScale = originalScale;
//            bravoText.color = originalColor;
//            bravoText.text = "BRAVO!";
//        }
//    }

//    void TriggerHapticFeedback()
//    {
//        // Pour SteamVR
//#if UNITY_STANDALONE_WIN
//        // SteamVR.instance?.hmd?.TriggerHapticPulse(0, 2000);
//#endif

//        // Pour Oculus
//#if UNITY_ANDROID
//        // OVRInput.SetControllerVibration(1f, 1f, OVRInput.Controller.RTouch);
//#endif
//    }

//    // Méthodes publiques pour déclencher depuis d'autres scripts
//    public void TriggerBravo() => ShowBravoMessage();
//    public void TriggerBravoWithDelay(float delay) => StartCoroutine(TriggerWithDelay(delay));

//    IEnumerator TriggerWithDelay(float delay)
//    {
//        yield return new WaitForSeconds(delay);
//        ShowBravoMessage();
//    }
//}

//// Script séparé pour les effets visuels avancés
//[System.Serializable]
//public class ModernVFXSettings
//{
//    [Header("Holographic Effect")]
//    public bool useHolographicEffect = true;
//    public float hologramFlicker = 0.1f;

//    [Header("Neon Glow")]
//    public bool useNeonGlow = true;
//    public Color neonColor = Color.cyan;

//    [Header("Particle Effects")]
//    public GameObject confettiPrefab;
//    public GameObject sparksPrefab;
//    public int particleCount = 50;
//}
using UnityEngine;
using TMPro;

public class ModernBravoSystem : MonoBehaviour
{
    public GameObject bravoPanel;

    void Start()
    {
        bravoPanel.SetActive(false);
    }

    public void ShowBravo()
    {
        bravoPanel.SetActive(true);
        bravoPanel.transform.localScale = Vector3.zero;

        // Animation scale "pop"
        LeanTween.scale(bravoPanel, Vector3.one, 0.5f).setEaseOutBack();

        // Disparition après 3s
        LeanTween.delayedCall(3f, () => {
            LeanTween.scale(bravoPanel, Vector3.zero, 0.5f).setEaseInBack()
            .setOnComplete(() => bravoPanel.SetActive(false));
        });
    }
}
