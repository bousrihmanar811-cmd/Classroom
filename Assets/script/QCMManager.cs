using UnityEngine;
using UnityEngine.UI;

public class QCMManager : MonoBehaviour
{
    public ToggleGroup toggleGroup;         // Assigne ton ToggleGroup ici
    public Color correctColor = Color.green;
    public Color wrongColor = Color.red;
    public Color defaultColor = Color.white;

    [System.Serializable]
    public class Answer
    {
        public Toggle toggle;
        public bool isCorrect;
        public Image feedbackImage; // le Background du Toggle
    }

    public Answer[] answers; // Mets tes 3 propositions ici dans l’Inspector

    void Start()
    {
        foreach (var ans in answers)
        {
            ans.toggle.onValueChanged.AddListener(delegate { OnAnswerSelected(ans); });
        }
    }

    void OnAnswerSelected(Answer selectedAnswer)
    {
        if (selectedAnswer.toggle.isOn)
        {
            // Colorer la réponse choisie
            selectedAnswer.feedbackImage.color = selectedAnswer.isCorrect ? correctColor : wrongColor;

            // Remettre les autres en blanc
            foreach (var ans in answers)
            {
                if (ans != selectedAnswer)
                    ans.feedbackImage.color = defaultColor;
            }
        }
    }
}
