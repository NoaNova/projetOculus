using UnityEngine;
using UnityEngine.Events;

public class DetecteurLevier : MonoBehaviour
{
    // choisir le numéro de la tâche dans l'inspecteur (3 pour levier d'arrêt d'urgence, 4 pour levier de vitesse)
    public int numeroTacheAValider;

    public void AnalyserLevier(float valeur)
    {
        //0.0 Levier baissé et 1.0 Levier levé
        if (valeur > 0.8f)
        { //activé a 0.8
            if (GestionTuto.instance != null)
            {
                GestionTuto.instance.ValiderTache(numeroTacheAValider);
            }
        }
    }
}
