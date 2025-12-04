using System.Collections;
using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    [Header("Réglages")]
    public GameObject[] trashPrefabs;
    public float spawnInterval = 2.0f;
    public float forceEjection = 2.0f;

    // --- NOUVEAU : L'interrupteur ---
    // Si c'est true, ça spawn. Si c'est false, ça attend.
    public bool machineActive = false;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            // On attend le délai quoiqu'il arrive
            yield return new WaitForSeconds(spawnInterval);

            // On vérifie si la machine est allumée AVANT de faire apparaître l'objet
            if (machineActive == true)
            {
                SpawnTrash();
            }
        }
    }

    void SpawnTrash()
    {
        if (trashPrefabs.Length == 0) return;

        int randomIndex = Random.Range(0, trashPrefabs.Length);
        GameObject selectedPrefab = trashPrefabs[randomIndex];


        GameObject newTrash = Instantiate(selectedPrefab, transform.position, Random.rotation);

        Rigidbody rb = newTrash.GetComponent<Rigidbody>();
        if (rb != null)
        {
    
            rb.linearVelocity = Vector3.zero;
            rb.AddForce(-transform.right * forceEjection, ForceMode.VelocityChange);
        }
    }
}