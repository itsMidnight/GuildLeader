using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Respawn : MonoBehaviour
{
    public GameObject enemy;
    private Dictionary<int, GameObject> dict;
    private object Lock;

    void Start()
    {
        dict = new Dictionary<int, GameObject>();
        Lock = new object();
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        var nme = coll.GetComponent<Enemy>();
        lock (nme)
        {
            coll.enabled = false;
            Instantiate<GameObject>(enemy);
            Destroy(coll.gameObject);
        }

    }
}
