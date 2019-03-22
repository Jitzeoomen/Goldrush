
namespace Domain {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Canal {
        private static readonly Random getrandom = new Random();
        private static readonly object syncLock = new object();

        private Queue<Ship> _ships;

        public int ShipsHandeled { get; set; }

        public int Chance { get; set; }

        public int Count { get { return _ships.Count; } }

        public Canal() {
            _ships = new Queue<Ship>();
            Chance = 10;
        }

        public Ship Peek() {
            return _ships.First();
        }

        public void Enqueue(Ship ship) {
            _ships.Enqueue(ship);
        }

        public static int GetRandomNumber(int min, int max) {
            lock (syncLock) {
                return getrandom.Next(min, max);
            }
        }

        public void SpawnShip() {
            if (_ships.Count == 0) {
                Enqueue(new Ship());
            } else {
                int rand = GetRandomNumber(0, 101);

                if (rand <= Chance) {
                    Enqueue(new Ship());
                }
            }
        }

        public bool PlaceCargo(Cargo cargo) {
            Ship ship = _ships.Peek();

            if (ship != null) {
                if (ship.AddCargo(cargo)) {
                    if (ship.IsFull) {
                        ShipsHandeled++;
                        _ships.Dequeue();
                        if (_ships.Count == 0) {
                            SpawnShip();
                        }
                    }
                    return true;
                } else {
                    return false;
                }
            }
            return false;
        }
    }
}