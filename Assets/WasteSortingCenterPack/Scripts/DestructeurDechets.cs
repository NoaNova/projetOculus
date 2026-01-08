using UnityEngine;

public class DestructeurDechets : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // RÈGLE DU TAPIS : On ne doit pas laisser tomber le plastique par terre !

        // 1. Si c'est du Plastique, c'est une faute -> -1 point
        if (other.CompareTag("Plastique"))
        {
            GestionScore.instance.AjouterPoints(-1);
        }

        // 2. Si c'est du Carton ou Trash, ce n'est pas grave (0 point)
        // Mais dans TOUS les cas, on détruit l'objet pour nettoyer la scène.

        if (other.CompareTag("Plastique") || other.CompareTag("Carton") || other.CompareTag("Trash"))
        {
            Destroy(other.gameObject);
        }
    }
}