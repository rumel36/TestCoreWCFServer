using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCoreWCFServer
{
    public  class RandomNumberGenerator : IRandomCodeGenerator
    {
        public  string GenerateRandomCode()
        {
          
           System.Random r = new System.Random();
           return r.Next().ToString();  
        }
    }
}
