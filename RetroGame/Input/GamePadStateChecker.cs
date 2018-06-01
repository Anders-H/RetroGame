using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RetroGameClasses.Scene;

namespace RetroGameClasses.Input
{
	public class GamePadStateChecker : IRetroActor
	{
		private GamePadState GamePadState { get; set; }
		private GamePadState OldGamePadState { get; set; }
		public PlayerIndex PlayerIndex { get; }
		public GamePadStateChecker(PlayerIndex playerIndex)
		{
			PlayerIndex = playerIndex;
			Act(0);
		}
		public void Act(ulong ticks)
		{
			OldGamePadState = GamePadState;
			GamePadState = GamePad.GetState(PlayerIndex);
		}
		public bool IsButtonUp(Buttons button) => GamePadState.IsButtonUp(button);
		public bool IsButtonDown(Buttons button) => GamePadState.IsButtonDown(button);
		public bool IsButtonPressed(Buttons button) => GamePadState.IsButtonDown(button) && OldGamePadState.IsButtonUp(button);
	}
}
