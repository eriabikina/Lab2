using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackSystem {
    class ReportFacade {

        StandardTitle title;       
        Backlog backlog;

        public ReportFacade (Tasks task) {
            title = new StandardTitle ();
            backlog = new Backlog (title, task);
        }

        public void CreateSmallTaskReport (Tasks task) {
            backlog.BacklogSummary (task);
            Reporter.CompareTask (task);
        }
    }
}

