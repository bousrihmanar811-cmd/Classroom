//using UnityEngine;
//using UnityEngine.UI;

//public class ToggleShowObject : MonoBehaviour
//{
//    public Toggle myToggle;        // Le Toggle à assigner dans l’inspecteur
//    public GameObject bravoObject; // Le GameObject à afficher

//    void Start()
//    {
//        // S'assurer que l'objet est caché au début
//        bravoObject.SetActive(false);

//        // Ajouter un listener pour détecter le clic
//        myToggle.onValueChanged.AddListener(OnToggleChanged);
//    }

//    void OnToggleChanged(bool isOn)
//    {
//        // Si le toggle est activé ? affiche Bravo
//        bravoObject.SetActive(isOn);
//    }
//}
using UnityEngine;
using UnityEngine.UI;

public class ToggleShowObject : MonoBehaviour
{
    public Toggle myToggle;        // Le Toggle à assigner dans l'inspecteur
    public GameObject bravoObject; // Le GameObject à afficher
    public AudioClip soundEffect;  // Le son à jouer
    public AudioSource audioSource; // Source audio (peut être partagée)

    void Start()
    {
        // S'assurer que l'objet est caché au début
        bravoObject.SetActive(false);

        // Créer une source audio si aucune n'est assignée
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Ajouter un listener pour détecter le clic
        myToggle.onValueChanged.AddListener(OnToggleChanged);
    }

    void OnToggleChanged(bool isOn)
    {
        // Si le toggle est activé, affiche Bravo et joue le son
        if (isOn)
        {
            bravoObject.SetActive(true);
            PlaySound();
        }
        else
        {
            bravoObject.SetActive(false);
        }
    }

    void PlaySound()
    {
        if (soundEffect != null && audioSource != null)
        {
            audioSource.PlayOneShot(soundEffect);
        }
    }
}