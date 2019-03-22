namespace Presentation {
    using Domain;
    using Process;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class OutputView {
        private RailroadVistor _visitor;

        public OutputView() {
            _visitor = new RailroadVistor();
        }

        public void ShowMenu() {
            Console.WriteLine("*************************************************");
            Console.WriteLine("*                    Goldrush!                  *");
            Console.WriteLine("*               |--------------------|          *");
            Console.WriteLine("*               |   ~   | Water      |          *");
            Console.WriteLine("*               |   -   | Dock       |          *");
            Console.WriteLine("*               |   =   | Railroad   |          *");
            Console.WriteLine("*               | ┴ / ┬ | Split      |          *");
            Console.WriteLine("*               | ┴ / ┬ | Merge      |          *");
            Console.WriteLine("*               |   W   | Warehouse  |          *");
            Console.WriteLine("*               |   _   | Switchyard |          *");
            Console.WriteLine("*               |   0   | Full cart  |          *");
            Console.WriteLine("*               |   O   | Empty cart |          *");
            Console.WriteLine("*               |   S   | Empty boat |          *");
            Console.WriteLine("*               |   $   | Half boat  |          *");
            Console.WriteLine("*               |   F   | Full boat  |          *");
            Console.WriteLine("*               |--------------------|          *");
            Console.WriteLine("*************************************************");
        }

        public void showRailroad(Controller controller, Game game) {
            // Console.Clear();
            Console.CursorTop = 0;
            Console.CursorLeft = 0;
            Console.WriteLine("*************************************************");
            Console.WriteLine("*  Countdown: " + controller.Counter + "                     Score: " + game.GetScore() + "    *");
            Console.WriteLine("*************************************************");
            Console.WriteLine("*                                               *");

            // Draw the Canal including it's Ships
            Console.Write("*     ");
            for (int i = 0; i < 9; i++) {
                if (10 - game.Canal.Count <= i) {
                    Console.Write("S");
                } else {
                    Console.Write("~");
                }
                Console.Write(" ");
            }
            _visitor.Visit(game.Canal.Peek());
            Console.WriteLine(" ~ ~              *");

            int rowLength = game.Railroad.Field2D.GetLength(0);
            int colLength = game.Railroad.Field2D.GetLength(1);

            // Draw the Railroad system.
            for (int i = 0; i < rowLength; i++) {
                Console.Write("*     ");
                for (int j = 0; j < colLength; j++) {
                    if (game.Railroad.Field2D[i, j] != null) {
                        game.Railroad.Field2D[i, j].Accept(_visitor);
                        Console.Write(" ");
                    } else
                        Console.Write("  ");
                }
                Console.WriteLine("                  *");
            }
            Console.WriteLine("*                                               *");
            Console.WriteLine("*************************************************");
        }

        public void ShowGameOver() {
            Console.WriteLine("*************************************************");
            Console.WriteLine("*       Oh, I'm sorry! You have lost...         *");
            Console.WriteLine("*************************************************");
        }

        public void UpdateTimer(int counter) {
            int x = Console.CursorLeft;
            int y = Console.CursorTop;
            Console.SetCursorPosition(14, 1);
            Console.Write(counter);
            Console.SetCursorPosition(x, y);
        }
    }
}