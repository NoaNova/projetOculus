using UnityEngine;
using TMPro; // Indispensable pour parler au texte

public class GestionScore : MonoBehaviour
{
    public static GestionScore instance; // Permet aux autres scripts de l'appeler facilement
    public TextMeshProUGUI texteScore;   // Case pour glisser ton texte HUD

    private int score = 0;

    void Awake()
    {
        instance = this; // Je suis le chef !
    }

    public void AjouterPoints(int points)
    {
        score += points;
        MiseAJourTexte();
    }

    void MiseAJourTexte()
    {
        // Met à jour le texte à l'écran
        if (texteScore != null)
        {
            texteScore.text = "Score : " + score;

            // Petit bonus visuel : Rouge si négatif, Blanc si positif
            if (score < 0) texteScore.color = Color.red;
            else texteScore.color = Color.white;
        }
    }
}