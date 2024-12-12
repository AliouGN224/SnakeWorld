using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Xml.Serialization;
using System.Xml.Xsl;
using System;
namespace SerpentJeu
{
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

        public JeuPrincipal()
        {
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

                if (_serpent.DetecterCollision() || 
                    _serpent.HorsLimites(GraphicsDevice.Viewport.Width / 20, GraphicsDevice.Viewport.Height / 20))
                {
                    _jeuTermine = true;
                    //SauvegarderEtatJeu();
                }

                if (Vector2.Distance(_serpent.PositionTete, _nourriture.RecupererPosition()) < 1)
                {
                    _serpent.Agrandir();
                    _nourriture.GenererPositionAleatoire(GraphicsDevice.Viewport.Width / 20, GraphicsDevice.Viewport.Height / 20);
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
}
