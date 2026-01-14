using UnityEngine;

public class ZonePoubelle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // --- SECURITE ANTI-SUICIDE ---
        // On vérifie si l'objet est le Joueur ou une partie du corps
        // Si c'est le cas, on arrête tout de suite la fonction (return).
        if (other.CompareTag("Player") || other.CompareTag("MainCamera") || other.CompareTag("GameController"))
        {
            return; // On ne fait RIEN, on laisse le joueur tranquille !
        }

        // On ignore aussi le décor (Untagged) pour éviter de détruire le sol si la zone est mal placée
        if (other.CompareTag("Untagged"))
        {
            return;
        }

        // -----------------------------

        // CAS 1 : C'est du PLASTIQUE (Bravo)
        if (other.CompareTag("Plastique"))
        {
            if (GestionScore.instance != null) GestionScore.instance.AjouterPoints(1);
            if (GestionTuto.instance != null) GestionTuto.instance.ValiderTache(0); // Tache "Bouteille"

            Destroy(other.gameObject);
        }

        // CAS 2 : C'est du CARTON ou NORMAL (Erreur)
        else if (other.CompareTag("Carton") || other.CompareTag("Normal"))
        {
            if (GestionScore.instance != null) GestionScore.instance.AjouterPoints(-1);
            if (GestionTuto.instance != null) GestionTuto.instance.ValiderTache(1); // Tache "Carton" (Validée quand même pour l'apprentissage)

            Destroy(other.gameObject);
        }

        // CAS 3 : Autres objets (Optionnel)
        // J'ai retiré le "else" destructeur universel par sécurité.
        // Si tu veux détruire d'autres trucs spécifiques, ajoute des "else if".
    }
}