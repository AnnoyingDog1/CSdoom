using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.Xna.Framework.MathHelper;

namespace CSdoom.Engine
{
    public class Entity
    {
        public Matrix transform;

        public Vector3 position;
        public Vector2 scale;
        public float rotation;

        public Texture2D texture;
        public BasicEffect effect;

        VertexBuffer buffer;
        VertexPositionTexture[] corners;

        public Entity(Texture2D texture, Game1 game)
        {
            this.texture = texture;
            effect = new BasicEffect(game.GraphicsDevice);
            
            this.position = Vector3.Zero;
            this.rotation = 0;
            this.scale = Vector2.One;
            transform = Matrix.CreateScale(new Vector3(scale, 0)) * Matrix.CreateRotationY(ToRadians(rotation)) * Matrix.CreateTranslation(position);

            buffer = new VertexBuffer(game.GraphicsDevice, VertexPositionTexture.VertexDeclaration, 6, BufferUsage.None);
            corners = new VertexPositionTexture[] {
                new VertexPositionTexture(Vector3.UnitY, Vector2.UnitY),
                new VertexPositionTexture(new Vector3(1,1,0), Vector2.One),
                new VertexPositionTexture(Vector3.Zero, Vector2.Zero),

                new VertexPositionTexture(new Vector3(1,1,0), Vector2.One),
                new VertexPositionTexture(Vector3.UnitX, Vector2.UnitX),
                new VertexPositionTexture(Vector3.Zero, Vector2.Zero),
        };
            buffer.SetData<VertexPositionTexture>(corners);

        
            effect.TextureEnabled = true;
            effect.Texture = texture;
        }

        public void Update(Game1 game)
        {
            
            
        }
        public void Draw(Game1 game)
        {
            transform = Matrix.CreateScale(new Vector3(scale, 0)) * Matrix.CreateRotationY(ToRadians(rotation)) * Matrix.CreateTranslation(position);
           
            effect.View = game.player.camera.view;
            effect.Projection = game.player.camera.perspective;
            effect.World = transform;
            

            game.GraphicsDevice.SetVertexBuffer(buffer);
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                game.GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, 2);
            }
            
        }

    }
}
