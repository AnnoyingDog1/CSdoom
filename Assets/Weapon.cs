using CSdoom.Engine;
using Microsoft.Xna.Framework;
using static Microsoft.Xna.Framework.MathHelper;
using static System.MathF;
using System.Diagnostics;
using System.Collections.Generic;
using System;
using static CSdoom.Engine.Utilities;
using static CSdoom.Engine.Map;
using Microsoft.Xna.Framework.Graphics;

namespace CSdoom.Assets
{
    public class Weapon : Sprite
    {
        public Color debugColor;
        private Vector2 pos;
        public Vector3 rayHitPos;

        public List<Projectile> bullets;
        public Texture2D bulletTexture;


        public Weapon(string assetName, string BulletTexName, Game1 game) : base(assetName, game)
        {
            scale = Vector2.One * 7;
            pos.X = game.ScreenWidth - (scale.X * texture.Width);
            pos.Y = game.ScreenHeight - (scale.Y * texture.Height);
            position = pos;
            bulletTexture = game.Content.Load<Texture2D>(BulletTexName);
            bullets = new List<Projectile>();
        }
        public void Shoot(Game1 game)
        {
            Projectile newBullet = new Projectile(bulletTexture, game);
            newBullet.position = game.player.position + (Vector3.Up*0.7f);
            newBullet.rotation = game.player.rotation;
            newBullet.scale = Vector2.One * 0.1f;
            bullets.Add(newBullet);
          
        }
        public void Draw(Game1 game)
        {
            base.Draw(game);
            foreach (Projectile bullet in bullets) bullet.Draw(game);
        }
        public void Update(Game1 game)
        {
            var player = game.player;
            float speed = Utilities.Magnitude(new Vector2(player.velocity.X, player.velocity.Z));
            Vector2 Offset = Vector2.UnitY * (speed * Sin(10 * game.elapsedTime) * 200);
            position = pos - Offset;

            foreach (Projectile bullet in bullets) bullet.Update(game);
        }

    }
}