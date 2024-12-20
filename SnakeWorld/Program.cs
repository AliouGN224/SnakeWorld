using System;
using SerpentJeu;
using SnakeWorld;

namespace SerpentJeu
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            /*string pathXml = "xml/joueurs.xml";
            SerialisableJoueurs joueurs = new SerialisableJoueurs();
            Joueur j1 = new Joueur
            {
                Nom = "Joueur 1", Id = 123
            };
            Joueur j2 = new Joueur
            {
                Nom = "Joueur 2", Id = 456
            };
            Joueur j3 = new Joueur
            {
                Nom = "Joueur 3", Id = 789
            };
            
            joueurs.Joueurs.Add(j1);
            joueurs.Joueurs.Add(j2);
            joueurs.Joueurs.Add(j3);
            
<<<<<<< HEAD
            joueurs.SerialiserJoueurs(pathXml);*/
            /*SerialisableJoueurs joueursDeserialiser = new SerialisableJoueurs();
            joueursDeserialiser.DeserialiserJoueurs(pathXml);
            Console.WriteLine(joueursDeserialiser.ToString());
            joueursDeserialiser.Joueurs[0].updateMeilleurScore(89);
            Console.WriteLine(joueursDeserialiser.ToString());*/
            
            /*SerialisableJoueurs joueursDeserialiser = new SerialisableJoueurs();
=======
            joueurs.SerialiserJoueurs(pathXml);
            SerialisableJoueurs joueursDeserialiser = new SerialisableJoueurs();
>>>>>>> 2679330e8adc9d974a447739d76a8757263d5863
            joueursDeserialiser.DeserialiserJoueurs(pathXml);
            Console.WriteLine(joueursDeserialiser.ToString());
            joueursDeserialiser.Joueurs[0].updateMeilleurScore(89);
            Console.WriteLine(joueursDeserialiser.ToString());
    */
            try
            {
                using var game = new JeuPrincipal();
                game.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }

            // Garder la fenêtre ouverte
            Console.WriteLine("Appuyez sur une touche pour fermer...");
            Console.ReadLine();
        }
    }
}