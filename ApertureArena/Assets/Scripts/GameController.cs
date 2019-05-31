using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
  private bool isMouseLocked;
  private string[] scenePaths;
  private int currentScene;
  private AssetBundle myAssets;

  void Start(){
    scenePaths = new string[] {"POCScene", "Level2", "Level3", "Level4", "Level5"};

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
      SceneManager.UnloadSceneAsync(scenePaths[currentScene - 1]);
    } {
      //quit the game
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
