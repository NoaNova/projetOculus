using UnityEngine;
using TMPro;

public class GestionScore : MonoBehaviour
{
    public static GestionScore instance;
    public TextMeshProUGUI texteScore;

    private int score = 0;

    void Awake()
    {
        instance = this;
    }

    public void AjouterPoints(int points)
    {
        // 1. On modifie le score
        score += points;

        // 2. LA SÉCURITÉ (Clamp)
        // Si le score est passé en dessous de 0, on le force à 0.
        if (score < 0)
        {
            score = 0;
        }

        // 3. On met à jour l'affichage
        MiseAJourTexte();

        // 4. Vérification Tuto (Tâche 2 : Atteindre 10 pts)
        if (score >= 10)
        {
            if (GestionTuto.instance != null) GestionTuto.instance.ValiderTache(2);
        }
    }

    void MiseAJourTexte()
    {
        if (texteScore != null)
        {
            texteScore.text = "Score : " + score;
            // Comme le score ne peut plus être négatif, on le laisse toujours blanc
            texteScore.color = Color.white;
        }
    }
}