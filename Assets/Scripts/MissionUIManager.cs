using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionUIManager : MonoBehaviour
{
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
