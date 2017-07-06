using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackSystem {
    class StandardTitle: ITitle {
        public string Title (string key, string value) {
            return string.Format("   -{0}: {1}", key, value);
        }
    }
}
