// Type: Microsoft.Xna.Framework.Input.GamePad
// Assembly: Microsoft.Xna.Framework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=842cf8be1de50553
// Assembly location: C:\Program Files (x86)\Microsoft XNA\XNA Game Studio\v4.0\References\Windows\x86\Microsoft.Xna.Framework.dll

using Microsoft.Xna.Framework;

namespace Microsoft.Xna.Framework.Input
{
    public static class GamePad
    {
        public static GamePadState GetState(PlayerIndex playerIndex);
        public static GamePadState GetState(PlayerIndex playerIndex, GamePadDeadZone deadZoneMode);
        public static GamePadCapabilities GetCapabilities(PlayerIndex playerIndex);
        public static bool SetVibration(PlayerIndex playerIndex, float leftMotor, float rightMotor);
    }
}
