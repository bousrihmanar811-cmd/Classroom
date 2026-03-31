using UnityEngine;

public class TableLightMarker : MonoBehaviour
{
    public Light tableLight; // Assigne ta PointLight ou SpotLight dans l’inspecteur

    void Start()
    {
        if (tableLight != null)
        {
            tableLight.enabled = false; // commence éteinte

            // Planifie l'allumage et l'extinction
            Invoke(nameof(HighlightTable), 3f);   // allume à 3s
            Invoke(nameof(RemoveHighlight), 27f); // éteint à 27s
        }
    }

    public void HighlightTable()
    {
        if (tableLight != null)
            tableLight.enabled = true;
    }

    public void RemoveHighlight()
    {
        if (tableLight != null)
            tableLight.enabled = false;
    }
}
