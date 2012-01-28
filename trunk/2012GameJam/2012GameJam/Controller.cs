using System;
using Microsoft.Xna.Framework.Input;

/// <summary>
/// Summary description for Controller class.
/// </summary>
public class Controller
{
    GamePadState playerOne = GamePad.GetState(PlayerIndex.One);
    GamePadState playerTwo = GamePad.GetState(PlayerIndex.Two);
    GamePadState playerThree = GamePad.GetState(PlayerIndex.Three);
    GamePadState playerFour = GamePad.GetState(PlayerIndex.Four);


    /// <summary>
    /// Constructor for controller class. 
    /// Each controller takes in a Player
    /// </summary>
	public Controller(GamePadState gamePadState, Button button)
	{
        this.gamePadState = gamePadState;
        this.button = button;
	}
}
