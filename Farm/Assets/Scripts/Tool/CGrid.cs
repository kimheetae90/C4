using UnityEngine;
using System.Collections;

public class CGrid : MonoBehaviour {

	public int line;
	public int time;
	public int marker;


	public void SetMarker(Texture _texture, int _marker)
	{
		GetComponent<Renderer> ().material.mainTexture = _texture;
		marker = _marker;
	}

	public void SetMarkerNull(Texture _texture)
	{
		GetComponent<Renderer> ().material.mainTexture = _texture;
		marker = 0;
	}
}
