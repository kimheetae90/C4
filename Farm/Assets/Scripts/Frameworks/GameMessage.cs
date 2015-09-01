using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMessage{

	public MessageName messageName;
	Dictionary<string, object> messageDic;

	public GameMessage()
	{
		messageDic = new Dictionary<string,object> ();
	}

	public static GameMessage Create(MessageName _messageName)
	{
		GameMessage gameMessage = GameMaster.Instance.gameMessageManager.Pop ();
		gameMessage.messageName = _messageName;
		gameMessage.messageDic.Clear();
		return gameMessage;
	}

	public void Insert(string _name, object _data)
	{
		object tempValue;
		if (messageDic.TryGetValue (_name, out tempValue)) 
		{
				LogManager.log ("Error : " + _name + "의 Key가 이미 있음");
				return ;
		}

		messageDic.Add (_name,_data);
	}

	public object Get(string _name)
	{
		object value;
		if (messageDic.TryGetValue (_name, out value)) 
		{
			return value;
		}
		else
		{
			Debug.Log("GameMessage Error : " + _name + "의 key값이 없음");
			return null;
		}
	}

	public void Clear()
	{
		messageDic.Clear();
	}

	public void Update(string _name, object _data)
	{
		object value;
		if (messageDic.TryGetValue (_name, out value)) 
		{
			messageDic[_name] = _data;
		}
		else
		{
			Debug.Log("GameMessage Error : " + _name + "의 key값이 없음");
		}
	}

    public object Remove(string _name)
	{
		object value;
		if (messageDic.TryGetValue (_name, out value)) 
		{
			messageDic.Remove(_name);
			return value;
		}
		else
		{
			Debug.Log("GameMessage Error : " + _name + "의 key값이 없음");
			return null;
		}
    }

	public void Destroy()
	{
		messageDic.Clear ();
		GameMaster.Instance.gameMessageManager.Push(this);
	}
}
