using UnityEngine;
using System.Collections;

public class GameDataManager
{
    public RaidManager raids;
    public MemberManager members;
    public GameDataManager()
    {
        raids = new RaidManager();
        members = new MemberManager();
    }

    public void LoadImmatable()
    {
        raids.Load();
        members.LoadAvailable();
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

        this.manager = new GameDataManager();
        this.manager.LoadImmatable();
    }
    
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
