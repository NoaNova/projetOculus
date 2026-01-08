using UnityEngine;

public class DestructeurDechets : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // On vérifie si l'objet a l'étiquette "Plastique" OU (||) l'étiquette "Normal"
        // On garde aussi "Trash" au cas où tu as oublié de changer certains objets.
        if (other.CompareTag("Plastique") || other.CompareTag("Carton") || other.CompareTag("Trash"))
        {
            // On détruit l'objet pour nettoyer le jeu
            Destroy(other.gameObject);
        }
    }
}