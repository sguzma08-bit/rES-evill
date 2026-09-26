using UnityEngine;
using UnityEngine.SceneManagement;

public class KioskJump : MonoBehaviour
{
    // Must be exactly public, void, and accept a string parameter
    public void LoadTargetScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
