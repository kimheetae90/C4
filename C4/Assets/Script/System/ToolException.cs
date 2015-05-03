using System;

public class ToolException : Exception
{
	public ToolException()
	{
	}
	
	public ToolException(string message)
		: base(message)
	{
	}
	
	public ToolException(string message, Exception inner)
		: base(message, inner)
	{
	}
}