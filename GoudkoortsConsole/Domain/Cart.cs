using System.CodeDom;
using GoudkoortsConsole.Exceptions;

namespace Domain {
    using Presentation;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Cart {
        // The Field I am currently on.
        public Field Field { get; set; }
        public Cargo Cargo { get; set; }

        public virtual void Move() {
            if (Field.Next == null) {
                // this.Field = null;
                Field.RemoveCart();
                return;
            } 
            if (Field.Next.PlaceCart(this)) {
                // If the Next Field can Place this Cart: Move this Cart.
                // The Next Field might throw an RailsOccupiedException:
                // This will be caught by the Controller.
                Field.Cart = null;
                Field = Field.Next;
                Field.Cart = this;
            }
            // Else I'll just stay put on the current Field.
        }
    }
}