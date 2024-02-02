using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSdoom.Engine
{

    public class ParticleSystem
    {

        public Vector3 startPosition;
        public VertexPositionTexture[] vertexData;
        public BasicEffect effect;

        Random random;


        public List<Particle> particles;


        public struct Particle
        {
            public Vector3 position;
            public Matrix transform;
        }

        public ParticleSystem(Game1 game)
        {
            effect = new BasicEffect(game.GraphicsDevice);
            random = new Random();
            vertexData = new VertexPositionTexture[]
            {

                new VertexPositionTexture(Vector3.UnitY, Vector2.UnitY),
                new VertexPositionTexture(new Vector3(Vector2.One, 0), Vector2.One),
                new VertexPositionTexture(Vector3.Zero, Vector2.Zero),
                new VertexPositionTexture(Vector3.UnitX, Vector2.UnitX),
            };

        }
        public void Update(Game1 game)
        {
            var cam = game.player.camera;
            effect.View = cam.view;
            effect.Projection = cam.perspective;
            
            startPosition = Vector3.Forward * game.elapsedTime;
        }
        public void Draw(Game1 game)
        {
            var cam = game.player.camera;

            Matrix origin = Matrix.CreateTranslation(startPosition);

            effect.CurrentTechnique.Passes[0].Apply();
            game.GraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, vertexData, 0, 4, new int[] { 0, 1, 2, 3, 2, 1 }, 0, 2);

        }
    }

}
