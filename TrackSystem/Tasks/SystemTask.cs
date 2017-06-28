using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackSystem {
    public enum Priority {
        Critical,
        High,
        Medium,
        Low
    };

    public enum TaskType {
        Development,
        CodeReview,
        Test
    };

    public class SystemTask {

        public int Estimate { get; set; }

        public TaskType TaskType { get; set; }

        public Priority Priority { get; set; }      

        public Dictionary<string, List<SystemTask>> sampleTask = new Dictionary<string, List<SystemTask>> ();
        public virtual  void RandomSystemTask (int sample) {
            Random random = new Random (Guid.NewGuid ().GetHashCode ());
            for (int i = 0; i < sample; i++) {
                sampleTask.Add ("CR" + (1000 + i).ToString (), new List<SystemTask> () { new SystemTask {TaskType=RandomEnum.GenerateRandomEnum<TaskType>(),  Estimate = Fibonacci.FibonacciNumber (random.Next (1,6)), Priority = RandomEnum.GenerateRandomEnum<Priority> () } });
            }
        }
    }
}
