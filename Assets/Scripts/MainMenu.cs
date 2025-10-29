using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    //load starting scene for start button
    public void startGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    //load story scene for story button 
    public void loadStory() 
    {
        SceneManager.LoadScene("IntroScene");
    }

    public void quit() 
    {
        Application.Quit();
    }
}
