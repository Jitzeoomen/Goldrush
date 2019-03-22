namespace Domain {
    using Presentation;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public abstract class Switch : Field {
		public abstract void SwitchRails();

        public override bool RemoveCart() {
            if (Cart == null) {
                return false;
            } else {
                Cart = null;
                return true;
            }
        }
	}
}