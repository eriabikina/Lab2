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

            string workResult = "=================\n";
            workResult += "Test distribution\n";
            workResult += "=================\n\n";
            string taskDone = "";
            int num;

            Func<int> junior = CalcForJunior; // Stategy pattern with variation
            Func<int> middle = delegate () { return 3; };
            Func<int> senior = () => 4;
            var capacity = new CapacityCalculator ();

            Func<int> estimateJunior = EstimateForJunior; // Stategy pattern with variation
            Func<int> estimateMiddle = delegate () { return 10; };
            Func<int> estimateSenior = () => 40;
            var estimate = new EstimateCalculator ();

            List<string> remove = new List<string> ();// list of tasks to be removed once tester has solved the task

            foreach (var itemTest in tester.employee) {  // go through each tester and find a task 
                foreach (var inItemTest in itemTest.Value) {

                    num = 0;
                    taskDone = "";
                    remove.Clear ();
                    
                    foreach (var itemTask in task.sampleTask) {// go through each task to see if it can be solved by one of the testers
                        foreach (var inItemTask in itemTask.Value) {

                            if (inItemTask.TaskType == TaskType.Test) //testers do only Test tasks
                                switch (itemTest.Key) {

                                    case Proficiency.Junior:
                                        if (num < capacity.CalculateEmployeeCapacity (junior) && inItemTask.Estimate <= estimate.CalculateTaskEstimate (estimateJunior) && (inItemTask.Priority == Priority.Low || inItemTask.Priority == Priority.Medium)) {
                                            taskDone += $"|| {itemTask.Key} {inItemTask.TaskType} task closed! Est.:{inItemTask.Estimate} /Pr.:{inItemTask.Priority}\n";
                                            remove.Add (itemTask.Key);
                                            num++;
                                        }

                                        break;
                                    case Proficiency.Middle:
                                        if (num < capacity.CalculateEmployeeCapacity (middle) && inItemTask.Estimate <= estimate.CalculateTaskEstimate (estimateMiddle) && (inItemTask.Priority == Priority.High || inItemTask.Priority == Priority.Medium)) {
                                            taskDone += $"|| {itemTask.Key} {inItemTask.TaskType} task closed! Est.:{inItemTask.Estimate} /Pr.:{inItemTask.Priority}\n";
                                            remove.Add (itemTask.Key);
                                            num++;
                                        }
                                        break;
                                    default:
                                        if (num < capacity.CalculateEmployeeCapacity (senior) && inItemTask.Estimate <= estimate.CalculateTaskEstimate (estimateSenior)) {
                                            taskDone += $"|| {itemTask.Key} {inItemTask.TaskType} task closed! Est.:{inItemTask.Estimate} /Pr.:{inItemTask.Priority}\n";
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

                    inItemTest.NumberOfTasks = num;
                    workResult += $"{inItemTest.Name} ({itemTest.Key}) solved {inItemTest.NumberOfTasks} task(s)\n";
                    workResult += taskDone;
                    workResult += "\n";

                }
            }
            Console.WriteLine( workResult);
        }

        internal static int CalcForJunior () {
            return 2;
        }

        internal static int EstimateForJunior () {
            return 2;
        }

    }
}


