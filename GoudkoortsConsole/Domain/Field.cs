namespace Domain {
    using Presentation;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public abstract class Field {
        // Used for displaying the Field in the View.
        public abstract void Accept(RailroadVistor vistitor);

        // Reference to the Next link.
        public Field Next { get; set; }

        public Cart Cart { get; set; }

        public abstract bool PlaceCart(Cart cart);
        public abstract bool RemoveCart();
    }
}