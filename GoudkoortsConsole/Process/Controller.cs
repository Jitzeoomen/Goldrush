namespace Process {
    using Domain;
    using GoudkoortsConsole.Exceptions;
    using Presentation;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Timers;

    public class Controller {
        private Game _game;

        private Timer _timer;
        private int _numberOfRuns;
        public int Counter { get; set; }

        private MapLoader _mapLoader;

        private InputView _inputView;
        private OutputView _outputView;

        public Controller() {
            _game = new Game();

            _mapLoader = new MapLoader(_game);

            _inputView = new InputView();
            _outputView = new OutputView();
        }

        public void GoToMenu() {
            Console.Clear();
            _outputView.ShowMenu();
            if (_inputView.AskForStart()) {
                Run();
            } else {
                Environment.Exit(0);
            }
        }

        public void GoToGameOver() {
            _timer.Stop();
            _timer.Dispose();
            _game.IsOver = true;

            Console.Clear();

            _outputView.ShowGameOver();
            _inputView.AskForContinue();
            GoToMenu();
        }

        public void Run() {
            Console.Clear();
            // _outputView.showRailroad(this, _game);

            // Prepare the interval timer: each tick takes 1 second at the begin of the Game.
            _timer = new Timer(1000);
            _timer.Elapsed += DoTick;
            _timer.AutoReset = true;
            _timer.Enabled = true;

            Counter = 4;
            _numberOfRuns = 0;

            while (!_game.IsOver) {
                _outputView.showRailroad(this, _game);
                int sw = _inputView.AskForSwitch();
                try {
                    _game.Railroad.FlipSwitch(sw);
                } catch (CannotFlipSwitchException) {
                    // TODO: Maybe throw an error message for the user?
                }   
            }
        }


        private void DoTick(Object source, ElapsedEventArgs e) {
            _outputView.UpdateTimer(Counter);

            if (Counter == 0) {
                _game.SpawnCarts();
                try {
                    _game.MoveCarts();
                } catch (RailsOccupiedException) {
                    GoToGameOver();
                }
                _game.SpawnShips();

                _outputView.showRailroad(this, _game);  // Update the board.
                Counter = 4;                            // Reset the Counter to it's default.
                _numberOfRuns++;                        // 
            } else {
                Counter--;
            }

            // Each 100 Ticks, increase the Tick speed, and in the Cart spawn rate.
            if (_numberOfRuns == 100 && _timer.Interval > 400) {
                _timer.Interval -= 100;
                _game.increaseSpawnRate();
                _numberOfRuns = 0;
            }
        }
    }
}