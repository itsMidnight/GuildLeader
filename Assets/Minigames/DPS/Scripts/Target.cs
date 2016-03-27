using UnityEngine;
using System.Collections.Generic;

public class Target : MonoBehaviour
{
    private Dictionary<int, SpriteRenderer> dictSpRend;
    private Dictionary<int, Rigidbody2D> dictRb2d;


    // Use this for initialization
    void Start()
    {
        dictSpRend = new Dictionary<int, SpriteRenderer>();
        dictRb2d = new Dictionary<int, Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        var id = coll.GetInstanceID();
        var gObj = coll.gameObject;
        dictRb2d.Add(id, gObj.GetComponent<Rigidbody2D>());
        dictSpRend.Add(id, gObj.GetComponent<SpriteRenderer>());
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        var id = coll.GetInstanceID();
        if (dictRb2d.ContainsKey(id))
        {
            if (Input.GetMouseButtonDown(0))
            {
                dictSpRend[id].color = Color.red;
                dictRb2d[id].gravityScale = 6.5F;
                dictRb2d[id].AddForce(new Vector2(0, 100));
                dictRb2d.Remove(id);
                dictSpRend.Remove(id);
            }
        }
    }
    void OnTriggerExit2D(Collider2D coll)
    {
        var id = coll.GetInstanceID();
        dictSpRend.Remove(id);
        dictRb2d.Remove(id);
    }
}