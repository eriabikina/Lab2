using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackSystem {
    class ReportFacade {        
        Backlog backlog;


        public ReportFacade (Tasks task) {
            var title = new StandardTitle ();
            backlog = new Backlog (title, task);
        }

        public void CreateCompleteReport (Tasks task) {
            backlog.BacklogSummary (task);

            Reporter.CompareTask (task);
        }
    }
}

