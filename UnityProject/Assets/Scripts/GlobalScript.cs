using UnityEngine;
using System.Collections;

public class GlobalScript : MonoBehaviour {
    int hero1_score = 0;
    int hero2_score = 0;
    int hero3_score = 0;
    int hero4_score = 0;


    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void UpdatePlayerScore(string player, int score)
    {
        switch (player)
        {
            case "hero1":
                hero1_score += score;
                break;
            case "hero2":
                hero2_score += score;
                break;
            case "hero3":
                hero3_score += score;
                break;
            case "hero4":
                hero4_score += score;
                break;
        }
    }
}
