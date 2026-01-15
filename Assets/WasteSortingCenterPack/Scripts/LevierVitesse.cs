using UnityEngine;
using System.Collections;

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
    public float tempsAvantRelance =3.0f; 

    private float ratioDepart;
    private Vector3 positionUrgenceDepart;
    private bool aBougeVitesse = false;
    private bool aBougeUrgence = false;

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

        // levier vitesse
        float angleActuel = levierVitesse.angle;
        float ratioActuel = Mathf.InverseLerp(angleArret, angleMax, angleActuel);
        float vitesseTheorique = ratioActuel * vitesseMax;

        // tuto levier vitesse
        if (!aBougeVitesse && Mathf.Abs(ratioActuel - ratioDepart) > 0.05f)
        {
            if (GestionTuto.instance != null) GestionTuto.instance.ValiderTache(4);
            aBougeVitesse = true;
        }

        // arrêt d'urgence
        float distanceParcourue = Vector3.Distance(poigneeUrgence.localPosition, positionUrgenceDepart);
        float vitesseFinale = vitesseTheorique;

        // arrêt déclanché
        if (distanceParcourue > seuilDeclenchement && !estEnArretUrgence)
        {
            StartCoroutine(SequenceArretUrgence());
        }

        // en arrêt d'urgence la vitesse est forcée à 0
        if (estEnArretUrgence)
        {
            vitesseFinale = 0;
        }

        foreach (var t in tapis) if (t != null) t.SetSpeed(vitesseFinale);

        if (spawner != null)
        {
            spawner.machineActive = (vitesseFinale >= 0.1f);
        }
    }

    IEnumerator SequenceArretUrgence()
    {
        estEnArretUrgence = true;

        // partie tuto
        if (!aBougeUrgence)
        {
            if (GestionTuto.instance != null) GestionTuto.instance.ValiderTache(3);
            aBougeUrgence = true;
        }

        // attente
        yield return new WaitForSeconds(tempsAvantRelance);

        // retour à la normale : on remet la poignée à sa place
        poigneeUrgence.localPosition = positionUrgenceDepart;

        estEnArretUrgence = false;
    }
}
