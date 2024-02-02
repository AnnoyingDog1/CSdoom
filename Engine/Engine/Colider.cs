using Microsoft.Xna.Framework;

namespace CSdoom.Engine
{
    public class Colider
    {
        public Vector2 position, size, topLeft, bottomLeft, topRight, bottomRight, center;
        public float left, right, top, bottom;

        public Colider(Vector2 position, Vector2 size)
        {
            this.position = position;
            this.size = size;
            Update();
        }

        public void Update()
        {
            top = position.Y + size.Y;
            bottom = position.Y;
            left = position.X;
            right = position.X + size.X;

            topLeft = new Vector2(left, top);
            bottomLeft = new Vector2(left, bottom);
            topRight = new Vector2(right, top);
            bottomRight = new Vector2(right, bottom);

            center = (bottomLeft + topRight);
        }
    }
}