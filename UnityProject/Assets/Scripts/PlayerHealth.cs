﻿using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{	
	public float health = 100f;					// The player's health.
	public float repeatDamagePeriod = 2f;		// How frequently the player can be damaged.
	public AudioClip[] ouchClips;				// Array of clips to play when the player is damaged.
	public float hurtForce = 10f;				// The force with which the player is pushed when hurt.
	public float kDamageAmount = 10f;			// The amount of damage to take when enemies touch the player

	private SpriteRenderer healthBar;			// Reference to the sprite renderer of the health bar.
    private GameObject healthBarObject;
	private float lastHitTime;					// The time at which the player was last hit.
	private Vector3 healthScale;				// The local scale of the health bar initially (with full health).
	private PlayerControl playerControl;		// Reference to the PlayerControl script.
	private Animator anim;						// Reference to the Animator on the player


    private bool died;
	private string playerHealthBar = "HealthBar";

	void Awake ()
	{
        died = false;
		switch (gameObject.transform.name) 
		{
		case "hero1":  
			playerHealthBar += "Player1";
			break;
		case "hero2":
			playerHealthBar += "Player2";
			break;
		case "hero3":
			playerHealthBar += "Player3";
			break;
		case "hero4":
			playerHealthBar += "Player4";
			break;
		}

		// Setting up references.
		playerControl = GetComponent<PlayerControl>();
		healthBarObject = GameObject.Find(playerHealthBar);
        healthBar = healthBarObject.GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();

		// Getting the intial scale of the healthbar (whilst the player has full health).
		healthScale = healthBar.transform.localScale;
	}


	void OnCollisionEnter2D (Collision2D col)
	{
		// If the colliding gameobject is an Enemy...
		if(col.gameObject.tag == "Enemy" )
		{
			// ... and if the time exceeds the time of the last hit plus the time between hits...
			if (Time.time > lastHitTime + repeatDamagePeriod) 
			{
				// ... and if the player still has health...
				if(health > 0f)
				{
					// ... take damage and reset the lastHitTime.
					TakeDamage(col.transform); 
					lastHitTime = Time.time; 
				}
				// If the player doesn't have health, do some stuff, let him fall into the river to reload the level.
				else
				{
                    Die();
				}
			}
		}
	}


	public void TakeDamage (Transform enemy)
	{
		// Make sure the player can't jump.
		playerControl.jump = false;

		// Create a vector that's from the enemy to the player with an upwards boost.
		Vector3 hurtVector = transform.position - enemy.position + Vector3.up * 5f;

		// Add a force to the player in the direction of the vector and multiply by the hurtForce.
		rigidbody2D.AddForce(hurtVector * hurtForce);

		// Reduce the player's health by 10.
        ApplyDamage(kDamageAmount);

		
	}

    public void ApplyDamage(float damageAmount)
    {
        health -= damageAmount;
        // Update what the health bar looks like.
        if(!died)
            UpdateHealthBar();
        if (health > 0.0f)
        {
            // Play a random clip of the player getting hurt.
            int i = Random.Range(0, ouchClips.Length);
            AudioSource.PlayClipAtPoint(ouchClips[i], transform.position);
        }
        else
        {
            died = true;
            Die();
        }
    }

	public void UpdateHealthBar ()
	{
		// Set the health bar's colour to proportion of the way between green and red based on the player's health.
		healthBar.material.color = Color.Lerp(Color.green, Color.red, 1 - health * 0.01f);

		// Set the scale of the health bar to be proportional to the player's health.
		healthBar.transform.localScale = new Vector3(healthScale.x * health * 0.01f, 1, 1);
	}

    public bool IsDead()
    {
        return health <= 0.0f;
    }

    void Die()
    {
        // Find all of the colliders on the gameobject and set them all to be triggers.
        Collider2D[] cols = GetComponents<Collider2D>();
        foreach (Collider2D c in cols)
        {
            c.isTrigger = true;
        }

        // Move all sprite parts of the player to the front
        SpriteRenderer[] spr = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer s in spr)
        {
            s.sortingLayerName = "UI";
        }

        // ... disable user Player Control script
        GetComponent<PlayerControl>().enabled = false;

        // ... disable the Gun script to stop a dead guy shooting a nonexistant bazooka
        GetComponentInChildren<Gun>().enabled = false;

        // ... Trigger the 'Die' animation state
        anim.SetTrigger("Die");
    }

    public GameObject getPlayerHealthBar()
    {
        return healthBarObject.transform.parent.gameObject;
    }
}
