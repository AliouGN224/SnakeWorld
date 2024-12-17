using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace SerpentJeu
{
    public class Serpent
    {
        private readonly List<Vector2> _corps;
        private Vector2 _direction;
        private int _tailleCellule;
        private Texture2D _texture;

        public Serpent(int tailleCellule)
        {
            _tailleCellule = tailleCellule;
            _corps = new List<Vector2> { new Vector2(10, 10) };
            _direction = Vector2.UnitX;
        }

        public Vector2 PositionTete => _corps[0];

        public void ChargerContenu(GraphicsDevice appareilGraphique)
        {
            _texture = new Texture2D(appareilGraphique, _tailleCellule, _tailleCellule);
            Color[] couleur = new Color[_tailleCellule * _tailleCellule];
            for (int i = 0; i < couleur.Length; i++) couleur[i] = Color.Yellow;
            _texture.SetData(couleur);
        }

        public void MettreAJour()
        {
            for (int i = _corps.Count - 1; i > 0; i--)
            {
                _corps[i] = _corps[i - 1];
            }
            _corps[0] += _direction;
        }

        public void Dessiner(SpriteBatch crayon)
        {
            foreach (var segment in _corps)
            {
                crayon.Draw(_texture, segment * _tailleCellule, Color.White);
            }
        }

        public void Agrandir()
        {
            _corps.Add(_corps[^1]);
        }

        public void ChangerDirection(Vector2 nouvelleDirection)
        {
            if (nouvelleDirection != -_direction)
                _direction = nouvelleDirection;
        }

        public bool DetecterCollision()
        {
            for (int i = 1; i < _corps.Count; i++)
            {
                if (_corps[0] == _corps[i])
                    return true;
            }
            return false;
        }

        public bool HorsLimites(int largeur, int hauteur)
        {
            var tete = _corps[0];
            return tete.X < 0 || tete.Y < 0 || tete.X >= largeur || tete.Y >= hauteur;
        }
    }
}
