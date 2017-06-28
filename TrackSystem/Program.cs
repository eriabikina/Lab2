using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace TrackSystem {
    class Program {
        static void Main (string[] args) {
            Company company = Company.Instance;
            Manager manager = new Manager ();
            Developer developer;
            Tester tester;

            Filler.Fill (out developer, out tester, 5); // Randomly fill dev and testers

            string scrumTeamMemebers = company.Describe (developer, tester); // Display scrum team members 
            Console.WriteLine (scrumTeamMemebers);

            company.SalaryPaid += OnSalaryPaid;   //company is subscribed to event: salary time 
            manager.SubscribeForTaskEvent (developer, tester);//manager is subscribed to event: tasks from client       

            company.TaskFromClient ();// event: tasks from client 

            company.SalaryTime (tester.employee);// event: salary time
            company.SalaryTime (developer.employee); // event: salary time

            Console.Read ();
        }

        private static void OnSalaryPaid (object sender, SalaryPaidEventArgs args) {
            Console.WriteLine ($"My name is {args.Name} and I got paid: ${args.Salary}");
        }

    }
}
