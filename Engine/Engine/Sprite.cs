using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CSdoom.Engine
{
    public class Sprite
    {
        public Texture2D texture;
        public Vector2 position;
        public Vector2 scale;
        public float rotation;

        public Sprite(Texture2D texture)
        {
            this.texture = texture;
            position = Vector2.Zero;
            scale = Vector2.One;
        }

        public Sprite(string assetName, Game1 game)
        {
            texture = game.Content.Load<Texture2D>(assetName);
            position = Vector2.Zero;
            scale = Vector2.One;
        }

        public void Update(Game1 game)
        {
            
        }

        public void Draw(Game1 game)
        {
            game.spriteBatch.Draw(texture, new Rectangle(position.ToPoint(), texture.Bounds.Size * scale.ToPoint()), Color.White);
        }
    }
}