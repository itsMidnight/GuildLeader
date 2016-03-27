using UnityEngine;
using System.Collections;

public class CharacterMenu : MonoBehaviour {
    public GameObject b;
    public Canvas canvas;

    void Start ()
    {
        CharacterCardScript prefab = Resources.Load("Prefabs/YourPrefab") as CharacterCardScript;
        if (prefab)
        {
            //get down tonight 
        }

        var panel = GameObject.Find("CharacterCard0");
        if (panel != null)  // make sure you actually found it!
        {
            GameObject a = (GameObject)Instantiate(b);
            var test = a.GetComponent<RectTransform>();
            a.transform.SetParent(panel.transform, false);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        int i = 0; 
        if(i<1)
        {

        }
    }
}
