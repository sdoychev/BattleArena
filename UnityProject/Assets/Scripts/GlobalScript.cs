using UnityEngine;
using System.Collections;

public class GlobalScript : MonoBehaviour {
    int hero1_score = 0;
    int hero2_score = 0;
    int hero3_score = 0;
    int hero4_score = 0;


    public int maxScore = 3;

    private Score score;

    void Awake()
    {
        Load();
        score = GameObject.Find("Score").GetComponent<Score>();
        maxScore -= 1;// we are programmers and we count from zero
    }

	// Use this for initialization
	void Start () {
        /*
        print("hero1")
        print(hero1_score);
        print("hero2");
        print(hero2_score);
        print("hero3");
        print(hero3_score);
        print("hero4");
        print(hero4_score);*/
        
	}
	
	// Update is called once per frame
	void Update () {

        if (hero1_score >= maxScore)
        {
            WinGame("hero1");
        }else  if (hero2_score >= maxScore)
        {
            WinGame("hero2");
        }else  if (hero3_score >= maxScore)
        {
            WinGame("hero3");
        }else  if (hero4_score >= maxScore)
        {
            WinGame("hero4");
        }

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
        Save();
    }

    void WinGame(string player)
    {
        score.SetGuiText(player + " wins");
        Reset();
    }

    void Save()
    {
        PlayerPrefs.SetInt("hero1_score", hero1_score);
        PlayerPrefs.SetInt("hero2_score", hero2_score);
        PlayerPrefs.SetInt("hero3_score", hero3_score);
        PlayerPrefs.SetInt("hero4_score", hero4_score);
        PlayerPrefs.SetInt("hero1_score", hero1_score);
    }

    void Load()
    {
        hero1_score = PlayerPrefs.GetInt("hero1_score");
        hero2_score = PlayerPrefs.GetInt("hero2_score");
        hero3_score = PlayerPrefs.GetInt("hero3_score");
        hero4_score = PlayerPrefs.GetInt("hero4_score");
    }

    // use after showing the win screen
    void Reset()
    {
        PlayerPrefs.DeleteKey("hero1_score");
        PlayerPrefs.DeleteKey("hero2_score");
        PlayerPrefs.DeleteKey("hero3_score");
        PlayerPrefs.DeleteKey("hero4_score");
    }
}
