using System;
using System.Text;

namespace Calculator.Shared
{

  public interface ISerializer
  {
    byte [] Serialize (Request request);
    byte [] Serialize (Response response);

    Request DeserializeRequest (byte [] bytes);
    Response DeserializeResponse (byte [] bytes);
  }
  
}
