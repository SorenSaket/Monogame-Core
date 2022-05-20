using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public static class Input
    {
        public class Player
        {
            public GamePadState CurrentState = GamePadState.Default;
            public GamePadState PreviousState = GamePadState.Default;

            public Player()
            {
                CurrentState = GamePadState.Default;
                PreviousState = GamePadState.Default;
            }
        }

        private static Player[] internalStates;

        public static event Action<int> OnControllerConnected;
        public static  event Action<int> OnControllerDisconnected;

        public static void Initialize()
        {
            internalStates = new Player[GamePad.MaximumGamePadCount];
            for (int i = 0; i < internalStates.Length; i++)
            {
                internalStates[i] = new Player();
            }
        }
        public static void Update()
        {
            for (int i = 0; i < GamePad.MaximumGamePadCount; i++)
            {
                internalStates[i].PreviousState = internalStates[i].CurrentState;
                internalStates[i].CurrentState = GamePad.GetState(i);

                if(internalStates[i].CurrentState.IsConnected && !internalStates[i].PreviousState.IsConnected)
                {
                    OnControllerConnected?.Invoke(i);
                }
                if(!internalStates[i].CurrentState.IsConnected && internalStates[i].PreviousState.IsConnected)
                {
                    OnControllerDisconnected?.Invoke(i);
                }
            }
        }
        public static bool GetButton(Buttons input, int player) 
        {
            return internalStates[player].CurrentState.IsButtonDown(input);
        }
        public static  bool GetButtonDown(Buttons input, int player) 
        {
            return internalStates[player].CurrentState.IsButtonDown(input) && internalStates[player].PreviousState.IsButtonUp(input);
        }
        public static bool GetButtonUp(Buttons input, int player)
        {
            return internalStates[player].CurrentState.IsButtonUp(input) && internalStates[player].PreviousState.IsButtonDown(input);
        }


        public static float GetAxisRaw(Buttons input, int player)
        {
            switch (input)
            {
                case Buttons.RightTrigger:
                    return internalStates[player].CurrentState.Triggers.Right;
                case Buttons.LeftTrigger:
                    return internalStates[player].CurrentState.Triggers.Left;
                default:
                    break;
            }
            return 0;
        }

        public static Vector2 GetVectorRaw(Buttons input, int player)
        {
            switch (input)
            {
                case Buttons.RightStick:
                    return internalStates[player].CurrentState.ThumbSticks.Right;
                case Buttons.LeftStick:
                    return internalStates[player].CurrentState.ThumbSticks.Left;
                default:
                    break;
            }
            return Vector2.Zero;
        }

    }
}
