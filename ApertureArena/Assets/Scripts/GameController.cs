using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
  private bool isMouseLocked;
  private string[] scenePaths;
  private int currentScene = 0;
  private AssetBundle myAssets;
  private GameObject[] enemies;

  void Start(){
    scenePaths = new string[] {"Level1", "Level2", "Level3", "Level4", "Level5"};
    enemies = GameObject.FindGameObjectsWithTag("Boss");
    StartCoroutine(checkForEnemies());
  }

  IEnumerator checkForEnemies(){
    while(enemies.Length > 0) {
      enemies = GameObject.FindGameObjectsWithTag("Boss");
      Debug.Log(enemies.Length);
      yield return new WaitForSeconds(2f);
    }
    LoadNextScene();
  }

  public bool isLocked(){
    return isMouseLocked;
  }

  public void LockMouse(bool toLock) {
      if(toLock == isMouseLocked) {
        return;
      } else if (toLock == true){
        isMouseLocked = true;
        Cursor.lockState = CursorLockMode.Locked;
      } else {
        isMouseLocked = false;
        Cursor.lockState = CursorLockMode.None;
      }
  }

  public void LoadNextScene(){
    if (currentScene < 4){
      currentScene++;
      SceneManager.LoadScene(scenePaths[currentScene], LoadSceneMode.Single);
      //SceneManager.UnloadSceneAsync(scenePaths[currentScene - 1]);
    } {
      //quit the game
      Application.Quit();
    }
  }

  //Works
  public void ReloadScene() {
    //Debug.Log("Reloading Scene");
    //SceneManager.LoadScene(scenePaths[currentScene], LoadSceneMode.Single);
    Scene currentScene = SceneManager.GetActiveScene();
    SceneManager.LoadScene(currentScene.name);
  }
}
