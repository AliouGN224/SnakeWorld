 using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Xml.Serialization;
using System.Xml.Xsl;
using System;

namespace SerpentJeu
{
/*
    public class JeuPrincipal : Game
    {
        private GraphicsDeviceManager _gestionGraphique;
        private SpriteBatch _crayon;
        private Serpent _serpent;
        private Nourriture _nourriture;
        private int _score;
        private bool _jeuTermine;
        private float _tempsDepuisDernierMouvement;
        private float _delaiMouvement;
        private static int _tailleCelluleGril = 20;

        public JeuPrincipal()
        {
            _gestionGraphique = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // Initialisation des dimensions de la grille
<<<<<<< HEAD
            int tailleCelluleGril = 20; // Taille d'une cellule (en pixels)
            int largeGril = GraphicsDevice.Viewport.Width / tailleCelluleGril; // Largeur de la grille
            int HautGrill = GraphicsDevice.Viewport.Height / tailleCelluleGril; // Hauteur de la grill


            _serpent = new Serpent(tailleCelluleGril);
            _nourriture = new Nourriture(tailleCelluleGril);
            _nourriture.GenererPositionAleatoire(largeGril, HautGrill);
=======
             // Taille d'une cellule (en pixels)
            int largeGril = GraphicsDevice.Viewport.Width / _tailleCelluleGril; // Largeur de la grille
            int HautGrill = GraphicsDevice.Viewport.Height / _tailleCelluleGril; // Hauteur de la grill
            

            _serpent = new Serpent(_tailleCelluleGril);
            _nourriture = new Nourriture(_tailleCelluleGril);
            _nourriture.GenererPositionAleatoire(largeGril, HautGrill); 
>>>>>>> 03595a98ee8d667994da9998f16c26bd7c820bf5
            _score = 0;
            _jeuTermine = false;
            _tempsDepuisDernierMouvement = 1f;
            _delaiMouvement = 0.2f; // Délai entre les mouvements du serpent

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _crayon = new SpriteBatch(GraphicsDevice);
            _serpent.ChargerContenu(GraphicsDevice);
            _nourriture.ChargerContenu(GraphicsDevice);
        }

        protected override void Update(GameTime tempsDeJeu)
        {
            if (_jeuTermine)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    Initialize();
                }
                return;
            }

            _tempsDepuisDernierMouvement += (float)tempsDeJeu.ElapsedGameTime.TotalSeconds;

            if (_tempsDepuisDernierMouvement >= _delaiMouvement)
            {
                _tempsDepuisDernierMouvement = 0f;

                var etatClavier = Keyboard.GetState();
                if (etatClavier.IsKeyDown(Keys.Up)) _serpent.ChangerDirection(Vector2.UnitY * -1);
                if (etatClavier.IsKeyDown(Keys.Down)) _serpent.ChangerDirection(Vector2.UnitY);
                if (etatClavier.IsKeyDown(Keys.Left)) _serpent.ChangerDirection(Vector2.UnitX * -1);
                if (etatClavier.IsKeyDown(Keys.Right)) _serpent.ChangerDirection(Vector2.UnitX);

                _serpent.MettreAJour();

<<<<<<< HEAD
                if (_serpent.DetecterCollision() ||
                    _serpent.HorsLimites(GraphicsDevice.Viewport.Width / 20, GraphicsDevice.Viewport.Height / 20))
=======
                if (_serpent.DetecterCollision() || 
                    _serpent.HorsLimites(GraphicsDevice.Viewport.Width / _tailleCelluleGril, GraphicsDevice.Viewport.Height / _tailleCelluleGril))
>>>>>>> 03595a98ee8d667994da9998f16c26bd7c820bf5
                {
                    _jeuTermine = true;
                    //SauvegarderEtatJeu();
                }

                if (Vector2.Distance(_serpent.PositionTete, _nourriture.RecupererPosition()) < 1)
                {
                    _serpent.Agrandir();
                    _nourriture.GenererPositionAleatoire(GraphicsDevice.Viewport.Width / _tailleCelluleGril, GraphicsDevice.Viewport.Height / _tailleCelluleGril);
                    _score += 10;
                }
            }

            base.Update(tempsDeJeu);
        }

        protected override void Draw(GameTime tempsDeJeu)
        {
            GraphicsDevice.Clear(Color.Black);

            _crayon.Begin();
            _serpent.Dessiner(_crayon);
            _nourriture.Dessiner(_crayon);

            var police = Content.Load<SpriteFont>("DefaultFont");
            _crayon.DrawString(police, $"Score: {_score}", new Vector2(10, 10), Color.White);

            if (_jeuTermine)
            {
                string messageFin = "Jeu termine ! Appuyez sur ESPACE pour recommencer.";
                var tailleTexte = police.MeasureString(messageFin);
                var centreEcran = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
                _crayon.DrawString(police, messageFin, centreEcran - tailleTexte / 2, Color.Red);
            }

            _crayon.End();

            base.Draw(tempsDeJeu);
        }

        private void SauvegarderEtatJeu()
        {
            var etatJeu = new EtatJeu
            {
                Score = _score,
                Statut = _jeuTermine ? "Terminé" : "En cours",
                Horodatage = DateTime.Now
                
            };

            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(EtatJeu));
            using (var ecrivain = new System.IO.StreamWriter("EtatJeu.xml"))
            {
                serializer.Serialize(ecrivain, etatJeu);
            }
        }
    }
}*/
 


public class JeuPrincipal : Game
{
    private GraphicsDeviceManager _gestionGraphique;
   
    private SpriteBatch _crayon;
    private string _cheminXml = "xml/joueurs.xml";

    // État du jeu
    private bool _jeuEnCours = false; // Indique si le joueur est en train de jouer
    private bool _jeuTermine = false;

    // Classes principales
    private EcranConnexion _ecranConnexion;
    private Serpent _serpent;
    private Nourriture _nourriture;

    // Score et temps
    private int _score = 0;
    private float _tempsDepuisDernierMouvement = 0f;
    private float _delaiMouvement = 0.2f; // Vitesse initiale du serpent

    // Police pour affichage de texte
    private SpriteFont _police;
    private SerialisableJoueurs _joueurs=new SerialisableJoueurs();
    //_joueurs = new SerialisableJoueurs();
    public JeuPrincipal()
    {
        //_joueurs = new SerialisableJoueurs();
        _gestionGraphique = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // Initialisation des dimensions de la grille
        int tailleCelluleGril = 20; // Taille d'une cellule (en pixels)
        int largeGril = GraphicsDevice.Viewport.Width / tailleCelluleGril; // Largeur de la grille
        int HautGrill = GraphicsDevice.Viewport.Height / tailleCelluleGril; // Hauteur de la grill


        _serpent = new Serpent(tailleCelluleGril);
        _nourriture = new Nourriture(tailleCelluleGril);
        _nourriture.GenererPositionAleatoire(largeGril, HautGrill);
        _score = 0;
        _jeuTermine = false;
        _tempsDepuisDernierMouvement = 0f;
        _delaiMouvement = 0.2f; // Délai entre les mouvements du serpent
        _joueurs.DeserialiserJoueurs(_cheminXml); // Charger les données existantes


        base.Initialize();
    }

    protected override void LoadContent()
    {
        _crayon = new SpriteBatch(GraphicsDevice);

        // Charger la police pour les affichages
        _police = Content.Load<SpriteFont>("DefaultFont");

        // Initialisation des écrans
        _ecranConnexion = new EcranConnexion(_police,GraphicsDevice);

        // Initialisation du jeu Snake
        _serpent = new Serpent(20);
        _nourriture = new Nourriture(20);

        // Charger les contenus graphiques pour les objets du jeu
        _serpent.ChargerContenu(GraphicsDevice);
        _nourriture.ChargerContenu(GraphicsDevice);
    }
    
    private void MettreAJourMeilleurScore()
    {
        // Charger les données des joueurs depuis le fichier XML
        _joueurs.DeserialiserJoueurs(_cheminXml);

        // Trouver le joueur correspondant à l'utilisateur connecté
        foreach (var joueur in _joueurs.Joueurs)
        {
            if (joueur.Login == _ecranConnexion.getLogin() && joueur.MotDePasse == _ecranConnexion.getMotDePasse())
            {
                // Mettre à jour le meilleur score si le score actuel est supérieur
                if (_score > joueur.MeilleurScore)
                {
                    joueur.MeilleurScore = _score;
                    Console.WriteLine($"Nouveau meilleur score pour {joueur.Login} : {_score}");
                }
                break;
            }
        }

        // Sérialiser les données mises à jour dans le fichier XML
        _joueurs.SerialiserJoueurs(_cheminXml);
        XMLUtils.XslTransform("xml/joueurs.xml","xslt/joueurs.xslt","html/joueurs.html");
    }


    protected override void Update(GameTime tempsDeJeu)
    {
        var clavier = Keyboard.GetState();
        var souris = Mouse.GetState();

        // Si le joueur est sur l'écran de connexion
        if (!_jeuEnCours)
        {
            _ecranConnexion.MettreAJour(tempsDeJeu, souris, clavier,this);
            return;
        }

        // Si le jeu est terminé, afficher une option pour recommencer
        if (_jeuTermine)
        {
            
            MettreAJourMeilleurScore();
            
            if (clavier.IsKeyDown(Keys.Space))
            {
                InitialiserJeu();
            }
            return;
        }

        // Mise à jour du jeu Snake
        _tempsDepuisDernierMouvement += (float)tempsDeJeu.ElapsedGameTime.TotalSeconds;

        if (_tempsDepuisDernierMouvement >= _delaiMouvement)
        {
            _tempsDepuisDernierMouvement = 0f;

            // Gestion des directions
            if (clavier.IsKeyDown(Keys.Up)) _serpent.ChangerDirection(Vector2.UnitY * -1);
            if (clavier.IsKeyDown(Keys.Down)) _serpent.ChangerDirection(Vector2.UnitY);
            if (clavier.IsKeyDown(Keys.Left)) _serpent.ChangerDirection(Vector2.UnitX * -1);
            if (clavier.IsKeyDown(Keys.Right)) _serpent.ChangerDirection(Vector2.UnitX);

            // Mise à jour du serpent
            _serpent.MettreAJour();

            // Vérification des collisions
            if (_serpent.DetecterCollision() ||
                _serpent.HorsLimites(GraphicsDevice.Viewport.Width / 20, GraphicsDevice.Viewport.Height / 20))
            {
                _jeuTermine = true;
            }

            // Vérification si le serpent mange la nourriture
            if (Vector2.Distance(_serpent.PositionTete, _nourriture.RecupererPosition()) < 1)
            {
                _serpent.Agrandir();
                _nourriture.GenererPositionAleatoire(GraphicsDevice.Viewport.Width / 20, GraphicsDevice.Viewport.Height / 20);
                _score += 10;
                _delaiMouvement = Math.Max(0.05f, _delaiMouvement - 0.01f); // Augmenter la vitesse
            }
        }

        base.Update(tempsDeJeu);
    }

    public void DemarrerJeu()
    {
        // Passe à l'écran du jeu Snake
        _jeuEnCours = true;
        InitialiserJeu();
    }

    private void InitialiserJeu()
    {
        // Réinitialisation des variables du jeu
        _serpent = new Serpent(20);
        _serpent.ChargerContenu(GraphicsDevice);

        _nourriture = new Nourriture(20);
        _nourriture.ChargerContenu(GraphicsDevice);

        _score = 0;
        _jeuTermine = false;
        _tempsDepuisDernierMouvement = 0f;
        _delaiMouvement = 0.15f; // Réinitialisation de la vitesse
    }

    protected override void Draw(GameTime tempsDeJeu)
    {
        GraphicsDevice.Clear(Color.Black);


        if (!_jeuEnCours)
        {
            // Afficher l'écran de connexion
            _ecranConnexion.Dessiner(_crayon);
        }
        else
        {
            _crayon.Begin();
            // Dessiner le jeu Snake
            _serpent.Dessiner(_crayon);
            _nourriture.Dessiner(_crayon);

            // Afficher le score
            _crayon.DrawString(_police, $"Score : {_score}", new Vector2(10, 10), Color.White);

            // Afficher le message de fin de partie si le jeu est terminé
            if (_jeuTermine)
            {
                string messageFin = "Jeu termine ! Appuyez sur ESPACE pour recommencer.";
                var tailleTexte = _police.MeasureString(messageFin);
                var centreEcran = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
                _crayon.DrawString(_police, messageFin, centreEcran - tailleTexte / 2, Color.Red);
            }
            _crayon.End();
        }


        base.Draw(tempsDeJeu);
    }
}
}