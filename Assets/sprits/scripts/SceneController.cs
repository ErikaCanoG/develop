using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void changeScene(int _nivel){
      Debug.Log("CALL -> changeScene: " + _nivel);
      SceneManager.LoadScene(_nivel);
   }

   public void quitGame(){
      #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
      #endif
      Application.Quit();
   }
}
