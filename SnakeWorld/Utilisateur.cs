using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;


[Serializable]
public class Utilisateur
{
    [XmlElement("Login")]
    public string Login { get; set; }

    [XmlElement("MotDePasse")]
    public string MotDePasse { get; set; }

    [XmlElement("meilleurScore")]
    public int MeilleurScore { get; set; } = 0; // Ajout du champ `meilleurScore` pour correspondre au XML
}


[Serializable]
[XmlRoot("Utilisateurs", Namespace = "http://www.univ-grenoble-alpes.fr/l3miage/jeu")]
public class ListeUtilisateurs
{
    [XmlElement("utilisateur")] // Utilisez "utilisateur" pour correspondre à la balise XML
    public List<Utilisateur> Utilisateurs { get; set; } = new List<Utilisateur>();

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
        foreach (var utilisateur in Utilisateurs)
        {
            if (utilisateur.Login == login)
            {
                ok=true;
                Console.WriteLine("Erreur : cet utilisateur existe deja.");
                return;
            }
        }

        // Ajouter le nouvel utilisateur
        if(!ok){
            Utilisateurs.Add(new Utilisateur { Login = login, MotDePasse = motDePasse,MeilleurScore= MeilleurScore });
            //AddUtilisateur(filePath);
            SerialiserJoueurs(filePath);
            Console.WriteLine($"Utilisateur {login} ajoute avec succès.");
        }
    }

    
    
    public void SerialiserJoueurs(String filePath)
    {
        using (var writer = new StreamWriter(filePath))
        {
            var xmlJoueurs = new XmlSerializer(typeof(ListeUtilisateurs));
            xmlJoueurs.Serialize(writer, this);
        }
    }

    public static Utilisateur DeserializeFromFile(string filePath)
    {
        XmlSerializer serializer = new(typeof(Utilisateur));
        using StreamReader reader = new(filePath);
        return (Utilisateur)serializer.Deserialize(reader);
    }
    public bool VerifierUtilisateur(string login, string motDePasse)
    {
        if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(motDePasse))
        {
            return false; // Refuse les champs vides ou contenant uniquement des espaces
        }

        foreach (var utilisateur in Utilisateurs)
        {
            if (utilisateur.Login == login && utilisateur.MotDePasse == motDePasse)
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
