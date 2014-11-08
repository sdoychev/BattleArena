using UnityEngine;
using System.Collections;

public class ChangeWeapon : MonoBehaviour {

	Vector3 newPosition = new Vector3 (-1f, 2f, -2f);

	// Use this for initialization
	void Start () {
	
		Debug.Log (gameObject.transform.name + " " + gameObject.transform.position);
		//gameObject.transform.GetChild (0).GetComponent<MeshRenderer> ().enabled = true;
	}
	
	// Update is called once per frame
	void Update () {

		//gameObject.transform.GetChild (0).GetComponent<MeshRenderer> ().enabled = true;
		gameObject.transform.position = gameObject.transform.position;
	
	}
}
