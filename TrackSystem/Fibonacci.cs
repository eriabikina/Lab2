using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackSystem {
    static class Fibonacci {// Fibonacci sequence code found on stackoverflow

        public static int FibonacciNumber (int n) {
            if (n == 0) {
                return 0; //To return the first Fibonacci number  
            }
            if (n == 1) {
                return 1; //To return the second Fibonacci number   
            } 
            return FibonacciNumber (n - 1) + FibonacciNumber (n - 2);
        }
    }
}
