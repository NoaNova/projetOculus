using UnityEngine;
using UnityEngine.Events; // Nécessaire pour détecter les événements

public class DetecteurLevier : MonoBehaviour
{
    // On choisira le numéro de la tâche dans l'inspecteur (3 pour Urgence, 4 pour Vitesse)
    public int numeroTacheAValider;

    // Cette fonction sera appelée automatiquement par le levier quand il bouge
    public void AnalyserLevier(float valeur)
    {
        // "valeur" est un chiffre entre 0.0 (Levier baissé) et 1.0 (Levier levé)

        // Si le levier est activé (tiré à plus de 80%)
        if (valeur > 0.8f)
        {
            if (GestionTuto.instance != null)
            {
                GestionTuto.instance.ValiderTache(numeroTacheAValider);
            }
        }
    }
}