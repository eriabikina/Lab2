using Foundation.ObjectHydrator.Generators;
using Foundation.ObjectHydrator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackSystem {
    public delegate void TestTaskHandler (Tester test, Tasks task);
    public class Tester : SystemMember, IGenerator<string> {

        public Tester () { }

        public Tester (int sample) {
            RandomSystemMember (sample);
        }

        public void TestTask (Tester tester, Tasks task) {

            string workResult = "========================\n";
            workResult += "Test distribution\n";
            workResult += "========================\n\n";

            TaskType[] validTasks = { TaskType.Test };
            workResult += AssignTasksPerEmployee (tester.employee, task, validTasks);
            Console.WriteLine (workResult);
        }
    }
}


