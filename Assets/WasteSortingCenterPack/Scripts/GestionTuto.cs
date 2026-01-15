using UnityEngine;
using TMPro;

public class GestionTuto : MonoBehaviour
{
    public static GestionTuto instance;
    [Header("Configuration")]
    //la liste qui contient les textes
    public TextMeshProUGUI[] lesTaches;

    [Header("Ecran de Fin")]
    public GameObject ecranBravo;

    //pour savoir quelles tâches sont finies
    private bool[] tachesFaites;
    private int compteur = 0;

    void Awake()
    {
        instance = this;

        // On initialise la mémoire selon le nombre de textes que tu as mis
        tachesFaites = new bool[lesTaches.Length];

        // On cache l'écran Bravo au lancement du jeu
        if (ecranBravo != null)
        {
            ecranBravo.SetActive(false);
        }
    }

    // Fonction à appeler pour valider une étape
    public void ValiderTache(int numero)
    {

        if (numero < 0 || numero >= lesTaches.Length) return;
        if (tachesFaites[numero] == true) return;

        //validation de tâche
        tachesFaites[numero] = true;
        compteur++;

        //tâche rayée en vert qunad finie 
        if (lesTaches[numero] != null)
        {
            lesTaches[numero].color = Color.green;
            lesTaches[numero].fontStyle = FontStyles.Strikethrough;
        }

        //quand toutes les étapes sont finies 
        if (compteur >= lesTaches.Length)
        {
            AfficherEcranFin();
        }
    }

    void AfficherEcranFin()
    {
        if (ecranBravo != null)
        {
            ecranBravo.SetActive(true); //on active le canva de fin
        }
    }
}
