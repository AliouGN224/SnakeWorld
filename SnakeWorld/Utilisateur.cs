using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

[Serializable]
public class Utilisateur
{
    public string Login { get; set; }
    public string MotDePasse { get; set; }
}

[Serializable]
public class ListeUtilisateurs
{
    public List<Utilisateur> Utilisateurs { get; set; } = new List<Utilisateur>();

    public void AjouterUtilisateur(string login, string motDePasse)
    {
        Utilisateurs.Add(new Utilisateur { Login = login, MotDePasse = motDePasse });
    }

    public bool VerifierUtilisateur(string login, string motDePasse)
    {
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

        var serializer = new XmlSerializer(typeof(ListeUtilisateurs));
        using (var lecteur = new StreamReader(chemin))
        {
            return (ListeUtilisateurs)serializer.Deserialize(lecteur);
        }
    }

    public void SauvegarderEnXml(string chemin)
    {
        var serializer = new XmlSerializer(typeof(ListeUtilisateurs));
        using (var ecrivain = new StreamWriter(chemin))
        {
            serializer.Serialize(ecrivain, this);
        }
    }
}