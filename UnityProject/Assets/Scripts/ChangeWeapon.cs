using UnityEngine;
using System.Collections;

public class ChangeWeapon : MonoBehaviour {

	private string AimHorizontalAxis = "AimHorizontal";
	private string AimVerticalAxis = "AimVertical";
	private PlayerControl playerCtrl;

	void Awake()
	{
		playerCtrl = gameObject.transform.root.GetComponent<PlayerControl>();
	}

	// Use this for initialization
	void Start () {
	
		switch (gameObject.transform.root.name) 
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
		int heading = 0;
		if(playerCtrl.facingRight){
			heading = -1;
		}else{
			heading = 1;
		}

		if (0.01f > v1 && v1 > -0.01 && 0.01f  > v2 && v2 > -0.01)
		{
			v1 = -heading*1.0f;
			v2 = 0.0f;
		}
		Vector3 controllerInput = new Vector3 (v1*10, v2*10, 0);
		Vector3 player_pos = gameObject.transform.root.position;
		Vector3 mouse_pos = player_pos + controllerInput;

		mouse_pos.x = (mouse_pos.x - player_pos.x) * heading ;
		mouse_pos.y = (mouse_pos.y - player_pos.y)  ;

		float angle = Mathf.Atan2 (mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
		this.transform.rotation = Quaternion.Euler (new Vector3(0, 0, angle));

	}
}
