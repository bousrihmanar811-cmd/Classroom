//using UnityEngine;
//using TMPro;

//public class DelayedTexts : MonoBehaviour
//{
//    public TextMeshProUGUI text1;  // Premier texte
//    public TextMeshProUGUI text2;  // Deuxième texte

//    void Start()
//    {
//        // Cacher les textes au début
//        text1.gameObject.SetActive(false);
//        text2.gameObject.SetActive(false);

//        // Lancer les apparitions
//        Invoke(nameof(ShowText1), 70f);   // après 70s
//        Invoke(nameof(ShowText2), 130f);  // après 130s
//    }

//    void ShowText1()
//    {
//        text1.gameObject.SetActive(true);
//    }

//    void ShowText2()
//    {
//        text2.gameObject.SetActive(true);
//    }
//}

using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class TimedTextManager : MonoBehaviour
{
    public TextMeshProUGUI text1;   // Premier texte TMP
    public TextMeshProUGUI text2;   // Deuxième texte TMP
    public Image timerImage;        // Image du timer (par ex. un chrono)
    public TextMeshProUGUI timerText; // (optionnel) texte qui affiche le temps

    private float elapsedTime = 0f;
    private bool text1Shown = false;
    private bool text2Shown = false;

    void Start()
    {
        // Cacher tout au début
        text1.gameObject.SetActive(false);
        text2.gameObject.SetActive(false);
        timerImage.gameObject.SetActive(false);
        if (timerText != null)
            timerText.gameObject.SetActive(false);

        // Lancer le suivi du temps
        StartCoroutine(ShowTextsSequence());
    }

    void Update()
    {
        // Chronomètre
        elapsedTime += Time.deltaTime;

        if (timerText != null && timerText.gameObject.activeSelf)
        {
            int minutes = Mathf.FloorToInt(elapsedTime / 60f);
            int seconds = Mathf.FloorToInt(elapsedTime % 60f);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    IEnumerator ShowTextsSequence()
    {
        // Attendre 70 secondes
        yield return new WaitForSeconds(70f);

        // Afficher le timer et Text1
        timerImage.gameObject.SetActive(true);
        if (timerText != null) timerText.gameObject.SetActive(true);
        text1.gameObject.SetActive(true);
        text1Shown = true;

        // Attendre encore 60 secondes (total = 130s)
        yield return new WaitForSeconds(60f);

        // Cacher Text1, afficher Text2
        text1.gameObject.SetActive(false);
        text2.gameObject.SetActive(true);
        text2Shown = true;
    }
}
