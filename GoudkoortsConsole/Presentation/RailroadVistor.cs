using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Presentation {
    public class RailroadVistor : IVisitor {
        public void Visit(Domain.Rails rails) {
            if (rails.Cart != null) {
                VisitorContainsCart(rails);
                return;
            }
            Console.Write("=");
        }

        public void Visit(Domain.Ship ship) {
            if (ship.IsFull) Console.Write("F");
            else if (ship.IsHalfFull) Console.Write("$");
            else Console.Write("S");
        }

        public void Visit(Domain.Dock dock) {
            if (dock.Cart != null) {
                VisitorContainsCart(dock);
                return;
            }
            Console.Write("-");
        }

        public void Visit(Domain.Warehouse warehouse) {
            Console.Write("W");
        }

        public void Visit(Domain.Switchyard yard) {
            if (yard.Cart != null) {
                VisitorContainsCart(yard);
                return;
            }
            Console.Write("_");
        }

        public void Visit(Domain.Split split) {
            if (split.Cart != null) {
                VisitorContainsCart(split);
                return;
            }
            if (split.Next == split.NextTop) {
                Console.Write("┴");
            } else if (split.Next == split.NextBottom) {
                Console.Write("┬");
            }
        }

        public void Visit(Merge merge) {
            if (merge.Cart != null) {
                VisitorContainsCart(merge);
                return;
            }

            if (merge.Previous == merge.PreviousTop) {
                Console.Write("┴");
            } else if (merge.Previous == merge.PreviousBottom) {
                Console.Write("┬");
            }
        }

        public void Visit(Field field) {
            // This should never happen...
            Console.Write("?");
        }

        private void VisitorContainsCart(Field field) {
            if (field.Cart.Cargo == null) {
                Console.Write("O");
            } else {
                Console.Write("0");
            }
        }
    }
}