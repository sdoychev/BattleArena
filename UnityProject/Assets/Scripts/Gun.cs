﻿using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
	public Rigidbody2D rocket;				// Prefab of the rocket.
	public float speed = 20f;				// The speed the rocket will fire at.
	public float fireRate;
	private float fireTimer = 0f;
	public bool applyForceToBullet;
	
	private PlayerControl playerCtrl;		// Reference to the PlayerControl script.
	private Animator anim;					// Reference to the Animator component.
	
	private string FireButton = "Fire";
	private string AimHorizontalAxis = "AimHorizontal";
	private string AimVerticalAxis = "AimVertical";
	
	
	
	
	void Awake()
	{
		// Setting up the references.
		anim = transform.root.gameObject.GetComponent<Animator>();
		playerCtrl = transform.root.GetComponent<PlayerControl>();
		
		switch (gameObject.transform.parent.transform.parent.transform.name) 
		{
		case "hero1":  
			FireButton += "Player1";
			AimHorizontalAxis += "Player1";
			AimVerticalAxis += "Player1";
			break;
		case "hero2":
			FireButton += "Player2";
			AimHorizontalAxis += "Player2";
			AimVerticalAxis += "Player2";
			break;
		case "hero3":
			FireButton += "Player3";
			AimHorizontalAxis += "Player3";
			AimVerticalAxis += "Player3";
			break;
		case "hero4":
			FireButton += "Player4";
			AimHorizontalAxis += "Player4";
			AimVerticalAxis += "Player4";
			break;
		}
	}
	
	
	void Update ()
	{
		// If the fire button is pressed...
		//if(Input.GetButtonDown(FireButton))
		
		fireTimer -= Time.deltaTime;
		
		if( Input.GetAxisRaw(FireButton) != 0  && fireTimer <= 0.1f)
		{
			fireTimer = fireRate;
			
			// ... set the animator Shoot trigger parameter and play the audioclip.
			anim.SetTrigger("Shoot");
			audio.Play();
			
			float v1,v2;
			v1 = Input.GetAxis (AimHorizontalAxis);
			v2 = Input.GetAxis (AimVerticalAxis);
			
			Vector2 v = new Vector2(v1, v2);
			v.Normalize();
			v1 = v.x;
			v2 = v.y;
			
			
			
			// If the player is facing right...
			if(playerCtrl.facingRight)
			{
				if (0.01f > v1 && v1 > -0.01 && 0.01f  > v2 && v2 > -0.01)
				{
					v1 = 1.0f;
					v2 = 0.0f;
				}
				// ... instantiate the rocket facing right and set it's velocity to the right. 
				Rigidbody2D bulletInstance = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
				Vector2 vel = new Vector2(v1*speed, -v2*speed);
				if (applyForceToBullet)
				{
					bulletInstance.AddForce(vel);
				}
				else
				{
					bulletInstance.velocity = vel;
				}
			}
			else
			{
				if (0.01f > v1 && v1 > -0.01 && 0.01f  > v2 && v2 > -0.01)
				{
					v1 = -1.0f;
					v2 = 0.0f;
				}
				
				// Otherwise instantiate the rocket facing left and set it's velocity to the left.
				Rigidbody2D bulletInstance = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0,0,180f))) as Rigidbody2D;
				Vector2 vel = new Vector2(v1 * speed, -v2 * speed);
				if (applyForceToBullet)
				{
					bulletInstance.AddForce(vel);
				}
				else
				{
					bulletInstance.velocity = vel;
				}
			}
		}
	}
}