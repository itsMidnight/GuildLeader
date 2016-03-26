using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public int force;
    private bool initForce;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!initForce)
        {
            rb2d.AddForce(new Vector2(force, 0));
            initForce = true;
        }
    }
}