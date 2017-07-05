using Foundation.ObjectHydrator.Generators;
using Foundation.ObjectHydrator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackSystem {
    public delegate void DoTaskHandler (Developer developer, Tasks task);

    public class Developer : SystemMember, IGenerator<string> {

        public Developer () { }

        public Developer (int sample) {
            RandomSystemMember (sample);
        }                

        public void DoTask (Developer developer, Tasks task) {

            string workResult = "========================\n";
            workResult += "Development distribution\n";
            workResult += "========================\n\n";

            TaskType[] validTasks = { TaskType.Development, TaskType.CodeReview };
            workResult += AssignTasksPerEmployee (developer.employee, task, validTasks);
            Console.WriteLine (workResult);

            Reporter.GroupDevByAmountOfTasks (developer);                     
        }
               
        public override void EmployeeCapacityLimitPerProficiency (out Func<int> capacityJunior, out Func<int> capacityMiddle, out Func<int> capacitySenior, out CapacityCalculator capacity) {
            capacityJunior = CapacityForJunior;
            capacityMiddle = delegate () { return 4; };
            capacitySenior = () => 3;
            capacity = new CapacityCalculator ();
        }      

    }
}

