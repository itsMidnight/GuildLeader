using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    public void Finish()
    {
        LevelStateManager m = GameObject.FindGameObjectWithTag("LevelMgr").GetComponent<LevelStateManager>();
        SceneManager.LoadScene("main");
    }

    // Update is called once per frame
    void Update () {
	
	}
}
