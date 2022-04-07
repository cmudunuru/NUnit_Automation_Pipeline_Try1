using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnit_Automation_Pipeline_Try1.util.print;

public class PrintUtil
{
    public string greetName(string name)
    {
        return $"Hi! {name}";
    }

    public string sayHello(string name)
    {
        return $"Hello! {name}";
    }

    public int getStringLength(string name)
    {
        return name.Length;  
    }
}
