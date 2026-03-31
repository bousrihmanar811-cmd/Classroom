using UnityEngine;
using UnityEngine.UI;

public class ToggleVR : MonoBehaviour
{
    public Toggle toggle;
    public bool isCorrectAnswer = false;
    public Image feedbackImage; // drag "Background" ici

    void Start()
    {
        if (toggle == null) toggle = GetComponent<Toggle>();
        //toggle.onValueChanged.AddListener(OnToggleChanged);
    }
    private void OnTriggerEnter(Collider other)
    {
        // Vérifie que c'est la main ou le rayon
         if (other.CompareTag("Controller")) { 
            toggle.isOn = !toggle.isOn; // inverse l'état du Toggle
            CheckAnswer(); }
    }
    void CheckAnswer()
    {
        if (toggle.isOn)
        {
            feedbackImage.color = isCorrectAnswer ? Color.green : Color.red;
        }
        else
        {
            feedbackImage.color = Color.white;
        }
    }
}




//using UnityEngine;
//using UnityEngine.UI;

//public class ToggleVR : MonoBehaviour
//{
//    public Toggle toggle;
//    public bool isCorrectAnswer = false;
//    public Image feedbackImage;
//    public ToggleGroup toggleGroup; // Assign your toggle group here

//    void Start()
//    {
//        if (toggle == null) toggle = GetComponent<Toggle>();
//        toggle.onValueChanged.AddListener(OnToggleChanged);
//    }

//    void OnToggleChanged(bool isOn)
//    {
//        if (isOn)
//        {
//            feedbackImage.color = isCorrectAnswer ? Color.green : Color.red;

//            // Disable colliders of other toggles in the group
//            foreach (Toggle otherToggle in toggleGroup.GetComponentsInChildren<Toggle>())
//            {
//                if (otherToggle != toggle)
//                {
//                    otherToggle.GetComponent<Collider>().enabled = false;
//                }
//            }
//        }
//        else
//        {
//            feedbackImage.color = Color.white;
//        }
//    }

//    // Call this to reset all toggles
//    public void ResetToggles()
//    {
//        foreach (Toggle t in toggleGroup.GetComponentsInChildren<Toggle>())
//        {
//            t.GetComponent<Collider>().enabled = true;
//            t.isOn = false;
//        }
//    }
//}