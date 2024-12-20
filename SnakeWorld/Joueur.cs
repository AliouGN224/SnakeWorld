using System;
using System.Xml.Serialization;

namespace SerpentJeu;

[Serializable]
public class Joueur
{
    [XmlAttribute("id")]
    private int Id { get; set; }
    [XmlElement("login")]
    public string Login { get; set; }

    [XmlElement("motDePasse")]
    public string MotDePasse { get; set; }

    [XmlElement("meilleurScore")]
    public int MeilleurScore { get; set; } = 0; // Ajout du champ `meilleurScore` pour correspondre au XML
    
    

  

    public void updateMeilleurScore(int score)
    {
        if (score > this.MeilleurScore)
        {
            this.MeilleurScore = score;
        }
    }
}