using UnityEngine;
using System.Collections;

public class Remover : MonoBehaviour
{
	public GameObject splash;
    int deathGamecount = 0;
    int maxDeaths = 2;


	void OnTriggerEnter2D(Collider2D col)
	{
		// If the player hits the trigger...
		if(col.gameObject.tag == "Player")
		{
			// .. stop the camera tracking the player
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>().enabled = false;

			// .. stop the Health Bar following the player
			if(GameObject.FindGameObjectWithTag("HealthBar").activeSelf)
			{
				GameObject.FindGameObjectWithTag("HealthBar").SetActive(false);
			}

			// ... instantiate the splash where the player falls in.
			Instantiate(splash, col.transform.position, transform.rotation);
			// ... destroy the player.
			Destroy (col.gameObject);
			// ... reload the level.
            if (deathGamecount == maxDeaths)
            {
                GameObject[] lastPlayer = GameObject.FindGameObjectsWithTag("Player");
                foreach(GameObject player in lastPlayer)
                {

                    if (!player.GetComponent<PlayerHealth>().IsDead())
                    {
                        GameObject globalScript = GameObject.Find("GlobalObject");
                        if (globalScript)
                        {
                            globalScript.GetComponent<GlobalScript>().UpdatePlayerScore(player.transform.name, 1);
                        }
                    }
                }
                deathGamecount = 0;
                StartCoroutine("ReloadGame");


            }
            else
            {
                deathGamecount++;
            }
		}
		else
		{
			// ... instantiate the splash where the enemy falls in.
			Instantiate(splash, col.transform.position, transform.rotation);

			// Destroy the enemy.
			Destroy (col.gameObject);	
		}
	}

	IEnumerator ReloadGame()
	{			
		// ... pause briefly
		yield return new WaitForSeconds(2);
		// ... and then reload the level.
		Application.LoadLevel(Application.loadedLevel);
	}
}
