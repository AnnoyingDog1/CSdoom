using CSdoom.Engine;
using Microsoft.Xna.Framework;
using static Microsoft.Xna.Framework.MathHelper;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSdoom.Assets
{
    public class Projectile : Entity
    {
        public float speed;
        public Projectile(Texture2D texture, Game1 game) : base(texture, game)
        {
            speed = 2f;
        }
        public void Update(Game1 game)
        {
            base.Update(game);
            Vector3 direction = new Vector3(MathF.Sin(ToRadians(rotation)), 0, MathF.Cos(ToRadians(rotation)));
            position += direction * speed;
           

        }
        public void Draw(Game1 game)
        {
            base.Draw(game);

            

            
        }
    }

  
}
