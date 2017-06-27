using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackSystem {
  class RandomEnum {

        public static T GenerateRandomEnum<T> () where T : struct, IConvertible {

            if (!typeof (T).IsEnum) {
                throw new ArgumentException ("T must be an enumerated type");
            }

            Array values = Enum.GetValues (typeof (T));
            Random random = new Random (Guid.NewGuid ().GetHashCode ());

            T randomValue = (T)values.GetValue (random.Next (values.Length));
            return randomValue;
        }
    }
}
