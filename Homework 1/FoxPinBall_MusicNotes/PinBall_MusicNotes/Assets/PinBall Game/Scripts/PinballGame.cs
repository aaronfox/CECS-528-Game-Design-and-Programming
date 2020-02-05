using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;

public enum PinballGameState {playing, won, lost};

public class PinballGame : MonoBehaviour
{
    public static PinballGame SP;
    public ToggleGroup toggleGroup;

    public Transform ballPrefab;
	
	private int score;
    private PinballGameState gameState;

    private float totalTime = 0;

    private bool hasClickedTryAgainYet = false;

    void Awake()
    {
        SP = this;
        gameState = PinballGameState.playing;
        Time.timeScale = 1.0f; // was 1.0f
        SpawnBall();
    }

    void SpawnBall()
    {
        Instantiate(ballPrefab, new Vector3(0f, 1.0f , 4.75f), Quaternion.identity);
    }

    public void FixedUpdate()
    {
        // Add to total time with a number that is relative to deltaTime so the player earns more points for
        // more difficult play levels
        totalTime = totalTime + 1 * Time.deltaTime;
        Toggle active = toggleGroup.ActiveToggles().ElementAt(0);
        
        //Debug.Log("//" + active.ToString().Substring(0, 1) + "//");
        string letter = active.ToString().Substring(0, 1);
        if (letter == "E")
        {
            Time.timeScale = 1.0f;
        }
        else if (letter == "M")
        {
            Time.timeScale = 2.5f;
        }
        else
        {
            Time.timeScale = 4.0f;
        }
    }

    void OnGUI()
    {
    
        GUILayout.Space(10);
        //GUILayout.Label("  Score: " + score.ToString());

        if (gameState == PinballGameState.lost)
        {
            GUILayout.Label("You Lost!");
            GUILayout.Label("  Score: " + score.ToString());
            if (hasClickedTryAgainYet == false)
            {
                score = score + (int)totalTime;
                hasClickedTryAgainYet = true;
            }
            if (GUILayout.Button("Try again"))
            {
                hasClickedTryAgainYet = false;
                Application.LoadLevel(Application.loadedLevel);
            }
            //gameState = PinballGameState.playing;
        }
        else if (gameState == PinballGameState.won)
        {
            GUILayout.Label("You won!");
            if (GUILayout.Button("Play again"))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }

    public void HitBlock()
    {
		score += 10;
    }

    public void WonGame()
    {
        Time.timeScale = 0.0f; //Pause game
        gameState = PinballGameState.won;
    }

    public void LostBall()
    {
        int ballsLeft = GameObject.FindGameObjectsWithTag("Player").Length;
        if(ballsLeft<=1)
        {
            //Was the last ball..
            SetGameOver();
        }
    }

    public void SetGameOver()
    {
        Time.timeScale = 0.0f; //Pause game
        gameState = PinballGameState.lost;
    }
}
