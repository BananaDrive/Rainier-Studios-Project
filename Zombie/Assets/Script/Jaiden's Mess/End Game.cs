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
        if (collision.CompareTag("Player") && Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            Debug.Log("Colliding");
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
        
        UI.SetActive(true);
    }

    public void Menu()
    {
        EndMenu.SetActive(false);
        GameOver.SetActive(true);
    }
}
