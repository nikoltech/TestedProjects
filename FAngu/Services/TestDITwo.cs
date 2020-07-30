using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FAngu.Services
{
    public class TestDITwo
    {
        private readonly TestDIOne one;
        public string State { get; set; }
        public TestDITwo(TestDIOne one)
        {
            this.one = one;
            this.State = "2";
            one.State = "modifiedBy2";
        }
    }
}
