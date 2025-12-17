using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

public class PseudoDescentControl : MonoBehaviour
{

    public float speedDiv = 2.0f;

    // the Y positions it aims for
    public float startY = 12.5f;
    public float endY = 2.5f;
    private Vector3 startPos;
    private Vector3 endPos;

    // tracks the time of descent
    private float timePass;
    private float drag;
    private float partMove;
    // is it descending or disabled (for avoiding unnecesary updating)
    private bool descending;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void BeginDescent()
    {
        GetComponent<Renderer>().enabled = true; // makes visible
        descending = true;
        timePass = 0;

        startPos = new Vector3(transform.position.x, startY, transform.position.z);
        endPos = new Vector3(transform.position.x, endY, transform.position.z);

        transform.position = startPos;
    }

    // Update is called once per frame - if descending, smoothsteps to get a psuedodrag timing, then applies it to lerp between start and end for smooth descent
    void Update()
    {
        if (descending)
        {

            timePass += Time.deltaTime / speedDiv;

            drag = Mathf.SmoothStep(0f, 1f, timePass);

            //partMove = (transform.position.y - endPos.y) + 0.1f; 

            transform.position = Vector3.Lerp(startPos, endPos, drag);

            //transform.position = Vector3.MoveTowards(transform.position, endPos, 0.001f); 

            if (timePass >= 1) // overshoot protection
            {
                transform.position = endPos;
                descending = false;
            }
        }
    }

    public void ResetPseudo() // set the Y value back to starting height
    {
        GetComponent<Renderer>().enabled = false; // makes hidden
        descending = false;
        //gameGrid.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        transform.position = startPos; // resets to start
    }
}
