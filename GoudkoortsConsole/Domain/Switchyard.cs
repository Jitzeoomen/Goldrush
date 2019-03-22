namespace Domain {
    using Presentation;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Switchyard : Rails {
        // Used for displaying the Field in the View.
        public override void Accept(RailroadVistor vistitor) {
            vistitor.Visit(this);
        }

        public override bool PlaceCart(Cart cart) {
            if (Cart != null) {
                return false;
            }
            return true;
        }

        public override bool RemoveCart() {
            return false;
        }
	}
}