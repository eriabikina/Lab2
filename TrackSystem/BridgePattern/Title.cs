using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackSystem {
   public abstract class Title {

        protected readonly ITitle title;

        public Title (ITitle title) {
            this.title = title;
        }

        abstract public void BacklogSummary (Tasks task);
        
    }
}
