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
    
    void OnTriggerEnter2D(Collider2D coll)
    {
        var id = coll.GetInstanceID();
        var gObj = coll.gameObject;
        dictRb2d.Add(id, gObj.GetComponent<Rigidbody2D>());
        dictSpRend.Add(id, gObj.GetComponentsInChildren<SpriteRenderer>()[0]);
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
                dictRb2d[id].AddForce(new Vector2(0, 100 * Random.Range(0.8f,2.2f)));
                dictRb2d[id].AddTorque(10 * Random.Range(2, 3), ForceMode2D.Force);
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