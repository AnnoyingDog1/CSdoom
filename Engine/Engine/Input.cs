using CSdoom;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Biking_Badass.Engine
{
    public static class Input
    {
        private static bool isKeyDown = false;
        private static bool isMouseDown = false;
        private static bool isMouseUp = true;

        public static bool KeyDown(Keys key)
        {
            return Keyboard.GetState().IsKeyDown(key);
        }

        public static bool KeyPressed(Keys key)
        {
            if (KeyDown(key) && !isKeyDown)
            {
                isKeyDown = true;
                return true;
            }
            isKeyDown = !Keyboard.GetState().IsKeyUp(key);
            return false;
        }

        public static float Axis(Keys neg, Keys pos)
        {
            float val = KeyDown(neg) ? -1 : KeyDown(pos) ? 1 : 0;
            return val;
        }

        public static Vector2 MousePos()
        {
            return Mouse.GetState().Position.ToVector2();
        }

        public static Vector2 MouseDelta(Vector2 lastPos, bool lockMouse)
        {
            Vector2 pos = MousePos();
            if (lockMouse) Mouse.SetPosition(800, 400);
            return pos - lastPos;
        }

       

        public static bool MouseDown(int button)
        {
            if (button == 0) return Mouse.GetState().LeftButton == ButtonState.Pressed;
            if (button == 1) return Mouse.GetState().RightButton == ButtonState.Pressed;
            if (button == 2) return Mouse.GetState().MiddleButton == ButtonState.Pressed;
            else return false;
        }

        public static bool MouseUp(int button)
        {
            if (button == 0) return Mouse.GetState().LeftButton == ButtonState.Released;
            if (button == 1) return Mouse.GetState().RightButton == ButtonState.Released;
            if (button == 2) return Mouse.GetState().MiddleButton == ButtonState.Released;
            else return false;
        }

        public static bool MousePressed(int button)
        {
            if (MouseDown(button) && !isMouseDown)
            {
                isMouseDown = true;
                return true;
            }
            isMouseDown = MouseDown(button);
            return false;
        }

        public static bool MouseReleased(int button)
        {
            if (MouseUp(button) && !isMouseUp)
            {
                isMouseUp = true;
                return true;
            }
            isMouseUp = MouseUp(button);
            return false;
        }
    }
}