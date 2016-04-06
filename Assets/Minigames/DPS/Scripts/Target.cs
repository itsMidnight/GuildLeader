using UnityEngine;
using System.Collections.Generic;

public class Target : MonoBehaviour
{
    private Dictionary<int, Character> characterDict;
    private bool wasHit;
    private DPSGameStateManager gameStateManager;

    // Use this for initialization
    void Start()
    {
        characterDict = new Dictionary<int, Character>();
        gameStateManager = GetComponentInParent<DPSGameStateManager>();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        var id = coll.GetInstanceID();
        var gObj = coll.gameObject;
        characterDict.Add(id, gObj.GetComponent<Character>());
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        var id = coll.GetInstanceID();
        if (characterDict.ContainsKey(id))
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                characterDict[id].RegisterDirectHit();
                wasHit = true;
            }
        }
    }
    void OnTriggerExit2D(Collider2D coll)
    {
        var id = coll.GetInstanceID();
        if (characterDict.ContainsKey(id))
        {
            characterDict.Remove(id);
            if (wasHit)
            {
                gameStateManager.Hits++;
                wasHit = false;
            }
        }

    }
}