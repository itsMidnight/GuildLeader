using UnityEngine;
using System.Collections;

public class GameDataManager
{
    public RaidManager raids;
    public MemberManager members;
    public GameDataManager()
    {

    }
}

public class GameStateManager : MonoBehaviour {

    public GameDataManager manager;
    private static GameStateManager instance = null;
    public static GameStateManager Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void StartGame()
    {
        this.manager = new GameDataManager();
    }
    
    // Use this for initialization
    void Start()
    {
		manager.raids.Load ();
		manager.members.LoadAvailable ();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
