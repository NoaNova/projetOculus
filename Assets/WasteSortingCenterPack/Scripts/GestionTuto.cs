using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Utile si tu veux un bouton "Rejouer"

public class GestionTuto : MonoBehaviour
{
    public static GestionTuto instance;

    [Header("--- UI Tâches ---")]
    // Ton tableau actuel avec les 5 textes
    public TextMeshProUGUI[] lesTaches;

    [Header("--- Écran de Fin ---")]
    // Glisse ton Canvas de fin (ou un Panel) ici dans l'inspecteur
    public GameObject ecranFin;

    // Compteur interne pour savoir où on en est
    private int nombreTachesFinies = 0;

    void Awake()
    {
        instance = this;

        // On s'assure que l'écran de fin est caché au démarrage
        if (ecranFin != null)
            ecranFin.SetActive(false);
    }

    public void ValiderTache(int numero)
    {
        // On vérifie si la tâche n'est pas déjà validée pour ne pas compter deux fois
        if (numero >= 0 && numero < lesTaches.Length && lesTaches[numero].color != Color.green)
        {
            // 1. Visuel : Vert et Barré
            lesTaches[numero].color = Color.green;
            lesTaches[numero].fontStyle = FontStyles.Strikethrough;

            // 2. Logique : On incrémente le compteur
            nombreTachesFinies++;

            // 3. Vérification : Est-ce que tout est fini ?
            VerifierFinJeu();
        }
    }

    void VerifierFinJeu()
    {
        if (nombreTachesFinies >= lesTaches.Length)
        {
            AfficherEcranVictoire();
        }
    }

    void AfficherEcranVictoire()
    {
        if (ecranFin != null)
        {
            ecranFin.SetActive(true);

            // Si tu es en VR, l'écran apparaîtra là où tu l'as placé.
            // Si tu es sur PC (souris), on débloque le curseur :
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            Debug.Log("Bravo ! Toutes les tâches sont terminées.");
        }
    }

    // Fonction optionnelle pour un bouton "Recommencer" sur ton écran de fin
    public void Rejouer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
