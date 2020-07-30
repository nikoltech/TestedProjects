using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FAngu.Services
{
    public class TestDIOne
    {
        //private readonly TestDITwo two;
        public string State { get; set; }
        public TestDIOne() 
        {
            //this.two = two;
            State = "1";
        }
    }
}
