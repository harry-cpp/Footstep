using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Footstep
{
    static class InputManager
    {
        private static MouseState _prevMState, _mState;
        private static KeyboardState _prevkState, _kState;
        private static Dictionary<InputKey, Keys> _dictKeys;

        static InputManager()
        {
            _dictKeys = new Dictionary<InputKey, Keys>();
            _dictKeys.Add(InputKey.MoveForward, Keys.W);
            _dictKeys.Add(InputKey.MoveBackwards, Keys.S);
            _dictKeys.Add(InputKey.MoveLeft, Keys.A);
            _dictKeys.Add(InputKey.MoveRight, Keys.D);

            _dictKeys.Add(InputKey.MenuUp, Keys.Up);
            _dictKeys.Add(InputKey.MenuDown, Keys.Down);
            _dictKeys.Add(InputKey.MenuSelect, Keys.Enter);
        }

        public static Vector2 Position => _mState.Position.ToVector2();

        public static void Update()
        {
            _prevkState = _kState;
            _kState = Keyboard.GetState();

            _prevMState = _mState;
            _mState = Mouse.GetState();
        }

        public static bool IsPressed(InputKey key)
        {
            if (_dictKeys.TryGetValue(key, out Keys value))
                return _prevkState.IsKeyUp(value) && _kState.IsKeyDown(value);

            return false;
        }

        public static bool IsReleased(InputKey key)
        {
            if (_dictKeys.TryGetValue(key, out Keys value))
                return _kState.IsKeyUp(value) && _prevkState.IsKeyDown(value);

            return false;
        }

        public static bool IsDown(InputKey key)
        {
            if (_dictKeys.TryGetValue(key, out Keys value))
                return _kState.IsKeyDown(value);

            return false;
        }

        public static bool IsUp(InputKey key)
        {
            if (_dictKeys.TryGetValue(key, out Keys value))
                return _kState.IsKeyUp(value);

            return false;
        }
    }
}
