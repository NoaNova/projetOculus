using UnityEngine;

public class DestructeurDechets : MonoBehaviour
{
    // Cette fonction magique est appelée automatiquement par Unity
    // quand UNIQUEMENT un objet entre dans une zone "Is Trigger".
    private void OnTriggerEnter(Collider other)
    {
        // 1. Vérification de sécurité :
        // Est-ce que l'objet qui vient d'entrer a bien le badge "Dechet" ?
        if (other.CompareTag("Trash"))
        {
            // 2. Si oui, on détruit l'objet entier (gameObject).
            Destroy(other.gameObject);

            // (Optionnel) Tu pourrais ajouter du son ou des points ici plus tard !
            // Debug.Log("Déchet détruit !"); 
        }
    }
}