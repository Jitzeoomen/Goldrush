namespace Domain {
    using Presentation;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Railroad {
        public Field[,] Field2D {
            get;
            set;
        }

        private Warehouse[] _warehouses;
        public List<Warehouse> Warehouses { get { return _warehouses.ToList<Warehouse>(); } }

        private Switch[] _switches;

        public Railroad() {
            _warehouses = new Warehouse[3];
            _switches = new Switch[5];
        }

        public bool AddWarehouse(Warehouse warehouse) {
            for (int i = 0; i < _warehouses.Length; i++) {
                if (_warehouses[i] == null) {
                    _warehouses[i] = warehouse;
                    return true;
                }
            }
            return false;
        }

        public bool AddSwitch(Switch mySwitch) {
            for (int i = 0; i < _switches.Length; i++) {
                if (_switches[i] == null) {
                    _switches[i] = mySwitch;
                    return true;
                }
            }
            return false;
        }

        public void FlipSwitch(int id) {
            if (id - 1 < _switches.Length && _switches[id - 1] != null) {
                _switches[id - 1].SwitchRails();
            }
        }
    }
}