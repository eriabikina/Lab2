using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackSystem {
    class Backlog {

        public readonly int SprintTotalPoints;
        
        public Backlog() {
            this.SprintTotalPoints = 0;
        }

        public Backlog (Tasks task) {
            if (task != null) {
                foreach (var item in task.sampleTask) {
                    foreach (var inItem in item.Value) {
                        this.SprintTotalPoints += inItem.Estimate;
                    }
                }
            }          
        }
        
    }
}
