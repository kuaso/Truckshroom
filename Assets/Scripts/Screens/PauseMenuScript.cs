using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    
    public delegate void MenuDestroyed();
    public static event MenuDestroyed OnMenuDestroyed;

    public void DestroyMenu()
    {
        Destroy(transform.root.gameObject);
        OnMenuDestroyed?.Invoke();
    }

    public void ReturnToMainMenu() {
        SaveGame();
        SceneManager.LoadScene("MainMenu");
        // MenuDestroyed is still invoked as the scene it is no longer active in the new scene
        OnMenuDestroyed?.Invoke();
    }
    
    private void SaveGame() {  
        // TODO save logic, either in this or in another script
    }

}
