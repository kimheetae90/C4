using UnityEngine;
using UnityEditor;
using System.Collections;
 
public class AnimationEventEditor : MonoBehaviour {

	public bool doWindow0 = true;
	Animator anim;

	// Use this for initialization
	void Start () {

	 	anim = GetComponent<Animator>();

		if (anim == null) {
			throw new ToolException("Doesn't have Animator Component");
		}
	}

	// Update is called once per frame
	void Update () {
		AnimatorClipInfo[] infos = anim.GetCurrentAnimatorClipInfo (0);

		foreach (var info in infos) {
			Debug.Log(info.clip.name);
		}

	}

	void OnGUI() {

		doWindow0 = GUI.Toggle(new Rect(10, 10, 100, 20), doWindow0, "Window 0");

		if (doWindow0)
			GUI.Window(0, new Rect(110, 10, 200, 60), DoWindow0, "Basic Window");
	}

	void DoWindow0(int windowID) {
		GUI.Button(new Rect(10, 30, 80, 20), "Click Me!");
	}
}
