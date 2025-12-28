using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraSmoothMove : MonoBehaviour
{

    public float speedDiv = 3.0f;

    // the Y positions it aims for
    public float startY = 0f;
    public float endY = 0f;
    private Vector3 startPos;
    private Vector3 endPos;

    // tracks the time of descent
    private float timePass;
    private float drag;
    private float partMove;
    // is it moving or disabled (for avoiding unnecesary updating)
    private bool moving;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void BeginMove()
    {
        GetComponent<Renderer>().enabled = true; // makes visible
        moving = true;
        timePass = 0;

        startPos = new Vector3(transform.position.x, startY, transform.position.z);
        endPos = new Vector3(transform.position.x, endY, transform.position.z);

        transform.position = startPos;
    }

    // Update is called once per frame - if moving, smoothsteps to get a psuedodrag timing, then applies it to lerp between start and end for smooth descent
    void Update()
    {
        if (moving)
        {

            timePass += Time.deltaTime / speedDiv;

            drag = Mathf.SmoothStep(0f, 1f, timePass);

            transform.position = Vector3.Lerp(startPos, endPos, drag);

            if (timePass >= 1) // overshoot protection
            {
                transform.position = endPos;
                moving = false;
            }
        }
    }

    // CHECK
    public void ResetPseudo() // set the Y value back to starting height
    {
        GetComponent<Renderer>().enabled = false; // makes hidden
        moving = false;
        //gameGrid.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        transform.position = startPos; // resets to start
    }
}
