using Microsoft.Xna.Framework;

namespace CSdoom.Engine
{
    public class Camera
    {
        public Vector3 position;
        public float rotation;
        public float FOV = 90f;
        public Matrix view, perspective;

        public Camera(Vector2 position, float height, float rotation, Game1 game)
        {
            Update(game);
        }

        public void Update(Game1 game)
        {
            Matrix rot = Matrix.CreateRotationY(MathHelper.ToRadians(rotation));
            Vector3 offset = Vector3.Transform(Vector3.UnitZ, rot);

            view = Matrix.CreateLookAt(position, position + offset, Vector3.Up);
            perspective = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(FOV), game.AspectRatio, 0.05f, 1000f);
        }
    }
}