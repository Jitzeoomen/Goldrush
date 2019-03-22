namespace Domain {
    using GoudkoortsConsole.Exceptions;
    using Presentation;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Dock : Rails {
        public Canal Canal { get; set; }

        // Used for displaying the Field in the View.
        public override void Accept(RailroadVistor vistitor) {
            vistitor.Visit(this);
        }

        public override bool PlaceCart(Cart cart) {
            if (Cart != null) {
                throw new RailsOccupiedException();
            }
            if (Canal != null) {
                Cargo cargo = cart.Cargo;
                // Try to place the Cargo inside the Ship and remove Cargo from Cart if succesful.
                if (Canal.PlaceCargo(cargo)) {
                    cart.Cargo = null;
                }
            }
            return true;
        }
    }
}