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

        public Developer (int sample) {

            RandomSystemMember (sample);
        }
          
       public void DoTask (Developer developer, Tasks task) {

            string workResult = "==============================\n";
            workResult += "Development tasks distribution\n";
            workResult += "==============================\n\n";
            string taskDone = "";
            int num;

            int capacity;
                        
            List<string> remove = new List<string> ();// list of tasks to be removed once developer has solved the task

            foreach (var itemDev in developer.employee) {  // go through each developer and find a task 
                foreach (var inItemDev in itemDev.Value) {

                    num = 0;
                    taskDone = "";
                    remove.Clear ();

                    switch (itemDev.Key) {// Limit of number of tasks that a developer can handle based on his/her proficiency

                        case Proficiency.Junior:
                            capacity = 2;
                            break;
                        case Proficiency.Middle:
                            capacity = 4;
                            break;
                        default:
                            capacity = 3;
                            break;
                    }

                    foreach (var itemTask in task.sampleTask) {// go through each task to see if it can be solved by one of the developers
                        foreach (var inItemTask in itemTask.Value) {

                            if (inItemTask.TaskType != TaskType.Test)// developers do only Dev and CoR tasks
                                switch (itemDev.Key) {

                                    case Proficiency.Junior:
                                        if (num < capacity && inItemTask.Estimate <= 2 && (inItemTask.Priority == Priority.Low || inItemTask.Priority == Priority.Medium)) {
                                            taskDone += $"|| {itemTask.Key} {inItemTask.TaskType} task closed! Est.:{inItemTask.Estimate}/Pr.:{inItemTask.Priority}\n";
                                            remove.Add (itemTask.Key);
                                            num++;
                                        }

                                        break;
                                    case Proficiency.Middle:
                                        if (num < capacity && inItemTask.Estimate <= 10 && (inItemTask.Priority == Priority.High || inItemTask.Priority == Priority.Medium)) {
                                            taskDone += $"|| {itemTask.Key} {inItemTask.TaskType} task closed! Est.:{inItemTask.Estimate}/Pr.:{inItemTask.Priority}\n";
                                            remove.Add (itemTask.Key);
                                            num++;
                                        }
                                        break;
                                    default:
                                        if (num < capacity && inItemTask.Estimate <= 40) {
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

                    workResult += $"{inItemDev.Name} ({itemDev.Key}) solved {num} task(s)\n";
                    workResult += taskDone;
                    workResult += "\n";

                }
            }
            Console.WriteLine( workResult);
        }
    }
}

