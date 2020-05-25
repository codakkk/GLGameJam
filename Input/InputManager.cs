using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLJamGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GLGameJam.Input
{
    // Porca madonna?
    public enum MouseAction
    {
        None,
        Left, 
        Middle,
        Right
    }

    public class KeyMap
    {
        public Keys Key { get; set; } = Keys.None;
        public MouseAction MouseAction { get; set; } = MouseAction.None;

    }

    // Singleton? porcodio
    public class InputManager
    {

        private readonly Dictionary<string, KeyMap> keyMap;

        private KeyboardState lastKeyboardState;
        private MouseState lastMouseState;

        public Point MousePosition
        {
            get
            {
                var (x, y) = Mouse.GetState().Position;
                return new Point((int)(x* CoreGame.GameScaleX), (int) (y* CoreGame.GameScaleY));
            }
        }


        public InputManager()
        {
            keyMap = new Dictionary<string, KeyMap>();

            Register("shop_refresh", new KeyMap() { Key = Keys.R });
            Register("shop_buyexp", new KeyMap() { Key = Keys.F });
        }

        public void Update(GameTime gameTime)
        {
            lastKeyboardState = Keyboard.GetState();
            lastMouseState = Mouse.GetState();
        }

        public void DrawDebug(CustomBatch customBatch)
        {
            //customBatch.DrawPixelString(new Vector2(0, 0), $"{MousePosition.X}:{MousePosition.Y}", Color.White);
        }

        public bool IsActionDown(string actionName)
        {
            if (!keyMap.ContainsKey(actionName))
                return false;
            var kMap = keyMap[actionName];
            var input = Keyboard.GetState().IsKeyDown(kMap.Key) || IsMouseButtonPressed(Mouse.GetState(), kMap.MouseAction);
            return input;
        }

        public bool IsActionJustDown(string actionName)
        {
            if (!keyMap.ContainsKey(actionName))
                return false;
            var kMap = keyMap[actionName];
            var keyState = lastKeyboardState[kMap.Key] != KeyState.Down && Keyboard.GetState().IsKeyDown(kMap.Key);
            var mouseState = !IsMouseButtonPressed(lastMouseState, kMap.MouseAction) && IsMouseButtonPressed(Mouse.GetState(), kMap.MouseAction);
            return keyState || mouseState;
        }

        public bool IsActionUp(string actionName)
        {
            if (!keyMap.ContainsKey(actionName))
                return false;
            var kMap = keyMap[actionName];
            var input = Keyboard.GetState().IsKeyUp(kMap.Key) || !IsMouseButtonPressed(Mouse.GetState(), kMap.MouseAction);
            return input;
        }

        public bool IsActionJustUp(string actionName)
        {
            if (!keyMap.ContainsKey(actionName))
                return false;
            var kMap = keyMap[actionName];
            var keyState = lastKeyboardState[kMap.Key] == KeyState.Down && Keyboard.GetState().IsKeyUp(kMap.Key);
            var mouseState = IsMouseButtonPressed(lastMouseState, kMap.MouseAction) && !IsMouseButtonPressed(Mouse.GetState(), kMap.MouseAction);
            return keyState || mouseState;
        }


        public bool IsKeyJustPressed(Keys key)
        {
            return lastKeyboardState[key] != KeyState.Down && Keyboard.GetState().IsKeyDown(key);
        }

        private void Register(string actionName, KeyMap key)
        {
            if (!keyMap.ContainsKey(actionName))
            {
                keyMap.Add(actionName, key);
            }
        }

        private bool IsMouseButtonPressed(MouseState state, MouseAction mouseAction)
        {
            switch (mouseAction)
            {
                case MouseAction.Left:
                    return state.LeftButton == ButtonState.Pressed;
                case MouseAction.Middle:
                    return state.MiddleButton == ButtonState.Pressed;
                case MouseAction.Right:
                    return state.RightButton == ButtonState.Pressed;
                case MouseAction.None:
                    return false;
                default:
                    return false;
            }
        }
    }
}
