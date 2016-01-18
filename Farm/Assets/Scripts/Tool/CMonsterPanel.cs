using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CMonsterPanel : MonoBehaviour {

	Text text;
	public int id;
	public int mark;

	// Use this for initialization
	void Start () {
		text = GetComponentInChildren<Text> ();
	}

	public void SetNull()
	{
		text.text = "";
		mark = 0;
	}

	public void SetMarker(int _value)
	{
		text.text = _value.ToString ();
		mark = _value;
	}
}
