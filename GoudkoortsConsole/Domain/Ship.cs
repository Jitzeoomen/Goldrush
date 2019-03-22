namespace Domain {
    using Presentation;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Ship {
        public static int SIZE = 8;
        private Cargo[] _cargo;
        public int Current { get; set; }

        public bool IsFull { get { return Current == 8; } }
        public bool IsHalfFull { get { return Current >= 4;  } }

        public Ship() {
            _cargo = new Cargo[SIZE];
        }

        public void Accept(IVisitor visitor) {
            visitor.Visit(this);
        }

        public bool AddCargo(Cargo cargo) {
            for (int i = 0; i < SIZE; i++) {
                if (_cargo[i] == null) {
                    _cargo[i] = cargo;
                    Current++;
                    return true;
                }
            }
            return false;
        }
    }
}