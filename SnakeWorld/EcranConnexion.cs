using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace SerpentJeu{

public class EcranConnexion
{
    private string _login = "";
    private string _motDePasse = "";
    private bool _afficherMotDePasse = false; // Permet d'afficher ou masquer le mot de passe
    private bool _saisieLogin = true; // Indique si on est en train de saisir le login ou le mot de passe

    private Texture2D _texturePixel;
    private Rectangle _boutonConnexion;
    private Rectangle _boutonInscription;

    private SpriteFont _police;
    //private ListeUtilisateurs _utilisateurs;
    private SerialisableJoueurs _joueurs;

    private string _cheminXml = "xml/joueurs.xml";
        //Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "xml", "Utilisateurs.xml");

    private Keys[] _dernieresTouches; // Garde une trace des touches déjà pressées

    public bool Connecte { get; private set; } = false;

    public string getLogin()
    {
        return _login;
    }
    public string getMotDePasse()
    {
        return _motDePasse;
    }

    public EcranConnexion(SpriteFont police, GraphicsDevice graphicsDevice)
    {
        _police = police;
        _boutonConnexion = new Rectangle(200, 300, 150, 50);
        _boutonInscription = new Rectangle(400, 300, 150, 50);
        //_utilisateurs = ListeUtilisateurs.ChargerDepuisXml(_cheminXml);
        _joueurs = new SerialisableJoueurs();
        _joueurs.DeserialiserJoueurs(_cheminXml); 

        // Création d'une texture blanche de 1x1 pixel
        _texturePixel = new Texture2D(graphicsDevice, 1, 1);
        _texturePixel.SetData(new[] { Color.White });

        _dernieresTouches = new Keys[0]; // Initialisation des touches pressées
    }

    public void MettreAJour(GameTime tempsDeJeu, MouseState souris, KeyboardState clavier, JeuPrincipal jeu)
    {
       

        if (_boutonConnexion.Contains(souris.Position))
        {
            if (_joueurs.VerifierUtilisateur(_login, _motDePasse))
            {
                Connecte = true;
                jeu.DemarrerJeu(); // Lancer le jeu si connexion réussie
                Console.WriteLine("Connexion reussie.");
            }
            else
            {
                Console.WriteLine("Connexion echouee : login ou mot de passe incorrect.");
            }
        }
        else if (_boutonInscription.Contains(souris.Position))
        {
            _joueurs.AjouterUtilisateur(_cheminXml,_login, _motDePasse,0);
            //_utilisateurs.SauvegarderEnXml(_cheminXml);
        }

        // Gestion de la saisie clavier
        var touchesActuelles = clavier.GetPressedKeys();
        foreach (var touche in touchesActuelles)
        {
            // Éviter la répétition des touches
            if (Array.Exists(_dernieresTouches, t => t == touche))
                continue;

            if (touche == Keys.Back) // Effacer un caractère
            {
                if (_saisieLogin && _login.Length > 0)
                    _login = _login.Substring(0, _login.Length - 1);
                else if (!_saisieLogin && _motDePasse.Length > 0)
                    _motDePasse = _motDePasse.Substring(0, _motDePasse.Length - 1);
            }
            else if (touche == Keys.Tab) // Passer de login à mot de passe
            {
                _saisieLogin = !_saisieLogin;
            }
            else
            {
                // Ajouter un caractère, respect des majuscules/minuscules
                string caractere = ToucheVersCaractere(touche, clavier);

                if (_saisieLogin)
                    _login += caractere;
                else
                    _motDePasse += caractere;
            }
        }

        _dernieresTouches = touchesActuelles; // Mettre à jour les touches pressées
    }

    private string ToucheVersCaractere(Keys touche, KeyboardState clavier)
    {
        bool shiftEnfonce = clavier.IsKeyDown(Keys.LeftShift) || clavier.IsKeyDown(Keys.RightShift);

        // Associer la touche au caractère correspondant
        if (touche >= Keys.A && touche <= Keys.Z)
        {
            return shiftEnfonce ? touche.ToString() : touche.ToString().ToLower();
        }

        // Gestion des chiffres (0-9)
        if (touche >= Keys.D0 && touche <= Keys.D9)
        {
            string caractere = touche.ToString().Replace("D", "");
            return shiftEnfonce ? ObtenirCaractereSpecial(caractere) : caractere;
        }

        return ""; // Retourne une chaîne vide pour les touches non prises en charge
    }

    private string ObtenirCaractereSpecial(string chiffre)
    {
        // Exemple : Shift + 1 = "!", Shift + 2 = "@"
        switch (chiffre)
        {
            case "1": return "!";
            case "2": return "@";
            case "3": return "#";
            case "4": return "$";
            case "5": return "%";
            case "6": return "^";
            case "7": return "&";
            case "8": return "*";
            case "9": return "(";
            case "0": return ")";
            default: return chiffre;
        }
    }

    public void Dessiner(SpriteBatch crayon)
    {
        crayon.Begin(); // Commencer le dessin

        // Afficher le champ login
        crayon.DrawString(_police, "Login :", new Vector2(200, 100), Color.White);
        crayon.DrawString(_police, _login, new Vector2(300, 100), Color.White);

        // Afficher le champ mot de passe
        crayon.DrawString(_police, "Mot de passe :", new Vector2(200, 200), Color.White);
        crayon.DrawString(_police, _afficherMotDePasse ? _motDePasse : new string('*', _motDePasse.Length),
            new Vector2(300, 200), Color.White);

        // Dessiner les boutons
        DrawRectangle(crayon, _boutonConnexion, Color.Green);
        crayon.DrawString(_police, "Se connecter", new Vector2(210, 310), Color.White);

        DrawRectangle(crayon, _boutonInscription, Color.Blue);
        crayon.DrawString(_police, "S'inscrire", new Vector2(410, 310), Color.White);

        crayon.End(); // Fin du dessin
    }

    public void DrawRectangle(SpriteBatch spriteBatch, Rectangle rectangle, Color couleur)
    {
        // Dessiner les 4 côtés du rectangle en utilisant des lignes
        spriteBatch.Draw(_texturePixel, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, 1), couleur); // Ligne supérieure
        spriteBatch.Draw(_texturePixel, new Rectangle(rectangle.X, rectangle.Y, 1, rectangle.Height), couleur); // Ligne gauche
        spriteBatch.Draw(_texturePixel,
            new Rectangle(rectangle.X, rectangle.Y + rectangle.Height - 1, rectangle.Width, 1), couleur); // Ligne inférieure
        spriteBatch.Draw(_texturePixel,
            new Rectangle(rectangle.X + rectangle.Width - 1, rectangle.Y, 1, rectangle.Height), couleur); // Ligne droite
    }
}
}