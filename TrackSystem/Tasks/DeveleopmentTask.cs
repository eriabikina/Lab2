using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackSystem {

    public class DeveleopmentTask : SystemTask {
        
        public Dictionary<TaskType, List<SystemTask>> devTask = new Dictionary<TaskType, List<SystemTask>> ();

        public override void RandomSystemTask (int sample) {
            Random random = new Random (Guid.NewGuid ().GetHashCode ());

            devTask.Add (TaskType.Development, new List<SystemTask> () { new SystemTask { Estimate = Fibonacci.FibonacciNumber (random.Next (5)), Priority = RandomEnum.GenerateRandomEnum<Priority> () } });

            for (int i = 0; i < sample - 3; i++) {
                devTask[TaskType.Development].Add (new SystemTask { Estimate = Fibonacci.FibonacciNumber (random.Next (5)), Priority = RandomEnum.GenerateRandomEnum<Priority> () });
            }
        }

        public DeveleopmentTask (int sample) {

            RandomSystemTask (sample);
        }                
        
    }
}

