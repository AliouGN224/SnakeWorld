using System;

namespace SerpentJeu
{
    [Serializable]
    public class EtatJeu
    {
        public int Score { get; set; }
        public string Statut { get; set; }
        public DateTime Horodatage { get; set; }
    }
}