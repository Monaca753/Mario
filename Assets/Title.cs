using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene("Map");
    }
}
