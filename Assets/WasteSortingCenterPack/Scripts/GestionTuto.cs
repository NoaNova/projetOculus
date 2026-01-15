using UnityEngine;
using TMPro; // Nécessaire pour modifier le texte

public class GestionTuto : MonoBehaviour
{
    public static GestionTuto instance;

    [Header("Configuration")]
    // Ta liste qui contient tes textes
    public TextMeshProUGUI[] lesTaches;

    [Header("Ecran de Fin")]
    public GameObject ecranBravo; // La case pour glisser ton Canvas "Bravo"

    // Mémoire interne pour savoir quelles tâches sont finies
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
        // 1. Sécurités de base
        if (numero < 0 || numero >= lesTaches.Length) return;

        // 2. Si la tâche est DEJA faite, on ne fait rien (pour ne pas compter les points en double)
        if (tachesFaites[numero] == true) return;

        // 3. On valide la tâche
        tachesFaites[numero] = true;
        compteur++;

        // 4. On change le visuel (Ton code d'origine)
        if (lesTaches[numero] != null)
        {
            lesTaches[numero].color = Color.green;
            lesTaches[numero].fontStyle = FontStyles.Strikethrough;
        }

        // 5. On vérifie si tout est fini
        // Si le compteur est égal au nombre total de textes dans ta liste...
        if (compteur >= lesTaches.Length)
        {
            AfficherEcranFin();
        }
    }

    void AfficherEcranFin()
    {
        if (ecranBravo != null)
        {
            ecranBravo.SetActive(true); // On allume l'écran
        }
    }
}