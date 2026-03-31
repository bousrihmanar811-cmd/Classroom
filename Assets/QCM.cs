//using UnityEngine;
//using TMPro;
//using UnityEngine.UI;

//public class QCM : MonoBehaviour
//{
//    [System.Serializable]
//    public class Question
//    {
//        public string question;
//        public string[] reponses;
//        public int bonneReponse; // index 0,1,2
//    }

//    public TextMeshProUGUI questionText;
//    public TextMeshProUGUI[] reponseTexts; // 3 TMP pour réponses
//    public AudioSource audioSource;        // pour le vocal
//    public AudioClip[] feedbackVocals;     // MP3 après 5, 10, 15 questions

//    private Question[] questions;
//    private int currentQuestion = 0;
//    private int scoreJuste = 0;
//    private int scoreFaux = 0;

//    void Start()
//    {
//        // ?? Ici on écrit toutes les questions et réponses directement en C# :
//        questions = new Question[]
//        {
//            new Question {
//                question = "Quelle est la capitale de la France ?",
//                reponses = new string[] { "Berlin", "Paris", "Madrid" },
//                bonneReponse = 1
//            },
//            new Question {
//                question = "Combien font 2 + 2 ?",
//                reponses = new string[] { "3", "4", "5" },
//                bonneReponse = 1
//            },
//            new Question {
//                question = "Qui a inventé la relativité ?",
//                reponses = new string[] { "Newton", "Einstein", "Tesla" },
//                bonneReponse = 1
//            },
//            // ?? ajoute ici jusqu’à 15 questions
//        };

//        AfficherQuestion();
//    }

//    void AfficherQuestion()
//    {
//        Question q = questions[currentQuestion];
//        questionText.text = q.question;

//        for (int i = 0; i < reponseTexts.Length; i++)
//        {
//            reponseTexts[i].text = q.reponses[i];
//            int index = i; // capture locale
//            Button btn = reponseTexts[i].GetComponent<Button>();
//            btn.onClick.RemoveAllListeners();
//            btn.onClick.AddListener(() => Repondre(index));
//        }
//    }

//    void Repondre(int choix)
//    {
//        if (choix == questions[currentQuestion].bonneReponse)
//            scoreJuste++;
//        else
//            scoreFaux++;

//        currentQuestion++;

//        if (currentQuestion % 5 == 0)
//            JouerFeedback();

//        if (currentQuestion < questions.Length)
//            AfficherQuestion();
//        else
//            Debug.Log("Quiz terminé ! Justes=" + scoreJuste + " Fausses=" + scoreFaux);
//    }

//    void JouerFeedback()
//    {
//        int index = (currentQuestion / 5) - 1;
//        if (index < feedbackVocals.Length)
//        {
//            audioSource.clip = feedbackVocals[index];
//            audioSource.Play();
//        }
//        Debug.Log("Feedback vocal : " + scoreJuste + " bonnes, " + scoreFaux + " mauvaises");
//    }
//}


///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///


//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;
//using System.Collections.Generic;

//[System.Serializable]
//public class QuestionUI
//{
//    public TextMeshProUGUI questionText; // texte de la question
//    public Toggle[] answerToggles;       // 3 toggles pour les réponses
//}

//public class QCM : MonoBehaviour
//{
//    [Header("Questions UI")]
//    public List<QuestionUI> questionsUI;  // liste de 15 questions

//    [Header("Audio")]
//    public AudioSource audioSource;
//    public AudioClip vocalClip;

//    private int currentQuestion = 0;
//    private int correctCount = 0;
//    private int wrongCount = 0;

//    void Start()
//    {
//        ShowQuestion();
//    }

//    void ShowQuestion()
//    {
//        if (currentQuestion >= questionsUI.Count)
//        {
//            Debug.Log($"Quiz terminé : {correctCount} justes, {wrongCount} fausses");
//            PlayVocal();
//            return;
//        }

//        // Activer la question courante et désactiver les autres
//        for (int i = 0; i < questionsUI.Count; i++)
//            questionsUI[i].questionText.gameObject.SetActive(i == currentQuestion);

//        // Réinitialiser tous les toggles
//        foreach (var toggle in questionsUI[currentQuestion].answerToggles)
//            toggle.isOn = false;

//        // Ajouter listener aux toggles
//        for (int i = 0; i < questionsUI[currentQuestion].answerToggles.Length; i++)
//        {
//            int idx = i;
//            questionsUI[currentQuestion].answerToggles[i].onValueChanged.RemoveAllListeners();
//            questionsUI[currentQuestion].answerToggles[i].onValueChanged.AddListener((isOn) =>
//            {
//                if (isOn)
//                    OnAnswerSelected(idx);
//            });
//        }
//    }

//    void OnAnswerSelected(int index)
//    {
//        Toggle selectedToggle = questionsUI[currentQuestion].answerToggles[index];

//        if (selectedToggle.isOn) // Si le toggle est activé, considérer comme correct
//        {
//            correctCount++;
//        }
//        else
//        {
//            wrongCount++;
//        }

//        currentQuestion++;
//        ShowQuestion();
//    }

//    void PlayVocal()
//    {
//        if (audioSource != null && vocalClip != null)
//        {
//            audioSource.clip = vocalClip;
//            audioSource.Play();
//        }
//    }
//}
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

[System.Serializable]
public class QuestionUI
{
    public GameObject questionPanel;     // Panel qui contient le texte + les toggles
    public TextMeshProUGUI questionText; // Texte de la question
    public Toggle[] answerToggles;       // 3 toggles pour les réponses
    public int correctAnswerIndex;       // Index de la bonne réponse (0, 1 ou 2)
}

public class QCM : MonoBehaviour
{
    [Header("Questions UI")]
    public List<QuestionUI> questionsUI;  // Liste des questions (ex: 15)

    [Header("UI Score")]
    public GameObject scorePanel;         // Nouveau panel pour afficher le score
    public TextMeshProUGUI scoreText;     // Texte du score

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip vocalClip;

    private int currentQuestion = 0;
    private int correctCount = 0;
    private int wrongCount = 0;
    private int questionsAnswered = 0;

    void Start()
    {
        // S’assurer que le panneau score est caché au début
        if (scorePanel != null)
            scorePanel.SetActive(false);

        ShowQuestion();
    }

    void ShowQuestion()
    {
        // Si toutes les questions sont terminées
        if (currentQuestion >= questionsUI.Count)
        {
            Debug.Log($"Quiz terminé : {correctCount} justes, {wrongCount} fausses");
            PlayVocal();

            // Afficher le score final
            ShowScore(final: true);
            return;
        }

        // Activer seulement le panel de la question courante
        for (int i = 0; i < questionsUI.Count; i++)
            questionsUI[i].questionPanel.SetActive(i == currentQuestion);

        // Réinitialiser les toggles
        foreach (var toggle in questionsUI[currentQuestion].answerToggles)
        {
            toggle.isOn = false;
            toggle.interactable = true;
        }

        // Ajouter les listeners
        for (int i = 0; i < questionsUI[currentQuestion].answerToggles.Length; i++)
        {
            int idx = i;
            questionsUI[currentQuestion].answerToggles[i].onValueChanged.RemoveAllListeners();
            questionsUI[currentQuestion].answerToggles[i].onValueChanged.AddListener((isOn) =>
            {
                if (isOn)
                    OnAnswerSelected(idx);
            });
        }
    }

    void OnAnswerSelected(int index)
    {
        // Vérifier la réponse
        if (index == questionsUI[currentQuestion].correctAnswerIndex)
        {
            correctCount++;
            Debug.Log("Bonne réponse !");
        }
        else
        {
            wrongCount++;
            Debug.Log("Mauvaise réponse !");
        }

        // Désactiver les toggles
        foreach (var toggle in questionsUI[currentQuestion].answerToggles)
            toggle.interactable = false;

        questionsAnswered++;

        // Si on a répondu à 5 questions, afficher le score
        //////////////////////////////////////////////////////////////////////:::::::::::::::::::::***************************
        if (questionsAnswered % 2 == 0 && currentQuestion < questionsUI.Count - 1)
        {
            Invoke(nameof(ShowScore), 1f);
        }
        else
        {
            // Sinon passer à la question suivante
            Invoke(nameof(NextQuestion), 1f);
        }
    }

    void NextQuestion()
    {
        currentQuestion++;
        ShowQuestion();
    }

    void ShowScore(bool final = false)
    {
        // Activer le panneau de score
        if (scorePanel != null)
        {
            scorePanel.SetActive(true);

            string message = final
                ? $"?? Score final : {correctCount} bonnes réponses sur {questionsUI.Count}"
                : $"? Tu as répondu à {questionsAnswered} questions.\nScore actuel : {correctCount} justes, {wrongCount} fausses.";

            scoreText.text = message;

            // Si ce n’est pas le score final, reprendre après quelques secondes
            if (!final)
                Invoke(nameof(HideScoreAndContinue), 3f);
        }
    }

    void HideScoreAndContinue()
    {
        if (scorePanel != null)
            scorePanel.SetActive(false);

        NextQuestion();
    }

    void PlayVocal()
    {
        if (audioSource != null && vocalClip != null)
        {
            audioSource.clip = vocalClip;
            audioSource.Play();
        }
    }
}

