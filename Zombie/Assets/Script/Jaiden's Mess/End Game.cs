using NUnit.Framework.Internal;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{

    public GameObject EndMenu;
    public GameObject GameOver;
    public GameObject Pause;
    public GameObject UI;
    public GameObject continueButton;

    public TMP_Text text;

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && (Input.GetKeyDown(KeyCode.Joystick1Button2) || Input.GetKeyDown(KeyCode.F)))
        {
            text.SetText("You Win!");
            EndMenu.SetActive(true);
            GameOver.SetActive(false);
            Pause.SetActive(false);
            UI.SetActive(false);
            Time.timeScale = 0;
            GameManager.Instance.eventSystemAcess.Select(continueButton);
            GameManager.Instance.gameOver = true;
        }
    }

    public void Continue()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);


        EndMenu.SetActive(false);
        text.SetText("Game Over");
        
        UI.SetActive(true);
    }

    public void Menu()
    {
        EndMenu.SetActive(false);
        GameManager.Instance.GameOver();
    }
}
