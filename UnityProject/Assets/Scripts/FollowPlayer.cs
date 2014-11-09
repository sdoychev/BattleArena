using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{
	public Vector3 offset;			// The offset at which the Health Bar follows the player.
	
	private Transform player;		// Reference to the player.

	private string playerGameObject = "hero";

	void Awake ()
	{/*
		switch (gameObject.transform.parent.transform.name) 
		{
			case "hero1":  
			playerGameObject += "1";
			break;
			case "hero2":
			playerGameObject += "2";
			break;
			case "hero3":
			playerGameObject += "3";
			break;
			case "hero4":
			playerGameObject += "4";
			break;
		}*/

		offset.y = 5.3f;

		// Setting up the reference.
		//player = GameObject.FindGameObjectWithTag("Player").transform;
        player = GameObject.Find(gameObject.transform.parent.transform.name).transform;
	}

	void Update ()
	{
		// Set the position to the player's position with the offset.
		transform.position = player.position + offset;
	}
}
