using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace image_trial
{
    class Class1
    {
        public string success { get; set; }
        public List<AllResult> cards { get; set; }

     
       
    }
  
    public class AllResult
    {
       
        public string image { get; set; }
        public string value { get; set; }
        public string suit { get; set; }
        public string code { get; set; }
        public override string ToString()
        {
            return value;
        }

    }
}

