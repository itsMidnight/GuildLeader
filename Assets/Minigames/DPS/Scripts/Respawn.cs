using UnityEngine;
using System.Collections.Generic;

public class Respawn : MonoBehaviour
{
    public GameObject enemy;
    private Dictionary<int, GameObject> dict;
    public int Level;

    void Start()
    {
        dict = new Dictionary<int, GameObject>();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        lock(coll.gameObject)
        {
            var gObj = Instantiate<GameObject>(enemy);
            var nme = gObj.GetComponent<Enemy>();
            nme.Run(Level);
            Destroy(coll.gameObject);
        }
    }
}