using UnityEngine;
using System.Collections;

public class ChangeWeapon : MonoBehaviour {

	private string AimHorizontalAxis = "AimHorizontal";
	private string AimVerticalAxis = "AimVertical";

	// Use this for initialization
	void Start () {
	
		switch (gameObject.transform.parent.transform.parent.
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

		Vector3 controllerInput = new Vector3 (v1*10, v2*10, 0);

		Vector3 mouse_pos = Input.mousePosition;
		Vector3 player_pos = Camera.main.WorldToScreenPoint(this.transform.position);

		mouse_pos = player_pos + controllerInput;

		if (gameObject.transform.parent.transform.parent.
		    transform.parent.transform.parent.
		    transform.parent.transform.parent.
		    transform.parent.name == "hero2") 
		{
			//Debug.Log ("mouse_pos.x" + mouse_pos.x);
			//Debug.Log ("mouse_pos.y" + mouse_pos.y);
		}
		
		mouse_pos.x = (mouse_pos.x - player_pos.x) * -1 ;
		mouse_pos.y = (mouse_pos.y - player_pos.y)  ;

		if (gameObject.transform.parent.transform.parent.
		    transform.parent.transform.parent.
		    transform.parent.transform.parent.
		    transform.parent.name == "hero2") 
		{
			//Debug.Log ("mouse_pos.x" + mouse_pos.x);
			//Debug.Log ("mouse_pos.y" + mouse_pos.y);
		}
		
		float angle = Mathf.Atan2 (mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
		this.transform.rotation = Quaternion.Euler (new Vector3(0, 0, angle));

	}
}
