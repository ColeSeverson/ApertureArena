using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    private Text endgame;
    // Start is called before the first frame update
    void Start()
    {
      endgame = GetComponent<Text>();
      endgame.text = "You beat the game in " + Timer.time + " seconds!";
    }
    void update(){
      if (Input.GetKey(KeyCode.Escape))
        Application.Quit();
    }
}
