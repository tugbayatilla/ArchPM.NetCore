using System;
using System.Collections.Generic;
using System.Text;

namespace ArchPM.NetCore.Tests.TestModels
{
    internal class SampleRecursiveListClass
    {
        internal class A
        {
            public string Name { get; set; }

            public B Ref_B {get;set;}
        }
        internal class B
        {
            public string Name { get; set; }
            public C Ref_C { get; set; }

        }
        internal class C
        {
            public string Name { get; set; }
            public A Ref_A { get; set; }
            public List<A> Ref_A_list { get; set; }
        }
    }
    
}
