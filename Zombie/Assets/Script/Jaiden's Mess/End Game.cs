using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{

    public GameObject EndMenu;
    public GameObject GameOver;
    public GameObject Pause;
    public GameObject UI;

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Input.GetKey(KeyCode.F))
        {
            EndMenu.SetActive(true);
            GameOver.SetActive(false);
            Pause.SetActive(false);
            UI.SetActive(false);
        }
    }

    public void Continue()
    {
        SceneManager.LoadScene(1);


        EndMenu.SetActive(false);
        GameOver.SetActive(true);
        Pause.SetActive(true);
        UI.SetActive(true);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);

        EndMenu.SetActive(false);
        GameOver.SetActive(true);
        Pause.SetActive(true);
        UI.SetActive(true);
    }
}
