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

    private void SaveGame()
    {
        // TODO save logic, either in this or in another script
    }
}