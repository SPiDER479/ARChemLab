using UnityEngine;
using UnityEngine.SceneManagement;
public class ReturnMenu : MonoBehaviour
{
    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}