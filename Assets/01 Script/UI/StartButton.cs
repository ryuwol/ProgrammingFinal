using UnityEngine;
using UnityEngine.SceneManagement;
public class StartButton : MonoBehaviour
{
    public void OnClickEvent()
    {
        SceneManager.LoadScene("GameScene");
    }
}
