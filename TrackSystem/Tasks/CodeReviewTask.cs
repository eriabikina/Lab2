﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackSystem {

    public class CodereviewTask : SystemTask {
        
        public Dictionary<TaskType, List<SystemTask>> corTask = new Dictionary<TaskType, List<SystemTask>> ();

        public override void RandomSystemTask (int sample) {
            Random random = new Random (Guid.NewGuid ().GetHashCode ());

            corTask.Add (TaskType.CodeReview, new List<SystemTask> () { new SystemTask { Estimate = Fibonacci.FibonacciNumber (random.Next (5)), Priority = RandomEnum.GenerateRandomEnum<Priority> () } });

            for (int i = 0; i < sample - 3; i++) {
                corTask[TaskType.CodeReview].Add (new SystemTask { Estimate = Fibonacci.FibonacciNumber (random.Next (5)), Priority = RandomEnum.GenerateRandomEnum<Priority> () });
            }
        }

        public CodereviewTask (int sample) {

            RandomSystemTask (sample);
        }                
        
    }
}

