using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackSystem {
    class Backlog : Title {

        public readonly int SprintTotalPoints;

        public Backlog (ITitle title) : base (title) {
            this.SprintTotalPoints = 0;
        }

        public Backlog (ITitle title, Tasks task) : base (title) {
            foreach (var item in task.sampleTask) {
                foreach (var inItem in item.Value) {
                    this.SprintTotalPoints += inItem.Estimate;
                }
            }
        }

        override public void BacklogSummary (Tasks task) {
            Backlog backlog = new Backlog (title, task);
            Console.WriteLine ("==========================");
            Console.WriteLine (title.Title ("Number of tasks", task.sampleTask.Count.ToString ()));// How many tasks arrived 
            Console.WriteLine (title.Title ("Total sprint score", backlog.SprintTotalPoints.ToString ()));// How many points does the sprint worth based on arrived tasks 
            Console.WriteLine ("==========================");
        }
    }
}
