using UnityEngine;
using UnityEngine.UI; // Nécessaire si tu utilises l'UI standard
using TMPro; // Nécessaire si tu utilises TextMeshPro

public class GestionTuto : MonoBehaviour
{
    public static GestionTuto instance;

    [Header("Configuration")]
    public int nombreTotalTaches = 2; // Mets ici le nombre de tâches (ex: 2 pour Bouteille + Carton)

    [Header("Interface")]
    public GameObject ecranBravo; // La case pour glisser ton Canvas "Ecran_Fin"

    // Pour éviter de gagner deux fois la même tâche
    private bool[] tachesRealisees;
    private int compteur = 0;

    private void Awake()
    {
        instance = this;
        tachesRealisees = new bool[nombreTotalTaches]; // On prépare le tableau

        // Sécurité : On s'assure que l'écran Bravo est éteint au début
        if (ecranBravo != null)
            ecranBravo.SetActive(false);
    }

    public void ValiderTache(int indexTache)
    {
        // 1. On vérifie si l'index est valide
        if (indexTache < 0 || indexTache >= nombreTotalTaches) return;

        // 2. Si cette tâche est déjà faite, on arrête (on ne la compte pas deux fois)
        if (tachesRealisees[indexTache] == true) return;

        // 3. On valide la tâche
        tachesRealisees[indexTache] = true; // On coche la case dans la mémoire
        compteur++; // On ajoute 1 au score de progression

        Debug.Log("Tâche " + indexTache + " validée ! Progression : " + compteur + "/" + nombreTotalTaches);

        // --- ICI TU PEUX AJOUTER TON CODE POUR RAYER LE TEXTE (Barre) ---
        // Exemple : lignesDeRayure[indexTache].SetActive(true);
        // ---------------------------------------------------------------

        // 4. VERIFICATION DE VICTOIRE
        if (compteur >= nombreTotalTaches)
        {
            AfficherEcranFin();
        }
    }

    void AfficherEcranFin()
    {
        Debug.Log("Tuto Terminé ! BRAVO !");
        if (ecranBravo != null)
        {
            ecranBravo.SetActive(true); // On allume le Canvas

            // Petit bonus : Jouer un son ici si tu veux
        }
    }
}