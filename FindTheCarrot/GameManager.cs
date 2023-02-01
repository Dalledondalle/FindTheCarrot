using System;
using System.Collections.Generic;
using System.Text;

namespace FindTheCarrot
{
    public class GameManager
    {
        public enum GameState { Choosing, Looking, Deciding, Finished };
        public GameState State;
    }
}
