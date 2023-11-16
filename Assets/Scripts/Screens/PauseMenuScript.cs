using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public delegate void OnMenuDestroyed();

    public static event OnMenuDestroyed MenuDestroyed;

    public void DestroyMenu()
    {
        Destroy(transform.root.gameObject);
        MenuDestroyed?.Invoke();
    }

    public void ReturnToMainMenu()
    {
        SaveGame();
        SceneManager.LoadScene("MainMenu");
        // MenuDestroyed is still invoked as the scene it is no longer active in the new scene
        MenuDestroyed?.Invoke();
    }

    private static void SaveGame()
    {
        // Since we're not storing much data, it doesn't make much sense to create a whole new json data manager
        PlayerPrefs.SetString("LastPlayedLevel", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();
    }
}