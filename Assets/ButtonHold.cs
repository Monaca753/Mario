using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHold : MonoBehaviour 
{
    private bool rightButtonDownFlag;
    private bool leftButtonDownFlag;

    private bool onGround;

    private static GameObject character;
    private static GameObject camera;

    private static Transform transform;

    private static float halfCanvasWidth;

    private static float cameraX;

    private static float speed = 100f;

    private float updatePos = 0f;

    public void RightButtonDown() 
    {
    rightButtonDownFlag = true;
    }

    public void RightButtonUp()
    {
    rightButtonDownFlag = false;
    }

    public void LeftButtonDown() 
    {
    leftButtonDownFlag = true;
    }

    public void LeftButtonUp() 
    {
    leftButtonDownFlag = false;
    }

    private void Right(bool flag, float step, GameObject character)
    {
        if (flag)
        {
            transform.Translate(step, 0f, 0f);
            float x = transform.localPosition.x;

            if (x > updatePos)
            {
                Vector3 cameraPos = camera.transform.position;
                camera.transform.position = new Vector3(cameraX + x, cameraPos.y, cameraPos.z);
                updatePos = x;
            }
        }
    }

    private void Left(bool flag, float step, GameObject character)
    {
        if (flag && transform.position.x >= camera.transform.position.x - halfCanvasWidth)
        {
            transform.Translate(-step, 0f, 0f);
        }
    }

    public void Jump()
    {
        if (onGround)
        {
            transform.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, 500f, 0f);
        }
    }

    public void Start()
    {
        character = GameObject.Find("Character");
        camera = GameObject.Find("Main Camera");

        transform = character.transform;

        halfCanvasWidth = GameObject.Find("Canvas").GetComponent<RectTransform>().rect.width / 2;

        cameraX = camera.transform.position.x;
    }

    public void OnCollisionEnter2D(Collision2D collider)
    {
        onGround = true;
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        onGround = false;
    }

    public void Update()
    {
        if (transform.localPosition.y < -400)
        {
            SceneManager.LoadScene("Title");
        }
        float step = speed * Time.deltaTime;
        Right(rightButtonDownFlag, step, character);
        Left(leftButtonDownFlag, step, character);
    }
} 