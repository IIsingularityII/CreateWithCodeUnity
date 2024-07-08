using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveAndScoreCounter : MonoBehaviour
{
    private int Lives = 3;
    private int minLives = 1;
    private int Score = 0;
    
    public void decreaseHP()
    {
        if(Lives > minLives)
        {
            Lives--;
            Debug.Log("Lives = "+Lives);
        }
        else
        {
            Lives = 0;
            Debug.Log("Lives = 0");
            Debug.Log("Game over!");
        }
    }
    public void increaseScore()
    {
        if(stillHasLives()) Debug.Log("Score = " + ++Score);
    }
    public bool stillHasLives()
    {
        return (Lives > 0);
    }
}
