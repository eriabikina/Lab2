using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackSystem.Tasks {
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

    class SystemTask   {

        private int estimate;

        public int Estimate {
            get { return estimate;            }
            set {  estimate = value;            }
        }

        private Priority priority;

        public Priority Priority {
            get { return priority; }
            set { priority = value; }
        }

        public static Priority GenerateRandomPriority (){ 

            Array priorityValues = Enum.GetValues (typeof (Priority));
            Random random = new Random ();

            Priority priorityRandom = (Priority)priorityValues.GetValue (random.Next (priorityValues.Length));
            return priorityRandom;
        }

        public static TaskType GenerateRandomTaskType () {

            Array taskValues = Enum.GetValues (typeof (TaskType));
            Random random = new Random ();

            TaskType taskRandom = (TaskType)taskValues.GetValue (random.Next (taskValues.Length));
            return taskRandom;
        }

        public Dictionary<TaskType, List<SystemTask>> sampleTask = new Dictionary<TaskType, List<SystemTask>> ();
        public void RandomSystemMember (int sample) {
            Random random = new Random ();

            sampleTask.Add (TaskType.Development, new List<SystemTask> () { new SystemTask { Estimate = Fibonacci.FibonacciNumber(random.Next(5)), Priority = GenerateRandomPriority() } });
            sampleTask.Add (TaskType.CodeReview, new List<SystemTask> () { new SystemTask { Estimate = Fibonacci.FibonacciNumber (random.Next (5)), Priority = GenerateRandomPriority () } });
            sampleTask.Add (TaskType.Test, new List<SystemTask> () { new SystemTask { Estimate = Fibonacci.FibonacciNumber (random.Next (5)), Priority = GenerateRandomPriority () } });

            for (int i = 0; i < sample - 3; i++) {
                sampleTask[GenerateRandomTaskType ()].Add (new SystemTask { Estimate = Fibonacci.FibonacciNumber (random.Next (5)), Priority = GenerateRandomPriority () });
            }
        }


    }
}
