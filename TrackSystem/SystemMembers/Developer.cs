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
            string taskDone = "";
            int num;

            Func<int> capacityJunior, capacityMiddle, capacitySenior;
            CapacityCalculator capacity;
            DevCapacity (out capacityJunior, out capacityMiddle, out capacitySenior, out capacity);

            Func<int> estimateJunior, estimateMiddle, estimateSenior;
            EstimateCalculator estimate;
            DevEstimate (out estimateJunior, out estimateMiddle, out estimateSenior, out estimate);

            List<string> remove = new List<string> ();// list of tasks to be removed once developer has solved the task

            TaskType[] validTasks = { TaskType.Test };

            foreach (var itemDev in developer.employee) {  // go through each developer and find a task 
                foreach (var inItemDev in itemDev.Value) {

                    num = 0;
                    taskDone = "";
                    remove.Clear ();

                    foreach (var itemTask in task.sampleTask) {// go through each task to see if it can be solved by one of the developers
                        foreach (var inItemTask in itemTask.Value) {

                            if (validTasks.Contains (inItemTask.TaskType))// developers do only Dev and CoR tasks
                                switch (itemDev.Key) {

                                    case Proficiency.Junior:

                                        if (num < capacity.CalculateEmployeeCapacity (capacityJunior) && inItemTask.Estimate <= estimate.CalculateTaskEstimate (estimateJunior) && (inItemTask.Priority == Priority.Low || inItemTask.Priority == Priority.Medium)) {
                                            taskDone += $"|| {itemTask.Key} {inItemTask.TaskType} task closed! Est.:{inItemTask.Estimate}/Pr.:{inItemTask.Priority}\n";
                                            remove.Add (itemTask.Key);
                                            num++;
                                        }

                                        break;
                                    case Proficiency.Middle:
                                        if (num < capacity.CalculateEmployeeCapacity (capacityMiddle) && inItemTask.Estimate <= estimate.CalculateTaskEstimate (estimateMiddle) && (inItemTask.Priority == Priority.High || inItemTask.Priority == Priority.Medium)) {
                                            taskDone += $"|| {itemTask.Key} {inItemTask.TaskType} task closed! Est.:{inItemTask.Estimate}/Pr.:{inItemTask.Priority}\n";
                                            remove.Add (itemTask.Key);
                                            num++;
                                        }
                                        break;
                                    default:
                                        if (num < capacity.CalculateEmployeeCapacity (capacitySenior) && inItemTask.Estimate <= estimate.CalculateTaskEstimate (estimateSenior)) {
                                            taskDone += $"|| {itemTask.Key} {inItemTask.TaskType} task closed! Est.:{inItemTask.Estimate}/Pr.:{inItemTask.Priority}\n";
                                            remove.Add (itemTask.Key);
                                            num++;
                                        }
                                        break;
                                }
                        }
                    }

                    foreach (string key in remove) { // remove tasks that were solved by developers
                        task.sampleTask.Remove (key);
                    }

                    inItemDev.NumberOfTasks = num;
                    workResult += $"{inItemDev.Name} ({itemDev.Key}) solved {inItemDev.NumberOfTasks} task(s)\n";
                    workResult += taskDone;
                    workResult += "\n";

                }
            }
            Reporter.GroupDevByAmountOfTasks (developer);

            Console.WriteLine (workResult);

        }

        private static void DevEstimate (out Func<int> estimateJunior, out Func<int> estimateMiddle, out Func<int> estimateSenior, out EstimateCalculator estimate) {
            estimateJunior = EstimateForJunior;
            estimateMiddle = delegate () { return 10; };
            estimateSenior = () => 40;
            estimate = new EstimateCalculator ();
        }

        private static void DevCapacity (out Func<int> capacityJunior, out Func<int> capacityMiddle, out Func<int> capacitySenior, out CapacityCalculator capacity) {
            capacityJunior = CapacityForJunior;
            capacityMiddle = delegate () { return 4; };
            capacitySenior = () => 3;
            capacity = new CapacityCalculator ();
        }

        internal static int CapacityForJunior () {
            return 2;            
        }

        internal static int EstimateForJunior () {
            return 2;
        }

    }
}

