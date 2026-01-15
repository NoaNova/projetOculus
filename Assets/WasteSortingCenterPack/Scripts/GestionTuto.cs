using UnityEngine;
using UnityEngine.UI;
using TMPro; // Nécessaire pour TextMeshPro

// Ajoute automatiquement un AudioSource si tu n'en as pas
[RequireComponent(typeof(AudioSource))]
public class GestionTuto : MonoBehaviour
{
    public static GestionTuto instance;

    [Header("--- CONFIGURATION ---")]
    public int nombreTotalTaches = 2;

    [Header("--- VISUEL TACHES (Tableaux) ---")]
    // Glisse tes textes (Bouteille / Carton) ici
    public TextMeshProUGUI[] textesTaches;

    // Glisse tes images de barres (si tu en as) ici
    public GameObject[] lignesDeRayure;

    [Header("--- FIN DU JEU ---")]
    public GameObject ecranBravo; // Le Canvas de fin
    public AudioClip sonVictoire; // Le son "Win"

    private AudioSource audioSource;
    private bool[] tachesRealisees;
    private int compteur = 0;

    private void Awake()
    {
        instance = this;
        tachesRealisees = new bool[nombreTotalTaches];
        audioSource = GetComponent<AudioSource>();

        // Sécurité : On cache l'écran de fin au début
        if (ecranBravo != null) ecranBravo.SetActive(false);

        // Sécurité : On cache toutes les barres de rayure au début
        foreach (GameObject barre in lignesDeRayure)
        {
            if (barre != null) barre.SetActive(false);
        }
    }

    public void ValiderTache(int indexTache)
    {
        // Vérifications de sécurité
        if (indexTache < 0 || indexTache >= nombreTotalTaches) return;
        if (tachesRealisees[indexTache] == true) return;

        // Validation logique
        tachesRealisees[indexTache] = true;
        compteur++;

        // --- PARTIE VISUELLE (Ce qui manquait) ---

        // 1. Changer la couleur du texte en VERT
        if (indexTache < textesTaches.Length && textesTaches[indexTache] != null)
        {
            textesTaches[indexTache].color = Color.green;
            // Optionnel : Ajouter un effet de style (Gras, italique...)
            // textesTaches[indexTache].fontStyle = FontStyles.Strikethrough; 
        }

        // 2. Activer la barre de rayure (si tu utilises des images)
        if (indexTache < lignesDeRayure.Length && lignesDeRayure[indexTache] != null)
        {
            lignesDeRayure[indexTache].SetActive(true);
        }

        // -----------------------------------------

        // Vérification Victoire
        if (compteur >= nombreTotalTaches)
        {
            AfficherEcranFin();
        }
    }

    void AfficherEcranFin()
    {
        if (ecranBravo != null) ecranBravo.SetActive(true);
        if (audioSource != null && sonVictoire != null) audioSource.PlayOneShot(sonVictoire);
    }
}