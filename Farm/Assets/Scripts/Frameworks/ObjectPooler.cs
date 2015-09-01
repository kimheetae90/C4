using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ObjectPooler : MonoBehaviour {
	
	//////////////////////////////////////////////////////////////////////////////
	private static ObjectPooler _instance;
	public static ObjectPooler Instance
	{
		get
		{
			if (!_instance)
			{
				_instance = GameObject.FindObjectOfType(typeof(ObjectPooler)) as ObjectPooler;
				if (!_instance)
				{
					GameObject container = new GameObject();
					container.name = "ObjectPooler";
					_instance = container.AddComponent(typeof(ObjectPooler)) as ObjectPooler;
				}
			}
			
			return _instance;
		}
	}   
	//////////////////////////////////////////////////////////////////////////////

	Dictionary<string, GameObject> prefabPool;
	List<string> onMemoryObjectNameList;
	List<GameObject> objectPool;
	Queue<int> objectIDQue;

	void Awake()
	{
		DontDestroyOnLoad (this);
		prefabPool = new Dictionary<string, GameObject> ();
		onMemoryObjectNameList = new List<string> ();
		objectPool = new List<GameObject> ();
		objectIDQue = new Queue<int> ();

		for(int i=0;i<200;i++)
		{
			objectIDQue.Enqueue(i);
		}
	}

	public void RegisterPrefab(string _name)
	{
		GameObject tempPrefab;
		if (!prefabPool.TryGetValue (_name, out tempPrefab)) {
			Register(_name);
		}
		else 
		{
			LogManager.log ("Error : 이미 등록된 prefab임");
		}
	}

	void Register(string _name)
	{
		GameObject tempPrefab;
		tempPrefab = Resources.Load ("Prefabs/" + _name) as GameObject;
		prefabPool.Add (_name, tempPrefab);
		onMemoryObjectNameList.Add(_name);
	}

	public GameObject GetGameobject(int _idx)
	{
		return objectPool.Find (result => result.GetComponent<BaseObject> ().id == _idx);
	}

	public GameObject GetGameObject(string _name)
	{
		GameObject tempPrefab;
		GameObject tempGameObject;

		if (!prefabPool.TryGetValue (_name, out tempPrefab)) 
		{
			Register(_name);
			tempPrefab = prefabPool[_name];
		}
		
		tempGameObject = Instantiate(tempPrefab);
		objectPool.Add (tempGameObject);
		tempGameObject.GetComponent<BaseObject>().id = GetCurrentID();
		return tempGameObject;
	}

	public void ClearPrefab()
	{
		prefabPool.Clear ();
		onMemoryObjectNameList.Clear ();
	}

	public void RemoveOnMemoryPrefab(string _name)
	{
		prefabPool.Remove (_name);
		onMemoryObjectNameList.Remove (_name);
	}

	public void Destroy(BaseObject _object)
	{
		objectPool.Remove (_object.gameObject);
		objectIDQue.Enqueue (_object.id);
	}

	int GetCurrentID()
	{
		if (objectIDQue.Count <= 0) 
		{
			objectIDQue.Enqueue(objectIDQue.Count);
		} 

		return objectIDQue.Dequeue();
	}
}
