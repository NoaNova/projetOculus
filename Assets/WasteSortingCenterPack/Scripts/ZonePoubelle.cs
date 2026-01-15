using UnityEngine;

public class ZonePoubelle : MonoBehaviour
{
    [Header("--- Effets Visuels ---")]
    public ParticleSystem particulesBravo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("MainCamera") || other.CompareTag("GameController"))
        {
            return;
        }

        if (other.CompareTag("Untagged"))
        {
            return;
        }

        // cas plastique
        if (other.CompareTag("Plastique"))
        {
            if (GestionScore.instance != null) GestionScore.instance.AjouterPoints(1);
            if (GestionTuto.instance != null) GestionTuto.instance.ValiderTache(0);

            // particules
            if (particulesBravo != null)
            {
                particulesBravo.Play();
            }

            Destroy(other.gameObject);
        }

        else if (other.CompareTag("Carton") || other.CompareTag("Normal"))
        {
            if (GestionScore.instance != null) GestionScore.instance.AjouterPoints(-1);
            if (GestionTuto.instance != null) GestionTuto.instance.ValiderTache(1);

            Destroy(other.gameObject);
        }
    }
}