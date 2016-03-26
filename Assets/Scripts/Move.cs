using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour
{
    public int force;
    public Color color;

    private SpriteRenderer spRend;
    private bool inRange;
    private Rigidbody2D rb2d;
    private bool initForce;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spRend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!initForce)
        {
            rb2d.AddForce(new Vector2(force, 0));
            initForce = true;
        }

        if (inRange && Input.GetKeyDown(KeyCode.Space))
        {

            spRend.color = Color.green;
            //spRend.enabled = false;
        }
    }

    void OnTriggerEnter2D()
    {
        inRange = true;
    }

    void OnTriggerExit2D()
    {
        inRange = false;
    }

}