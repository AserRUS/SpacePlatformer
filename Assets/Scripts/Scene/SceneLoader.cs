using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public event UnityAction LevelFinishedLoading;
    /*
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            Restart();
        }
    }
    */
    private void Awake()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    public void LoadMainMenu()
    {        
       LoadScene("MainMenu");
    }
    public void Restart()
    {
        LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        LevelFinishedLoading?.Invoke();
    }
}
