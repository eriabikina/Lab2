using Foundation.ObjectHydrator.Generators;
using Foundation.ObjectHydrator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackSystem {
    public class Tester : SystemMember, IGenerator<string> {

        public Tester (int sample) {

            RandomSystemMember (sample);
        }


        public string TestTask (Tester tester, Tasks task) {

            string workResult = "\n";
            string taskDone = "";
            int num;

            int limit;

            List<string> remove = new List<string> ();// list of tasks to be removed once developer has solved the task

            foreach (var itemTest in tester.member) {  // go through each developer and find a task 
                foreach (var inItemTest in itemTest.Value) {

                    num = 0;
                    taskDone = "";
                    remove.Clear ();

                    switch (inItemTest.Proficiency) {// Limit of number of tasks thst a developer can handle based on his/her proficiency

                        case Proficiency.Junior:
                            limit = 3;
                            break;
                        case Proficiency.Middle:
                            limit = 4;
                            break;
                        default:
                            limit = 4;
                            break;
                    }

                    foreach (var itemTask in task.sampleTask) {// go through each task to see if it can be solved by one of the developers
                        foreach (var inItemTask in itemTask.Value) {

                            if (inItemTask.TaskType == TaskType.Test)
                                switch (inItemTest.Proficiency) {

                                    case Proficiency.Junior:
                                        if (num <= limit && inItemTask.Estimate <= 2 && (inItemTask.Priority == Priority.Low || inItemTask.Priority == Priority.Medium)) {
                                            taskDone += $"{itemTask.Key} {inItemTask.TaskType} task closed! Est.:{inItemTask.Estimate}/Pr.:{inItemTask.Priority}\n";
                                            remove.Add (itemTask.Key);
                                            num++;
                                        }

                                        break;
                                    case Proficiency.Middle:
                                        if (num <= limit && inItemTask.Estimate <= 10 && (inItemTask.Priority == Priority.High || inItemTask.Priority == Priority.Medium)) {
                                            taskDone += $"{itemTask.Key} {inItemTask.TaskType} task closed! Est.:{inItemTask.Estimate}/Pr.:{inItemTask.Priority}\n";
                                            remove.Add (itemTask.Key);
                                            num++;
                                        }
                                        break;
                                    default:
                                        if (num <= limit && inItemTask.Estimate <= 40) {
                                            taskDone += $"{itemTask.Key} {inItemTask.TaskType} task closed! Est.:{inItemTask.Estimate}/Pr.:{inItemTask.Priority}\n";
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

                    workResult += $"**{inItemTest.Name} ({itemTest.Key}) solved {num} task(s)\n";
                    workResult += taskDone;
                    workResult += "------------------------------------------------\n";

                }
            }
            return workResult;
        }

    }
}


