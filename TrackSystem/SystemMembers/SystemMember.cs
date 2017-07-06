using Foundation.ObjectHydrator.Generators;
using Foundation.ObjectHydrator.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackSystem {

    public enum Proficiency {
        Junior,
        Middle,
        Senior
    };

    public class SystemMember : IComparable<SystemMember> {
       
        public string Name { get; set; }

        public int Salary { get; set; }

        public int NumberOfTasks { get; set; }

        public Proficiency Proficiency { get; set; }

        static readonly IGenerator<string> FirstNameGen = new FirstNameGenerator ();// found ObjectHydrator on github to generate random full names using real English names and surname
        static readonly IGenerator<string> LastNameGen = new LastNameGenerator ();
        public string GenerateRandomFullName () {
            return FirstNameGen.GenerateRandomFullName () + " " + LastNameGen.GenerateRandomFullName ();
        }

        public Dictionary<Proficiency, List<SystemMember>> employee = new Dictionary<Proficiency, List<SystemMember>> ();
               
        public void RandomSystemMember (int sample) {
            Random random = new Random (Guid.NewGuid ().GetHashCode ());

            employee.Add (Proficiency.Junior, new List<SystemMember> () { new SystemMember { Name = GenerateRandomFullName (), Salary = random.Next (1000, 5000) } });
            employee.Add (Proficiency.Middle, new List<SystemMember> () { new SystemMember { Name = GenerateRandomFullName (), Salary = random.Next (1000, 5000) } });
            employee.Add (Proficiency.Senior, new List<SystemMember> () { new SystemMember { Name = GenerateRandomFullName (), Salary = random.Next (1000, 5000) } });

            for (int i = 0; i < sample - 3; i++) {
                employee[RandomEnum.GenerateRandomEnum<Proficiency> ()].Add (new SystemMember { Name = GenerateRandomFullName (), Salary = random.Next (1000, 5000) });
            }
        }

        public virtual string ChooseTaskToClose (Dictionary<Proficiency, List<SystemMember>> employee, Tasks task, TaskType[] validTasks) {

            Func<int> capacityJunior, capacityMiddle, capacitySenior; //applying strategy pattern
            CapacityCalculator capacity;
            EmployeeCapacityLimitPerProficiency (out capacityJunior, out capacityMiddle, out capacitySenior, out capacity);

            Func<int> estimateJunior, estimateMiddle, estimateSenior;
            EstimateCalculator estimate;
            TaskEstimationLimitPerProficiency (out estimateJunior, out estimateMiddle, out estimateSenior, out estimate);

            List<string> remove = new List<string> ();// list of tasks to be removed once developer has solved the task

            string workResult="";
            string taskDone;
            int num;

            foreach (var itemDev in employee) {  // go through each developer and find a task 
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
            return workResult;
        }

        public static void TaskEstimationLimitPerProficiency (out Func<int> estimateJunior, out Func<int> estimateMiddle, out Func<int> estimateSenior, out EstimateCalculator estimate) {
            estimateJunior = EstimateForJunior;
            estimateMiddle = delegate () { return 10; };
            estimateSenior = () => 40;
            estimate = new EstimateCalculator ();
        }

        public virtual void EmployeeCapacityLimitPerProficiency (out Func<int> junior, out Func<int> middle, out Func<int> senior, out CapacityCalculator capacity) {
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

        public int CompareTo (SystemMember other) {

            if (this.Proficiency == other.Proficiency) { // sort by salary if the proficiency of both employees is the same 
                return this.Salary.CompareTo (other.Salary);
            }

            return this.Proficiency.CompareTo (other.Proficiency); //sort by proficiency
        }

    }
}

