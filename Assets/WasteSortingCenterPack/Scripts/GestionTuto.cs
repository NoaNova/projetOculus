using UnityEngine;
using TMPro; // Nécessaire pour modifier le texte

public class GestionTuto : MonoBehaviour
{
    public static GestionTuto instance;

    // Une liste (tableau) qui va contenir tes 5 textes
    public TextMeshProUGUI[] lesTaches;

    void Awake()
    {
        instance = this;
    }

    // Fonction à appeler pour valider une étape (0 pour la première, 1 pour la deuxième...)
    public void ValiderTache(int numero)
    {
        if (numero >= 0 && numero < lesTaches.Length)
        {
            // On change la couleur en vert
            lesTaches[numero].color = Color.green;

            // On ajoute l'effet "Barré" (Strikethrough)
            lesTaches[numero].fontStyle = FontStyles.Strikethrough;
        }
    }
}