using System;
using System.Text;

namespace Calculator.Shared
{

  public class CustomTextSerializer : ISerializer
  {
    public byte [] Serialize (Request request)
    {
      var text = string.Format ("{0},{1},{2}",
          request.Operator,
          request.OperandA,
          request.OperandB);

      return Encoding.UTF8.GetBytes (text);
    }

    public Request DeserializeRequest (byte [] bytes)
    {
      var text = Encoding.UTF8.GetString (bytes);
      var parts = text.Split (',');
      var op = (Operator)Enum.Parse (typeof (Operator), parts [0]);
      var operandA = int.Parse (parts [1]);
      var operandB = int.Parse (parts [2]);

      return new Request {
        Operator = op,
        OperandA = operandA,
        OperandB = operandB
      };
    }

    public byte [] Serialize (Response response)
    {
      var text = string.Format (
          "{0},{1}",
          response.Success,
          response.Result);

      return Encoding.UTF8.GetBytes (text);
    }

    public Response DeserializeResponse (byte [] bytes)
    {
      var text = Encoding.UTF8.GetString (bytes);
      var parts = text.Split (',');
      var success = bool.Parse (parts [0]);
      var result = int.Parse (parts [1]);

      return new Response {
        Success = success,
        Result = result
      };
    }
  }
}
