using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackSystem {
    public class EstimateCalculator {
        
        public int CalculateTaskEstimate (Func<int> estimateStrategy) {
            return estimateStrategy();
        }
    }
}
