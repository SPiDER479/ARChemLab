using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    public GameObject menu, credits, topics;
    public void LoadTopics()
    {
        menu.SetActive(false);
        topics.SetActive(true);
    }
    public void LoadCredits()
    {
        menu.SetActive(false);
        credits.SetActive(true);
    }
    public void LoadMenu()
    {
        topics.SetActive(false);
        credits.SetActive(false);
        menu.SetActive(true);
    }
    public void LoadIndicator()
    {
        SceneManager.LoadScene("Indicator");
    }
}