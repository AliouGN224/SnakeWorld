using System;
using SerpentJeu;

namespace SerpentJeu
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            try
            {
                //var pageConnexion = new PageConnexion();
                //pageConnexion.AfficherMenu();
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