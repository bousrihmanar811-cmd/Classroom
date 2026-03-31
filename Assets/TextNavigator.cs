using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextNavigator : MonoBehaviour
{
    public TextMeshProUGUI[] textes;  // liste de textes à afficher
    public Button nextButton;
    public Button backButton;

    private int index = 0;

    void Start()
    {
        // désactive tous les textes sauf le premier
        for (int i = 0; i < textes.Length; i++)
            textes[i].gameObject.SetActive(i == 0);

        // ajoute les fonctions aux boutons
        nextButton.onClick.AddListener(NextText);
        backButton.onClick.AddListener(PreviousText);

        UpdateButtons();
    }

    void NextText()
    {
        if (index < textes.Length - 1)
        {
            textes[index].gameObject.SetActive(false);
            index++;
            textes[index].gameObject.SetActive(true);
        }
        UpdateButtons();
    }

    void PreviousText()
    {
        if (index > 0)
        {
            textes[index].gameObject.SetActive(false);
            index--;
            textes[index].gameObject.SetActive(true);
        }
        UpdateButtons();
    }

    void UpdateButtons()
    {
        backButton.interactable = (index > 0);
        nextButton.interactable = (index < textes.Length - 1);
    }
}
