using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BehaviorRawNodeData {
    public int id;
    public string param;
	public string type;
	public double priority;

	public BehaviorRawNodeData parant;
	public List<BehaviorRawNodeData> childs;

	public BehaviorRawNodeData()
	{
		parant = null;
		childs = new List<BehaviorRawNodeData> ();
		priority = 0;
	}
}
