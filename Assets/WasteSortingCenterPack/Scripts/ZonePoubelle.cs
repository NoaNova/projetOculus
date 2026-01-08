using UnityEngine;

public class ZonePoubelle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // RÈGLE DE LA POUBELLE : Seul le plastique doit entrer ici.

        if (other.CompareTag("Plastique"))
        {
            // C'est bien du plastique ! Bravo !
            GestionScore.instance.AjouterPoints(1);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Carton") || other.CompareTag("Trash"))
        {
            // Aïe ! On a jeté du carton/déchet normal dans la poubelle plastique !
            GestionScore.instance.AjouterPoints(-1);
            Destroy(other.gameObject);
        }
    }
}