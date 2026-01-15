using UnityEngine;

public class DestructeurDechets : MonoBehaviour
{
    // Ajoute cette ligne pour voir la case dans l'Inspector
    public ParticleSystem particules;

    private void OnTriggerEnter(Collider other)
    {
        // On lance les particules
        if (particules != null)
        {
            particules.Play();
        }

        // On détruit l'objet
        Destroy(other.gameObject);
    }
}