using System;
using System.Collections.Generic;
using System.IO;
using TextReader = System.IO.TextReader;
using System.Xml.Serialization;

namespace SnakeWorld;

[Serializable]
[XmlRoot("joueurs", Namespace = "http://www.univ-grenoble-alpes.fr/l3miage/jeu")]
public class SerialisableJoueurs
{
    private List<Joueur> _joueurs;
    
    [XmlElement("joueur")]
    public List<Joueur> Joueurs
    {
        set => _joueurs = value;
        get => _joueurs;
    }
    
    public SerialisableJoueurs()
    {
        Joueurs = new List<Joueur>();
    }
    
    public void DeserialiserJoueurs(String path)
    {
        using (TextReader reader = new StreamReader(path))
        {
            var xmlJoueurs = new XmlSerializer(typeof(SerialisableJoueurs));
            var deserialized = (SerialisableJoueurs)xmlJoueurs.Deserialize(reader);
            this.Joueurs =  deserialized.Joueurs;
        }
    }
    
    public void SerialiserJoueurs(String path)
    {
        using (var writer = new StreamWriter(path))
        {
            var xmlJoueurs = new XmlSerializer(typeof(SerialisableJoueurs));
            xmlJoueurs.Serialize(writer, this);
        }
    }
    
    
    public override string ToString()
    {
        String s = "";
        s = s + "\t================================\tListe des Joueurs\t================================\n";
        foreach (Joueur joueur in Joueurs)
        {
            s = s + "\t   ---> Joueur : " + joueur.Id + "\n";
            s = s + "\t                 " + joueur.Nom + "\n";
            s = s + "\t                 " + joueur.MeilleurScore + "\n";
            s = s + "\t----------------------------------------------------------------------------------\n";
        }
        
        return s;
    }
}