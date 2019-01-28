using Microsoft.VisualStudio.TestTools.UnitTesting;
using debugws3;

namespace Tests
{
  [TestClass]
  public class ErrorHandling
  {
    [TestMethod]
    public void ShouldErrorOnCrap()
    {
      var result = Calc.Calculate(null);
      Assert.IsFalse(result.err.Length == 0, "should not be empty");
    }

    [DataTestMethod]
    [DataRow("")]
    [DataRow("x")]
    [DataRow("2z^2+z+c")]
    [DataRow("0*FF+1")]
    [DataRow("ABC")]
    public void DoErrors(string value)
    {
      var result = Calc.Calculate(value);
      Assert.IsFalse(result.err.Length == 0, $"{value} should yield an error");
    }
  }

  [TestClass]
  public class Basics
  {
    [DataTestMethod]
    [DataRow("3+3")]
    [DataRow("2 + 4")]
    [DataRow(" 4+2 ")]
    [DataRow("5+   1")]
    [DataRow(" 6 + 0 ")]
    public void DoPlus(string value)
    {
      var result = Calc.Calculate(value);
      Assert.IsTrue(result.result == 6, $"{value} should be 6");
    }

    [DataTestMethod]
    [DataRow(" 3-2")]
    [DataRow("2- 1")]
    [DataRow(" 4 - 3 ")]
    [DataRow("5-     4")]
    [DataRow("    6-5")]
    public void DoMinus(string value)
    {
      var result = Calc.Calculate(value);
      Assert.IsTrue(result.result == 1, $"{value} should be 1");
    }

    [DataTestMethod]
    [DataRow("1+3*3",10)]
    [DataRow("6/3+7-1",9)]
    [DataRow("9/3+2",5)]
    public void TestPrecedence(string value,double res){
      var result = Calc.Calculate(value);
      Assert.IsTrue(result.result == res, value + " should be " + res);
    }

    [DataTestMethod]
    [DataRow(4,0,new int[] {1,3,5,6,8,9},new int[] {4,5,6,8,9})]
    
    public void testResizeArray(int newVal,int pos,int[] arr, int[] newArr){
      int[] res = Calc.resizeArray(newVal,pos,arr);
       
       CollectionAssert.AreEqual(newArr,res);
    }

    [DataTestMethod]
    [DataRow(new int[]{1,3,3},new char[]{'+','*'},new int[]{1,9})]
    public void testArrangeNumber(ref int[] numbers,ref char[] operators,int[] res){
          Calc.ArrangeNumber(ref numbers,ref operators );

          CollectionAssert.AreEqual(res,numbers);
          
          
    }

    [DataTestMethod]
    [DataRow(new char[]{'+','*'},1,new char[]{'+'})]
    public void testRemoveOperator(char[] op,int ind,char[] res){
          char[] resOp = Calc.removeOperator(op,ind);

          CollectionAssert.AreEqual(res,resOp);
          
          
    }
  }
}
