using System;

public class BehaviorNodeException : Exception {

	public BehaviorNodeException()
	{
	}
	
	public BehaviorNodeException(string message)
		: base(message)
	{
	}

    public BehaviorNodeException(string message, Exception inner)
		: base(message, inner)
	{
	}
}