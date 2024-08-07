﻿using System.Collections.Generic;

namespace KENGINE
{
    public class Input
    {
        public static float mouseX, mouseY = 0;
        public static float mouseOffsetX, mouseOffsetY = 0;
        public static Dictionary<KeyCode, bool> lastKeys = new Dictionary<KeyCode, bool>();
        public static Dictionary<KeyCode, bool> currentKeys = new Dictionary<KeyCode, bool>();

        public static Dictionary<MouseButton, bool> lastMouse = new Dictionary<MouseButton, bool>();
        public static Dictionary<MouseButton, bool> currentMouse = new Dictionary<MouseButton, bool>();

        public static void Update()
        {
            lastKeys = new Dictionary<KeyCode, bool>();
            foreach(KeyCode key in currentKeys.Keys)
            {
                lastKeys.Add(key, currentKeys[key]);
            }

            lastMouse = new Dictionary<MouseButton, bool>();
            foreach (MouseButton mb in currentMouse.Keys)
            {
                lastMouse.Add(mb, currentMouse[mb]);
            }
            mouseOffsetX = mouseX;
            mouseOffsetY = mouseY;
        }
        public static bool GetKeyDown(KeyCode keyCode)
        {
            if (currentKeys.ContainsKey(keyCode))
            {
                if (currentKeys[keyCode])
                {
                    if (lastKeys.ContainsKey(keyCode))
                    {
                        if (!lastKeys[keyCode])
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public static bool GetKey(KeyCode keyCode)
        {
            if (currentKeys.ContainsKey(keyCode))
            {
                if (currentKeys[keyCode])
                {
                    return true;
                }
            }
            return false;
        }
        public static bool GetKeyUp(KeyCode keyCode)
        {
            if (lastKeys.ContainsKey(keyCode))
            {
                if (lastKeys[keyCode])
                {
                    if (currentKeys.ContainsKey(keyCode))
                    {
                        if (!currentKeys[keyCode])
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public static bool GetMouseButtonDown(MouseButton mouseButton)
        {
            if (currentMouse.ContainsKey(mouseButton))
            {
                if (currentMouse[mouseButton])
                {
                    if (lastMouse.ContainsKey(mouseButton))
                    {
                        if (!lastMouse[mouseButton])
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public static bool GetMouseButton(MouseButton mouseButton)
        {
            if (currentMouse.ContainsKey(mouseButton))
            {
                if (currentMouse[mouseButton])
                {
                    return true;
                }
            }
            return false;
        }
        public static bool GetMouseButtonUp(MouseButton mouseButton)
        {
            if (lastMouse.ContainsKey(mouseButton))
            {
                if (lastMouse[mouseButton])
                {
                    if (currentMouse.ContainsKey(mouseButton))
                    {
                        if (!currentMouse[mouseButton])
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public static float GetMouseX()
        {
            return mouseX;
        }
        public static float GetMouseY()
        {
            return mouseY;
        }
        public static float GetMouseDeltaX()
        {
            return mouseX - mouseOffsetX;
        }
        public static float GetMouseDeltaY()
        {
            return mouseY - mouseOffsetY;
        }
    }
    public enum KeyCode
    {
        Unknown = -1,
        Space = 0x20,
        Apostrophe = 39,
        Comma = 44,
        Minus = 45,
        Period = 46,
        Slash = 47,
        D0 = 48,
        D1 = 49,
        D2 = 50,
        D3 = 51,
        D4 = 52,
        D5 = 53,
        D6 = 54,
        D7 = 55,
        D8 = 56,
        D9 = 57,
        Semicolon = 59,
        Equal = 61,
        A = 65,
        B = 66,
        C = 67,
        D = 68,
        E = 69,
        F = 70,
        G = 71,
        H = 72,
        I = 73,
        J = 74,
        K = 75,
        L = 76,
        M = 77,
        N = 78,
        O = 79,
        P = 80,
        Q = 81,
        R = 82,
        S = 83,
        T = 84,
        U = 85,
        V = 86,
        W = 87,
        X = 88,
        Y = 89,
        Z = 90,
        LeftBracket = 91,
        Backslash = 92,
        RightBracket = 93,
        GraveAccent = 96,
        Escape = 0x100,
        Enter = 257,
        Tab = 258,
        Backspace = 259,
        Insert = 260,
        Delete = 261,
        Right = 262,
        Left = 263,
        Down = 264,
        Up = 265,
        PageUp = 266,
        PageDown = 267,
        Home = 268,
        End = 269,
        CapsLock = 280,
        ScrollLock = 281,
        NumLock = 282,
        PrintScreen = 283,
        Pause = 284,
        F1 = 290,
        F2 = 291,
        F3 = 292,
        F4 = 293,
        F5 = 294,
        F6 = 295,
        F7 = 296,
        F8 = 297,
        F9 = 298,
        F10 = 299,
        F11 = 300,
        F12 = 301,
        F13 = 302,
        F14 = 303,
        F15 = 304,
        F16 = 305,
        F17 = 306,
        F18 = 307,
        F19 = 308,
        F20 = 309,
        F21 = 310,
        F22 = 311,
        F23 = 312,
        F24 = 313,
        F25 = 314,
        KeyPad0 = 320,
        KeyPad1 = 321,
        KeyPad2 = 322,
        KeyPad3 = 323,
        KeyPad4 = 324,
        KeyPad5 = 325,
        KeyPad6 = 326,
        KeyPad7 = 327,
        KeyPad8 = 328,
        KeyPad9 = 329,
        KeyPadDecimal = 330,
        KeyPadDivide = 331,
        KeyPadMultiply = 332,
        KeyPadSubtract = 333,
        KeyPadAdd = 334,
        KeyPadEnter = 335,
        KeyPadEqual = 336,
        LeftShift = 340,
        LeftControl = 341,
        LeftAlt = 342,
        LeftSuper = 343,
        RightShift = 344,
        RightControl = 345,
        RightAlt = 346,
        RightSuper = 347,
        Menu = 348,
        LastKey = 348
    }
    public enum MouseButton
    {
        None = 0,
        Left = 1048576,
        Right = 2097152,
        Middle = 4194304,
        XButton1 = 8388608,
        XButton2 = 16777216
    }
}
