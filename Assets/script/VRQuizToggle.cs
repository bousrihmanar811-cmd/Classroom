using UnityEngine;
using UnityEngine.UI;

public class VRQuizToggle : MonoBehaviour
{
    public Toggle toggle;          // Le Toggle UI
    public Image background;       // L'image de fond (Background du Toggle)
    public bool isCorrectAnswer;   // Est-ce la bonne réponse ?

    private Color defaultColor = Color.white;
    private Color correctColor = Color.green;
    private Color wrongColor = Color.red;

    void Start()
    {
        if (toggle == null) toggle = GetComponent<Toggle>();
        if (background == null) background = toggle.targetGraphic as Image;

        toggle.onValueChanged.AddListener(OnToggleChanged);
    }

    void OnToggleChanged(bool isOn)
    {
        if (isOn)
        {
            if (isCorrectAnswer)
                background.color = correctColor;  // ? bonne réponse
            else
                background.color = wrongColor;    // ? mauvaise réponse
        }
        else
        {
            background.color = defaultColor;      // Retour à la couleur par défaut
        }
    }
}
