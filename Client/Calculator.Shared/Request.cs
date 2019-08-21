using System;
using System.Text;

namespace Calculator.Shared
{
  public class Request
  {
    public Operator Operator { get; set; }
    public int OperandA { get; set; }
    public int OperandB { get; set; }
  }
}
