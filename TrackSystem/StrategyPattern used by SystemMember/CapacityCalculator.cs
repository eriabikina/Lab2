using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackSystem {
    public class CapacityCalculator {
        
        public int CalculateEmployeeCapacity (Func<int> capacityStrategy) {
            return capacityStrategy();
        }
    }
}
