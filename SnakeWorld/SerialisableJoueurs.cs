using System;
using System.Collections.Generic;
using System.IO;
using TextReader = System.IO.TextReader;
using System.Xml.Serialization;

namespace SerpentJeu;

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
    public void AjouterUtilisateur(string filePath,string login, string motDePasse,int MeilleurScore)
    {
        
        // Vérifier si les champs sont valides
        if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(motDePasse))
        {
            Console.WriteLine("Erreur : login ou mot de passe vide.");
            return;
        }
        bool ok=false;
       
        // Vérifier si l'utilisateur existe déjà
        foreach (var joueur in Joueurs)
        {
            if (joueur.Login == login && joueur.MotDePasse==motDePasse)
            {
                ok=true;
                Console.WriteLine("Erreur : cet utilisateur existe deja.");
                return;
            }
        }
    
        // Ajouter le nouvel utilisateur
        if(!ok){
            Joueurs.Add(new Joueur { Login = login, MotDePasse = motDePasse,MeilleurScore= MeilleurScore });
            //AddUtilisateur(filePath);
            SerialiserJoueurs(filePath);
            Console.WriteLine($"Utilisateur {login} ajoute avec succès.");
        }
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
    
    
    public bool VerifierUtilisateur(string login, string motDePasse)
    {
        if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(motDePasse))
        {
            return false; // Refuse les champs vides ou contenant uniquement des espaces
        }

        foreach (var Joueur in Joueurs)
        {
            if (Joueur.Login == login && Joueur.MotDePasse == motDePasse)
                return true;
        }
        return false;
    }

    public static ListeUtilisateurs ChargerDepuisXml(string chemin)
    {
        if (!File.Exists(chemin))
            return new ListeUtilisateurs();

        var serializer = new XmlSerializer(typeof(ListeUtilisateurs), "http://www.univ-grenoble-alpes.fr/l3miage/jeu");
        using (var lecteur = new StreamReader(chemin))
        {
            return (ListeUtilisateurs)serializer.Deserialize(lecteur);
        }
    }
}