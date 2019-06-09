using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using System;

/*
  * Writen by Cole Severson
*/

//object that controls the scene changes in the game
public class GameController : MonoBehaviour
{
  private bool isMouseLocked;
  private AssetBundle myAssets;
  private GameObject[] enemies;
  private int currentTime;
  public Text timer;
  Scene currentScene;

  void Start(){
    //scenePaths = new string[] {"Level1", "Level2", "Level3", "Level4", "Level5"};
    enemies = GameObject.FindGameObjectsWithTag("Boss");
    currentScene = SceneManager.GetActiveScene();
    currentTime = Timer.time;
    StartCoroutine(checkForEnemies());
    StartCoroutine(updateTimer());
  }
  IEnumerator updateTimer(){
    while(currentTime < 60000) {
      currentTime += 1;
      timer.text = "Time: " + currentTime;
      yield return new WaitForSeconds(1f);
    }
  }
  IEnumerator checkForEnemies(){
    while(enemies.Length > 0) {
      enemies = GameObject.FindGameObjectsWithTag("Boss");
      Debug.Log(enemies.Length);
      yield return new WaitForSeconds(2f);
    }
    Timer.time = currentTime;
    LoadNextScene();
  }

  //controls the mouse locking
  public bool isLocked(){
    return isMouseLocked;
  }

  public void LockMouse(bool toLock) {
      if(toLock == isMouseLocked) {
        return;
      } else if (toLock == true){
        isMouseLocked = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
      } else {
        isMouseLocked = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
      }
  }

  //controls scene loading
  public void LoadNextScene(){
    SceneManager.LoadScene(currentScene.buildIndex + 1);
  /*  if (currentScene.buildIndex < 5){
      //currentSceneNum++;
      SceneManager.LoadScene(currentScene.buildIndex + 1);
      //SceneManager.UnloadSceneAsync(scenePaths[currentScene - 1]);
    } else {
      //quit the game
      //Application.Quit();
    }*/
  }

  //Works
  public void ReloadScene() {
    //Debug.Log("Reloading Scene");
    //SceneManager.LoadScene(scenePaths[currentScene], LoadSceneMode.Single);
    SceneManager.LoadScene(currentScene.name);
  }
}
