using System;

namespace Chain.Interfaces.Exceptions
{
	[Serializable]
	public class InterpreterException : Exception
	{
		public InterpreterException() : base() { }
		public InterpreterException(string message) : base(message) { }
		public InterpreterException(string message, Exception innerException) : base(message, innerException) { }
	}
}
