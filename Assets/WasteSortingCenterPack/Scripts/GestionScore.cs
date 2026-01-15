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
        // On modifie le score
        score += points;
        //pas de score  négatif
        if (score < 0)
        {
            score = 0;
        }
        // On met à jour l'affichage
        MiseAJourTexte();

        // Vérification Tuto (Tâche 2 : Atteindre 10 pts)
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
            //couleur du texte
            texteScore.color = Color.white;
        }
    }
}
