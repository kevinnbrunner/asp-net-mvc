using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Demo.Entities {
  public enum SerializationType {
    Json,
    NewtonJSON,
    fastJSON,
    Base64FastJson,
    ProtocolBuffers,
    fastJSONWithTypes,
    XML,
    BinaryDotNet,
  }
}
