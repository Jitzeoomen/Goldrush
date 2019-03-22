namespace Domain {
    using GoudkoortsConsole.Exceptions;
    using Presentation;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Merge : Switch {
        public Field Previous { get; set; }
        public Field PreviousTop { get; set; }
        public Field PreviousBottom { get; set; }

        // Used for displaying the Field in the View.
        public override void Accept(RailroadVistor vistitor) {
            vistitor.Visit(this);
        }

        public override void SwitchRails() {
            if (Cart != null) {
                throw new CannotFlipSwitchException();
            }

            if (Previous == PreviousTop) {
                Previous = PreviousBottom;
            } else if (Previous == PreviousBottom) {
                Previous = PreviousTop;
            }
        }

        public override bool PlaceCart(Cart cart) {
            if (Cart == null && cart.Field == Previous) {
                // If this Field is empty, and points to the Field the Cart is currently on.
                return true;
            } else if (Cart != null && cart.Field == Previous) {
                // If this Field is occupied, and points to the Field the Cart is currently on.
                throw new RailsOccupiedException();
            } else {
                // If this Field points to another Field, the Cart cannot move to this Field.
                return false;
            }
        }
    }
}