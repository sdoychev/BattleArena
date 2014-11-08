using UnityEngine;
using System.Collections;

public class ChangeWeapon : MonoBehaviour {

	private string AimHorizontalAxis = "AimHorizontal";
	private string AimVerticalAxis = "AimVertical";

	// Use this for initialization
	void Start () {
	
		switch (gameObject.transform.parent.
		        transform.parent.transform.parent.
		        transform.parent.transform.parent.
		        transform.parent.name) 
		{
		case "hero1":  
			AimHorizontalAxis += "Player1";
			AimVerticalAxis += "Player1";
			break;
		case "hero2":
			AimHorizontalAxis += "Player2";
			AimVerticalAxis += "Player2";
			break;
		case "hero3":
			AimHorizontalAxis += "Player3";
			AimVerticalAxis += "Player3";
			break;
		case "hero4":
			AimHorizontalAxis += "Player4";
			AimVerticalAxis += "Player4";
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		float v1,v2;
		v1 = Input.GetAxis (AimHorizontalAxis);
		v2 = Input.GetAxis (AimVerticalAxis);
		Vector2 v = new Vector2(v1, v2);

		//gameObject.transform.LookAt(v. transform, Vector3.up);
	
	}
}
