using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginStart : MonoBehaviour
{
    public EventSystemAcess eventSystem;
    public GameObject gameObject2;
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Update()
    {
        eventSystem.Select(gameObject2);
    }


}
