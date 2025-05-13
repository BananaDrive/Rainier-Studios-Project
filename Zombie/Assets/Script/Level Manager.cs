using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int levelToSwitch;
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && (Input.GetKey(KeyCode.F) || Input.GetKeyDown(KeyCode.Joystick1Button2)))
        {
            SceneManager.LoadScene(levelToSwitch);
        }
    }
}
