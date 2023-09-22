using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{

    public void DestroyMenu() => Destroy(transform.root.gameObject);
    public void ReturnToMainMenu() {
        SaveGame();
        SceneManager.LoadScene("MainMenu");
    }
    
    private void SaveGame() {  
        // TODO save logic, either in this or in another script
    }

}
