namespace Domain {
    using Presentation;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using GoudkoortsConsole.Exceptions;

    public class Rails : Field {
        // Used for displaying the Field in the View.
        public override void Accept(RailroadVistor vistitor) {
            vistitor.Visit(this);
        }

        public override bool PlaceCart(Cart cart) {
            if (Cart != null) {
                // If there is already a Cart on this Field.
                throw new RailsOccupiedException();
            }
            // If there is no Cart on this Field, let the Cart move on.
            return true;
        }

        public override bool RemoveCart() {
            if (Cart == null) {
                return false;
            } else {
                Cart = null;
                if (Next == null) {
                    throw new RailsDoesNotExistError();
                }
                return true;
            }
        }
    }
}