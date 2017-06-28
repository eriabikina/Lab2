using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackSystem {
    public class Repository {


        public string BuildPath (string folderName, string fileName) {
            StringBuilder sb = new StringBuilder ();

            string path = Environment.CurrentDirectory + "/" + folderName;
            if (!Directory.Exists (path)) {
                Directory.CreateDirectory (path);
            }
            sb.Append (path).Append ($"/{ fileName}.txt");

            return sb.ToString ();
        }


    }

}
