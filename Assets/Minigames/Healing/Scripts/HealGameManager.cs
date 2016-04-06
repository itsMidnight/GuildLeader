using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class HealGameManager : MonoBehaviour
{
    protected List<DudeBehavior> dudes;
    public GameObject dude;

	// Use this for initialization
	void Start () {
        // Read and process init info for this game instance.

        // Create list of characters for battle.        
        // Create list of mobs for battle.
        //  -- Create look and feel, P/T, etc.

        // Guildies start on screen, mobs walk in from off-screen.
        // As soon as battle finishes, if more mobs are coming, another mob enters
        // if no more mobs, remaining guildies help weakest guildie.

        // If guildies wipe, return shitty fame.
        // If guildies win, return fame based on remaining guildy count
        Debug.Log("HealGameManager::Start");        

        GameObject dudeClone = (GameObject)Instantiate(dude, new Vector3(transform.position.x / 2, transform.position.y / 2), Quaternion.identity);//, transform.position, Quaternion.identity);
        var dudeScript = dudeClone.GetComponent<DudeBehavior>();
        dudeScript.CharacterDied += Dude_CharacterDied;
    }

    private void Dude_CharacterDied(DudeBehavior dude)
    {
        // Dude died, unsub from this event.
        Debug.Log("Dude " + dude.CharacterId + " died.");
        dude.CharacterDied -= Dude_CharacterDied;
    }

    // Update is called once per frame
    void Update () {
	
	}
}
