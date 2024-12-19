using System;
using System.Xml.Serialization;

namespace SnakeWorld;

[Serializable]
public class Joueur
{
    private uint _id;
    private String _nom;
    private uint _meilleurScore;
    
    
    [XmlAttribute("id")]
    public uint Id
    {
        set => _id = value;
        get => _id;
    }

    [XmlElement("nom")]
    public string Nom
    {
        set => _nom = value;
        get => _nom;
    }

    [XmlElement("meilleurScore")]
    public uint MeilleurScore
    {
        init => _meilleurScore = 0;
        get => _meilleurScore;
    }

    public void updateMeilleurScore(uint score)
    {
        if (score > this._meilleurScore)
        {
            this._meilleurScore = score;
        }
    }
}