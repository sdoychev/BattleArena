using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour {

	public Texture2D controlTexture;
	public Texture2D startButtonTexture;

	public Texture2D creditsButtonTexture;
	public Texture2D quitButtonTexture;

	int offset = 88;
	int voffset = 124;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI () {

		GUI.backgroundColor = Color.clear;

		if (GUI.Button(new Rect(Screen.width/2 - 256 + voffset,0 + offset,384,192), startButtonTexture)) {
			Debug.Log("START GAME");
			Application.LoadLevel ("Level");
		}

		if (GUI.Button(new Rect(Screen.width/2 - 256 + voffset,128 + offset,384,192), creditsButtonTexture)) {
			Debug.Log("CREDITS");
			Application.LoadLevel ("Credits");
		}

		if (GUI.Button(new Rect(Screen.width/2 - 256 + voffset,256 + offset,384,192), quitButtonTexture)) {
			Debug.Log("QUIT");
			Application.Quit();
		}

	}
}
