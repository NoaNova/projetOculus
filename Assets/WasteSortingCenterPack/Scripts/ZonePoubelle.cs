using UnityEngine;

public class ZonePoubelle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // CAS 1 : C'est du PLASTIQUE (C'est bien !)
        if (other.CompareTag("Plastique"))
        {
            // +1 Point
            if (GestionScore.instance != null) GestionScore.instance.AjouterPoints(1);

            // Valide la Tâche n°0 ("Jeter une bouteille")
            if (GestionTuto.instance != null) GestionTuto.instance.ValiderTache(0);

            Destroy(other.gameObject);
        }

        // CAS 2 : C'est du CARTON ou NORMAL (C'est une erreur !)
        // Le joueur apprend qu'il ne faut pas jeter le carton ici.
        else if (other.CompareTag("Carton") || other.CompareTag("Normal"))
        {
            // -1 Point (Punition)
            if (GestionScore.instance != null) GestionScore.instance.AjouterPoints(-1);

            // Valide la Tâche n°1 ("Jeter une brique de carton")
            // On valide la tâche car le joueur a réalisé l'action demandée (même si c'est une bêtise !)
            if (GestionTuto.instance != null) GestionTuto.instance.ValiderTache(1);

            Destroy(other.gameObject);
        }

        // Sécurité pour les autres objets (Trash, etc.)
        else
        {
            if (GestionScore.instance != null) GestionScore.instance.AjouterPoints(-1);
            Destroy(other.gameObject);
        }
    }
}