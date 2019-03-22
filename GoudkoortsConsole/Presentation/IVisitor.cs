using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation {
    public interface IVisitor {
        void Visit(Ship ship);
        void Visit(Dock dock);
        void Visit(Field field);
        void Visit(Warehouse warehouse);
        void Visit(Rails rails);
        void Visit(Split split);
        void Visit(Merge merge);
        void Visit(Switchyard yard);
    }
}