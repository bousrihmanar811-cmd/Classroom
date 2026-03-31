using UnityEngine;

public class TableScript : MonoBehaviour
{
    public GameObject feuilleExamPrefab; // prefab de la feuille
    public GameObject[] stylos; // objets stylos dans la scène

    private bool feuilleAffichee = false;

    void Start()
    {
        // Les stylos sont invisibles au départ
        foreach (GameObject stylo in stylos)
        {
            if (stylo != null) stylo.SetActive(false);
        }
    }

    public void SelectionnerTable()
    {
        Debug.Log("Table touchée");

        // Téléportation du joueur
        Transform joueur = GameObject.FindWithTag("Player").transform;
        Vector3 positionAssise = transform.position + transform.forward * 0.5f;
        positionAssise.y = joueur.position.y; // garder hauteur du joueur
        joueur.position = positionAssise;
        joueur.rotation = Quaternion.LookRotation(-transform.forward);

        // Affichage feuille
        if (!feuilleAffichee)
        {
            Vector3 positionFeuille = transform.position + transform.forward * 0.5f + Vector3.up * 0.8f;

            GameObject feuille = Instantiate(feuilleExamPrefab, positionFeuille, Quaternion.identity);
            feuille.transform.SetParent(transform); // devient enfant de la table
            feuilleAffichee = true;

            Invoke("AfficherStylos", 30f);
        }
    }

    void AfficherStylos()
    {
        foreach (GameObject stylo in stylos)
        {
            if (stylo != null) stylo.SetActive(true);
        }
    }
}
