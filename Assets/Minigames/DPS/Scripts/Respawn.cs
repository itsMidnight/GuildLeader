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
        var item = coll.GetComponent<Character>();
        lock (item)
        {
            coll.enabled = false;   
            var gObj = Instantiate<GameObject>(enemy);
            var nme = gObj.GetComponent<Enemy>();
            nme.Go();
            //nme.Go();
            //character.Init();
            //character.InitializeLookAndFeel(Character.CharacterSkinTone.Medium, Character.CharacterSuit.K);
            Destroy(coll.gameObject);
        }

    }
}
