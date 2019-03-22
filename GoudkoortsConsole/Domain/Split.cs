namespace Domain {
    using GoudkoortsConsole.Exceptions;
    using Presentation;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Split : Switch {
        public Field NextTop { get; set; }
        public Field NextBottom { get; set; }

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

	    public override void SwitchRails() {
            if (Cart != null) {
                // Cannot flip when this Field contains a Cart.
                throw new CannotFlipSwitchException();
            }
            if (Next == NextTop) {
                Next = NextBottom;
            } else if (Next == NextBottom) {
                Next = NextTop;
            }
	    }
    }
}