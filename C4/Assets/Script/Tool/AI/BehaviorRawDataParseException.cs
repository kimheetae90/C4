using System;

public class BehaviorRawDataParseException : Exception
{
	public BehaviorRawDataParseException()
	{
	}
	
	public BehaviorRawDataParseException(string message)
		: base(message)
	{
	}
	
	public BehaviorRawDataParseException(string message, Exception inner)
		: base(message, inner)
	{
	}
}