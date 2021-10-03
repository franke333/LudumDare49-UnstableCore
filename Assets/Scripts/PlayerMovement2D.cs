using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    public float speed;


    float verticalAxis;
    float horizontalAxis;
    Rigidbody2D rb;
    SpriteRenderer sr;

    public float interval;
    private float timeRemaining=0;
    bool lookingUp=false;
    bool oddFrame = false;
    public Sprite[] sprites;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalAxis = Input.GetAxis("Horizontal");
        verticalAxis = Input.GetAxis("Vertical");
        if (verticalAxis > 0)
            lookingUp = true;
        else if (verticalAxis < 0)
            lookingUp = false;
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0)
        {
            timeRemaining = interval;
            oddFrame = !oddFrame;
        }
        int index = 0;
        if (oddFrame)
            index += 1;
        if (lookingUp)
            index += 2;
        sr.sprite = sprites[index];
    }

    private void FixedUpdate()
    {
        Vector3 dir = new Vector3(horizontalAxis, verticalAxis, 0);
        if(dir.magnitude!=0)
             transform.position += (dir * Time.deltaTime * speed / dir.magnitude);
    }
}
