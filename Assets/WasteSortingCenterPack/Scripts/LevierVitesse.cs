using UnityEngine;

public class LevierVitesse : MonoBehaviour
{
    [Header("--- Liaisons ---")]
    public HingeJoint levierVitesse;
    public Transform poigneeUrgence;
    public TreadmillForce[] tapis;
    public TrashSpawner spawner;

    [Header("--- Réglages Vitesse ---")]
    public float angleArret = -45f;
    public float angleMax = 45f;
    public float vitesseMax = 3.0f;

    [Header("--- Réglages Urgence ---")]
    // Distance en mètres (0.04 = 4cm). Si on tire de 4cm, ça coupe.
    public float seuilDeclenchement = 0.04f;

    // Mémorisation
    private float ratioDepart;
    private Vector3 positionUrgenceDepart; // On stocke la position 3D (Vector3)

    // Verrous
    private bool aBougeVitesse = false;
    private bool aBougeUrgence = false;

    void Start()
    {
        // 1. Levier Vitesse
        if (levierVitesse != null)
        {
            ratioDepart = Mathf.InverseLerp(angleArret, angleMax, levierVitesse.angle);
        }

        // 2. Poignée Urgence : On mémorise la position locale de départ exacte
        if (poigneeUrgence != null)
        {
            positionUrgenceDepart = poigneeUrgence.localPosition;
        }
    }

    void Update()
    {
        if (levierVitesse == null || poigneeUrgence == null) return;

        // --- PARTIE VITESSE (Manette Droite) ---
        float angleActuel = levierVitesse.angle;
        float ratioActuel = Mathf.InverseLerp(angleArret, angleMax, angleActuel);
        float vitesseTheorique = ratioActuel * vitesseMax;

        if (!aBougeVitesse && Mathf.Abs(ratioActuel - ratioDepart) > 0.05f)
        {
            if (GestionTuto.instance != null) GestionTuto.instance.ValiderTache(4);
            aBougeVitesse = true;
        }

        // --- PARTIE URGENCE (Manette Gauche) ---
        float vitesseFinale = vitesseTheorique;

        // NOUVEAU CALCUL MAGIQUE : La Distance 3D
        // On calcule la distance entre la position actuelle et celle de départ.
        // Peu importe si ça bouge en X, Y ou Z, Vector3.Distance le détectera.
        float distanceParcourue = Vector3.Distance(poigneeUrgence.localPosition, positionUrgenceDepart);

        // Si on a tiré la poignée de plus de 4cm (dans n'importe quel sens)
        if (distanceParcourue > seuilDeclenchement)
        {
            vitesseFinale = 0; // COUPURE

            // Validation Tuto
            if (!aBougeUrgence && vitesseTheorique > 0.1f)
            {
                if (GestionTuto.instance != null) GestionTuto.instance.ValiderTache(3);
                aBougeUrgence = true;
            }
        }

        // --- APPLICATION ---
        foreach (var t in tapis) if (t != null) t.SetSpeed(vitesseFinale);

        if (spawner != null)
        {
            spawner.machineActive = (vitesseFinale >= 0.1f);
        }
    }
}