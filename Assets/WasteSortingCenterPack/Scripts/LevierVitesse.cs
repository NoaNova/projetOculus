using UnityEngine;
using System.Collections; // Nécessaire pour les Coroutines

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
    public float seuilDeclenchement = 0.04f;
    public float tempsAvantRelance =3.0f; // Temps d'attente

    private float ratioDepart;
    private Vector3 positionUrgenceDepart;
    private bool aBougeVitesse = false;
    private bool aBougeUrgence = false;

    // Nouveau : pour savoir si on est en mode "Arrêt d'urgence"
    private bool estEnArretUrgence = false;

    void Start()
    {
        if (levierVitesse != null)
            ratioDepart = Mathf.InverseLerp(angleArret, angleMax, levierVitesse.angle);

        if (poigneeUrgence != null)
            positionUrgenceDepart = poigneeUrgence.localPosition;
    }

    void Update()
    {
        if (levierVitesse == null || poigneeUrgence == null) return;

        // 1. Calcul de la vitesse voulue par le levier de vitesse
        float angleActuel = levierVitesse.angle;
        float ratioActuel = Mathf.InverseLerp(angleArret, angleMax, angleActuel);
        float vitesseTheorique = ratioActuel * vitesseMax;

        // Tuto levier vitesse
        if (!aBougeVitesse && Mathf.Abs(ratioActuel - ratioDepart) > 0.05f)
        {
            if (GestionTuto.instance != null) GestionTuto.instance.ValiderTache(4);
            aBougeVitesse = true;
        }

        // 2. Gestion de l'urgence
        float distanceParcourue = Vector3.Distance(poigneeUrgence.localPosition, positionUrgenceDepart);
        float vitesseFinale = vitesseTheorique;

        // Déclenchement de l'arrêt
        if (distanceParcourue > seuilDeclenchement && !estEnArretUrgence)
        {
            StartCoroutine(SequenceArretUrgence());
        }

        // Si on est en arrêt d'urgence, la vitesse est forcée à 0
        if (estEnArretUrgence)
        {
            vitesseFinale = 0;
        }

        // 3. Application aux tapis et spawner
        foreach (var t in tapis) if (t != null) t.SetSpeed(vitesseFinale);

        if (spawner != null)
        {
            spawner.machineActive = (vitesseFinale >= 0.1f);
        }
    }

    // La Coroutine qui gère le délai
    IEnumerator SequenceArretUrgence()
    {
        estEnArretUrgence = true;

        // Validation Tuto
        if (!aBougeUrgence)
        {
            if (GestionTuto.instance != null) GestionTuto.instance.ValiderTache(3);
            aBougeUrgence = true;
        }

        // Attente de 5 secondes
        yield return new WaitForSeconds(tempsAvantRelance);

        // Retour à la normale : on remet la poignée visuellement à sa place
        // Note : Si c'est un objet physique (VR), il faudra peut-être désactiver le grab temporairement
        poigneeUrgence.localPosition = positionUrgenceDepart;

        estEnArretUrgence = false;
    }
}