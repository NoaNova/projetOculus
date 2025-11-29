using System.Collections;
using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    [Header("Réglages")]
    public GameObject[] trashPrefabs; // Liste de tes déchets
    public float spawnInterval = 2.0f; // Temps entre chaque apparition (en secondes)
    public float forceEjection = 2.0f; // Force pour pousser l'objet vers la gauche (optionnel)

    private void Start()
    {
        // Lance la boucle infinie d'apparition
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnTrash();
            // Attend le temps défini avant de recommencer
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnTrash()
    {
        // 1. Vérifie s'il y a des prefabs dans la liste
        if (trashPrefabs.Length == 0) return;

        // 2. Choisit un objet au hasard dans la liste
        int randomIndex = Random.Range(0, trashPrefabs.Length);
        GameObject selectedPrefab = trashPrefabs[randomIndex];

        // 3. Crée l'objet à la position du Spawner
        GameObject newTrash = Instantiate(selectedPrefab, transform.position, Random.rotation);

        // 4. (Optionnel) Ajoute une petite poussée vers la gauche si nécessaire
        // Assure-toi que l'axe -transform.right correspond bien à la gauche dans ta scène
        Rigidbody rb = newTrash.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(-transform.right * forceEjection, ForceMode.Impulse);
        }



    }
}