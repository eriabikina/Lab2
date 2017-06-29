using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackSystem {
    public class Manager { 

        private Company company = Company.Instance;

        public void SubscribeForTaskEvent (Developer developer, Tester tester) {
            company.TaskFromClientArrived += (s, args) => { DistributeTasks (developer,tester); };
        }

        public void DistributeTasks (Developer developer, Tester tester) {
            Tasks task;
            Filler.Fill (out task, 20); // Randomly fill tasks 

            Reporter.CompareTask (task);

            Backlog backlog = new Backlog (task);
            Console.WriteLine ("==================");
            Console.WriteLine ("Total sprint score");
            Console.WriteLine ("==================");
            Console.WriteLine ($"{ backlog.SprintTotalPoints} pts\n");// How many points does the sprint worth based on arrived tasks 

            DoTaskHandler devTask = developer.DoTask;
            devTask (developer, task);// display development task distribution

            TestTaskHandler testTask = tester.TestTask;
            testTask (tester, task); // display test task distribution     

        }       

    }
}
