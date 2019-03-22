namespace Presentation {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class InputView {

        public bool Asking { get; set; }

        public InputView() {
            Asking = true;
        }

        public bool AskForStart() {
            char input = '!';
            while (Asking) {
                Console.Write("Press [Spacebar] to start the game, or [q] to quit: ");
                input = Console.ReadKey().KeyChar;
                Console.WriteLine();
                
                if (input == ' ') return true;
                else if (input == 'q' || input == 'Q') return false;
            }
            return false;
        }

        public void AskForContinue() {
            Console.WriteLine("Press [Any] key to continue: ");
            Console.ReadKey();
        }

        public int AskForSwitch() {
            int input = -1;
            while(Asking && input <= 0 || input > 5){
                Console.Write("Press 1-5 to switch: ");
                char key = Console.ReadKey().KeyChar;
                Console.WriteLine();

                input = key - 48;
            }
            return input;
        }
    }
}