using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackSystem {

    public class TestTask : SystemTask {
        
        public Dictionary<TaskType, List<SystemTask>> testTask = new Dictionary<TaskType, List<SystemTask>> ();

        public override void RandomSystemTask (int sample) {
            Random random = new Random (Guid.NewGuid ().GetHashCode ());

            testTask.Add (TaskType.Test, new List<SystemTask> () { new SystemTask { Estimate = Fibonacci.FibonacciNumber (random.Next (5)), Priority = RandomEnum.GenerateRandomEnum<Priority> () } });

            for (int i = 0; i < sample - 3; i++) {
                testTask[TaskType.Test].Add (new SystemTask { Estimate = Fibonacci.FibonacciNumber (random.Next (5)), Priority = RandomEnum.GenerateRandomEnum<Priority> () });
            }
        }

        public TestTask (int sample) {

            RandomSystemTask (sample);
        }                
        
    }
}

