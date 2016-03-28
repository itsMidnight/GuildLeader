using UnityEngine;
using System.Collections.Generic;

public class Respawn : MonoBehaviour
{
    public GameObject enemy;
    private Dictionary<int, GameObject> dict;
    private DPSGameStateManager stateManager;

    void Start()
    {
        dict = new Dictionary<int, GameObject>();
        stateManager = GetComponentInParent<DPSGameStateManager>();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        lock(coll.gameObject)
        {
            var gObj = Instantiate<GameObject>(enemy);
            var nme = gObj.GetComponent<Enemy>();
            nme.Go();
            Destroy(coll.gameObject);
        }
    }
}