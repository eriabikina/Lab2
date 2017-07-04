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

            Func<int> junior, middle, senior;
            CapacityCalculator capacity;
            TestCapacity (out junior, out middle, out senior, out capacity);

            Func<int> estimateJunior, estimateMiddle, estimateSenior;
            EstimateCalculator estimate;
            TestEstimate (out estimateJunior, out estimateMiddle, out estimateSenior, out estimate);

            List<string> remove = new List<string> ();// list of tasks to be removed once tester has solved the task
            TaskType[] validTasks = { TaskType.Development, TaskType.CodeReview };

            foreach (var itemTest in tester.employee) {  // go through each tester and find a task 
                foreach (var inItemTest in itemTest.Value) {

                    num = 0;
                    taskDone = "";
                    remove.Clear ();

                    foreach (var itemTask in task.sampleTask) {// go through each task to see if it can be solved by one of the testers
                        foreach (var inItemTask in itemTask.Value) {

                            if (validTasks.Contains (inItemTask.TaskType)) //testers do only Test tasks
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
            Console.WriteLine (workResult);
        }

        private static void TestEstimate (out Func<int> estimateJunior, out Func<int> estimateMiddle, out Func<int> estimateSenior, out EstimateCalculator estimate) {
            estimateJunior = EstimateForJunior;
            estimateMiddle = delegate () { return 10; };
            estimateSenior = () => 40;
            estimate = new EstimateCalculator ();
        }

        private static void TestCapacity (out Func<int> junior, out Func<int> middle, out Func<int> senior, out CapacityCalculator capacity) {
            junior = CapacityForJunior;
            middle = delegate () { return 3; };
            senior = () => 4;
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


