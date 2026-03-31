using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class QuitGame : MonoBehaviour
{
    public float quitTime = 920f; // Temps avant de quitter (en secondes)

    private void Start()
    {
        Invoke(nameof(Quit), quitTime);
    }

    public void Quit()
    {
        Debug.Log("Le jeu est terminé !");
        // Quitter le mode Play dans l'éditeur
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

