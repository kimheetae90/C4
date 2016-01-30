using UnityEngine;
using System.Collections;

public class CGrid : MonoBehaviour {

	public int line;
	public int time;
	public int id;


	public void SetMarker(Texture _texture, int _id)
	{
		GetComponent<Renderer> ().material.mainTexture = _texture;
		id = _id;
	}

	public void SetMarkerNull(Texture _texture)
	{
		GetComponent<Renderer> ().material.mainTexture = _texture;
		id = 0;
	}
}
