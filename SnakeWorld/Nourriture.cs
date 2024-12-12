using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SerpentJeu
{
    public class Nourriture
    {
        private Vector2 _position;
        private int _tailleCellule;
        private Texture2D _texture;

        public Nourriture(int tailleCellule)
        {
            _tailleCellule = tailleCellule;
            GenererPositionAleatoire(20, 20);
        }

        public void ChargerContenu(GraphicsDevice appareilGraphique)
        {
            _texture = new Texture2D(appareilGraphique, _tailleCellule, _tailleCellule);
            Color[] couleur = new Color[_tailleCellule * _tailleCellule];
            for (int i = 0; i < couleur.Length; i++) couleur[i] = Color.Red;
            _texture.SetData(couleur);
        }

        public void GenererPositionAleatoire(int largeurGrille, int hauteurGrille)
        {
            var random = new Random();
            _position = new Vector2(random.Next(largeurGrille), random.Next(hauteurGrille));
        }

        public void Dessiner(SpriteBatch crayon)
        {
            crayon.Draw(_texture, _position * _tailleCellule, Color.White);
        }

        public Vector2 RecupererPosition() => _position;
    }
}