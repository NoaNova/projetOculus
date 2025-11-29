using UnityEngine;

public class LevierVitesse : MonoBehaviour
{
    [Header("--- Liaisons ---")]
    public HingeJoint levierVitesse;    // Manette Droite
    public Transform poigneeUrgence;    // Manette Gauche
    public TreadmillForce[] tapis;      // Les Tapis
    public TrashSpawner spawner;        // --- NOUVEAU : Le Spawner ---

    [Header("--- Réglages Vitesse ---")]
    public float angleArret = -45f;
    public float angleMax = 45f;
    public float vitesseMax = 3.0f;

    [Header("--- Réglages Urgence ---")]
    public float seuilDeclenchement = 0.04f;
    public bool inverserLogique = false;

    void Update()
    {
        if (levierVitesse == null || poigneeUrgence == null) return;

        // 1. Calcul vitesse
        float angleActuel = levierVitesse.angle;
        float ratio = Mathf.InverseLerp(angleArret, angleMax, angleActuel);
        float vitesseFinale = ratio * vitesseMax;

        // 2. Urgence
        float yActuel = poigneeUrgence.localPosition.y;
        if (yActuel > seuilDeclenchement)
        {
            vitesseFinale = 0;
        }

        // 3. Envoi aux tapis
        foreach (var t in tapis)
        {
            if (t != null) t.SetSpeed(vitesseFinale);
        }

        // --- 4. NOUVEAU : Contrôle du Spawner ---
        if (spawner != null)
        {
            // Si la vitesse est très faible (presque 0), on coupe le spawner
            if (vitesseFinale < 0.1f)
            {
                spawner.machineActive = false;
            }
            else
            {
                spawner.machineActive = true;
            }
        }
    }
}