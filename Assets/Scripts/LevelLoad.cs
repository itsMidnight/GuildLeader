using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelLoad : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void onClick()
    {
        LevelStateManager m = GameObject.FindGameObjectWithTag("LevelMgr").GetComponent<LevelStateManager>();
        SceneManager.LoadScene("crosshairs");
    }
}
