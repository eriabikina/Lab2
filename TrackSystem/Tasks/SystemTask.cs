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

        public Priority Priority { get; set; }

        public Dictionary<TaskType, List<SystemTask>> sampleTask = new Dictionary<TaskType, List<SystemTask>> ();
        public virtual  void RandomSystemTask (int sample) {
            Random random = new Random (Guid.NewGuid ().GetHashCode ());

            sampleTask.Add (TaskType.Development, new List<SystemTask> () { new SystemTask { Estimate = Fibonacci.FibonacciNumber (random.Next (5)), Priority = RandomEnum.GenerateRandomEnum<Priority> () } });
            sampleTask.Add (TaskType.CodeReview, new List<SystemTask> () { new SystemTask { Estimate = Fibonacci.FibonacciNumber (random.Next (5)), Priority = RandomEnum.GenerateRandomEnum<Priority> () } });
            sampleTask.Add (TaskType.Test, new List<SystemTask> () { new SystemTask { Estimate = Fibonacci.FibonacciNumber (random.Next (5)), Priority = RandomEnum.GenerateRandomEnum<Priority> () } });

            for (int i = 0; i < sample - 3; i++) {
                sampleTask[RandomEnum.GenerateRandomEnum<TaskType> ()].Add (new SystemTask { Estimate = Fibonacci.FibonacciNumber (random.Next (5)), Priority = RandomEnum.GenerateRandomEnum<Priority> () });
            }
        }
    }
}
