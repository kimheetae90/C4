using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMessagePooler{

	Stack<GameMessage> pool;

	public GameMessagePooler()
	{
		pool = new Stack<GameMessage> ();
		Allocate ();
	}

	void Allocate()
	{
		for (int i=0; i<10; i++) 
		{
			pool.Push(new GameMessage());
		}
	}

	public GameMessage Pop()
	{
			if (pool.Count <= 0)
			{
				Allocate();
			}
			
			return pool.Pop();
	}
	
	public void Push(GameMessage packet)
	{
			pool.Push(packet);
	}
}
