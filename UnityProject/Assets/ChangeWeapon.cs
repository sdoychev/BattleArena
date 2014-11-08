using UnityEngine;
using System.Collections;

public class ChangeWeapon : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		Debug.Log (gameObject.transform.name + " " + gameObject.transform.position);
		//gameObject.transform.GetChild (0).GetComponent<MeshRenderer> ().enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
