using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit_Automation_Pipeline_Try1.util.math;

namespace NUnit_Automation_Pipeline_Try1.unitTests;

public class MathUtilTest

{
    MathUtil? mathUtil1;

    [SetUp]
    public void init()
    {
        mathUtil1 = new MathUtil();
    }
    [Test]
    public void addNumberTest()
    {
        int expectedValue = 4;
        int total = mathUtil1.addNumbers(2, 2); 
        Assert.AreEqual(expectedValue, total,"total are not equal");
    }

    [Test]
    public void substractNumberTest()
    {
        int expectedValue = 0;
        int total = mathUtil1.substractNumbers(2, 2);
        //string applicationxpath = "//a[@href='{0}']";
        //Console.WriteLine(String.Format(applicationxpath, "/add_remove_elements/"));
        Assert.AreEqual(expectedValue, total, "total are not equal");

    }


    [Test]
    public void multipleNumberTest()
    {
        int expectedValue = 4;
        int total = mathUtil1.multiplyNumbers(2, 2);
        Assert.AreEqual(expectedValue, total, "total are not equal");
    }

    [Test]
    public void divideNumberTest()
    {
        int expectedValue = 2;
        int total = mathUtil1.divideNumbers(4, 2);
        Assert.AreEqual(expectedValue, total, "total are not equal");
    }



}
