using UnityEngine;

public class ZonePoubelle : MonoBehaviour
{
    // 1. ON AJOUTE LA VARIABLE ICI (Pour glisser l'effet dans l'inspecteur)
    [Header("--- Effets Visuels ---")]
    public ParticleSystem particulesBravo;

    private void OnTriggerEnter(Collider other)
    {
        // --- SECURITE ANTI-SUICIDE ---
        if (other.CompareTag("Player") || other.CompareTag("MainCamera") || other.CompareTag("GameController"))
        {
            return;
        }

        // On ignore le décor
        if (other.CompareTag("Untagged"))
        {
            return;
        }

        // -----------------------------

        // CAS 1 : C'est du PLASTIQUE (Bravo)
        if (other.CompareTag("Plastique"))
        {
            if (GestionScore.instance != null) GestionScore.instance.AjouterPoints(1);
            if (GestionTuto.instance != null) GestionTuto.instance.ValiderTache(0);

            // 2. ON LANCE LES PARTICULES ICI (Juste avant de détruire l'objet)
            if (particulesBravo != null)
            {
                particulesBravo.Play(); // BOUM ! Confettis verts !
            }

            Destroy(other.gameObject);
        }

        // CAS 2 : C'est du CARTON ou NORMAL (Erreur)
        else if (other.CompareTag("Carton") || other.CompareTag("Normal"))
        {
            if (GestionScore.instance != null) GestionScore.instance.AjouterPoints(-1);
            if (GestionTuto.instance != null) GestionTuto.instance.ValiderTache(1);

            // (Tu pourrais ajouter un son d'erreur ici plus tard)

            Destroy(other.gameObject);
        }
    }
}