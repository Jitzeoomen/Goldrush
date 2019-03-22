using GoudkoortsConsole.Exceptions;

namespace Domain {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Game {
        private Canal _canal;
        public Canal Canal { get { return _canal; } }

        private List<Cart> _carts;
        public List<Cart> Cart { get { return _carts; } }

        private Railroad _railroad;
        public Railroad Railroad { get { return _railroad; } }

        public Game() {
            _canal = new Canal();
            _canal.SpawnShip();
            _carts = new List<Cart>();
            _railroad = new Railroad();

            IsOver = false;
        }

        public void SpawnCarts() {
            if (Railroad.Warehouses.Count == 0) return;
            foreach (Warehouse w in Railroad.Warehouses) {
                Cart c = w.SpawnCart();
                if (c != null) {
                    Cart.Add(c);
                }
            }
        }

        public void increaseSpawnRate() {
            if (Railroad.Warehouses.Count == 0) return;
            foreach (Warehouse w in Railroad.Warehouses) {
                w.Chance += 5;
            }
        }

        public void MoveCarts() {
            if (Cart.Count == 0) return;
            foreach (Cart c in Cart) {
                try {
                    c.Move();
                } catch (RailsDoesNotExistError) {
                    _carts.Remove(c);
                }
            }
        }

        public void SpawnShips() {
            _canal.SpawnShip();
        }

        public int GetScore() {
            int score = Canal.ShipsHandeled * 20;
            return score + Canal.Peek().Current;;
        }

        public bool IsOver { get; set; }
    }
}