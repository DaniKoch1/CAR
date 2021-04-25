using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangingManager : MonoBehaviour
{
    public void GoToScene(string sceneName) {
        Debug.Log("***Go to: " + sceneName + "***");
        SceneManager.LoadScene(sceneName);
    }
}
