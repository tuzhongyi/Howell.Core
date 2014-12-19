using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Howell.Industry
{
    public class IdentityTest
    {
        public static void Test()
        {
            Identity id = new Identity(0, 1, 2, 3, IdentityClassification.IPCamera, 0, 12, NetworkType.None, ModuleType.NetworkInterface, 1, 0);
            Console.WriteLine("Id{");
            Console.WriteLine("   Nation:{0}", id.Nation);
            Console.WriteLine("   Privonce:{0}", id.Province);
            Console.WriteLine("   City:{0}", id.City);
            Console.WriteLine("   County:{0}", id.County);
            //Console.WriteLine("   Industry:{0}", id.Industry);
            Console.WriteLine("   ProjectNumber:{0}", id.ProjectNumber);
            Console.WriteLine("   Classification:{0}", id.Classification);
            Console.WriteLine("   SerialNumber:{0}", id.SerialNumber);
            Console.WriteLine("   Network:{0}", id.Network);
            Console.WriteLine("   ModuleType:{0}", id.ModuleType);
            Console.WriteLine("   ModuleNumber:{0}", id.ModuleNumber);
            Console.WriteLine("  }");
            Console.WriteLine("Identity:{0}", id.ToString());

            Identity newId = Identity.Parse(id.ToString());
            Console.WriteLine("Id{");
            Console.WriteLine("   Nation:{0}", newId.Nation);
            Console.WriteLine("   Privonce:{0}", newId.Province);
            Console.WriteLine("   City:{0}", newId.City);
            Console.WriteLine("   County:{0}", newId.County);
            Console.WriteLine("   ProjectNumber:{0}", newId.ProjectNumber);
            Console.WriteLine("   Classification:{0}", newId.Classification);
            Console.WriteLine("   SerialNumber:{0}", newId.SerialNumber);
            Console.WriteLine("   Network:{0}", newId.Network);
            Console.WriteLine("   ModuleType:{0}", newId.ModuleType);
            Console.WriteLine("   ModuleNumber:{0}", newId.ModuleNumber);
            Console.WriteLine("  }");


        }
    }
}
