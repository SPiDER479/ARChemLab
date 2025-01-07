using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void Experiment()
    {
        SceneManager.LoadScene("Indicator");
    }
    public void Metal()
    {
        SceneManager.LoadScene("Metal");
    }
    public void Periodic()
    {
        SceneManager.LoadScene("Periodic");
    }
}