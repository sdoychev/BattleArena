using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {

	public Texture2D startButtonTexture;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI () {

		GUI.backgroundColor = Color.clear;

		if (GUI.Button(new Rect(Screen.width/2-440/2,0,384,150), startButtonTexture)) {
			Application.LoadLevel ("Level");
		}

	}
}
