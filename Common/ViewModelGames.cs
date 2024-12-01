using System;

namespace Common
{
    public class ViewModelGames
    {
        public Snakes SnakesPlayers = new Snakes();
        public Snakes.Point Points = new Snakes.Point();
        public Int32 Top { get; set; } = 0;
        public Int32 IdSnake {  get; set; }
    }
}
