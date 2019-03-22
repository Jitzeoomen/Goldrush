namespace Domain {
    using Presentation;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Warehouse : Field {
        private static readonly Random getrandom = new Random();

        private static readonly object syncLock = new object();

        public int Chance { get; set; }

        public Warehouse() {
            Chance = 10;
        }

        // Used for displaying the Field in the View.
        public override void Accept(RailroadVistor vistitor) {
            vistitor.Visit(this);
        }

        public static int GetRandomNumber(int min, int max) {
            lock (syncLock) {
                return getrandom.Next(min, max);
            }
        }

        public override bool PlaceCart(Cart cart) {
            // Carts can not move onto a Warehouse.
            return false;
        }

        public override bool RemoveCart() {
            return false;
        }

        public Cart SpawnCart() {
            int rand = GetRandomNumber(0, 101);

            if (rand <= Chance) {
                Cart cart = new Cart();
                cart.Cargo = new Cargo();
                this.Cart = cart;
				cart.Field = this;
                return cart;
            }
            return null;
        }
    }
}