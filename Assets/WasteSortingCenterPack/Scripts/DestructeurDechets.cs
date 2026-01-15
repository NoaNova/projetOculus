using UnityEngine;

public class DestructeurDechets : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // On détruit simplement tout ce qui arrive au bout du tapis.
        // Pas de points, pas de validation de tâche.
        // C'est juste pour que les objets ne tombent pas à l'infini.
        Destroy(other.gameObject);
    }
}