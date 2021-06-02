using System;

[Serializable]
class GestionDeErroresPropiosException : Exception
{
    public GestionDeErroresPropiosException()
    {}

    public GestionDeErroresPropiosException(string mensaje)
        : base(mensaje)
    {}
    public GestionDeErroresPropiosException (string message, Exception innerException)
        : base (message, innerException)
    {}
  
}