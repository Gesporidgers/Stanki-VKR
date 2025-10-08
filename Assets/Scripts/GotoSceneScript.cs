using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoSceneScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public string TargetSceneName;
    public void GotoScene() => SceneManager.LoadScene(TargetSceneName);
}
