using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PanelController : MonoBehaviour 
{
    public GameDataManager GM;
    public GameObject Card;
    public GameObject SelectedCard;
    public float xPadding = 2f;
	private int num_cards = 1;
    Sprite[] raid_icons;
    Sprite[] req_icons;
    Sprite[] characterSprites;
    string[] names1 = {"Dark", "Haunted", "Eerie", "Blasted", "Forgotten", "Dragon's", "Mage's"};
	string[] names2 = {"Forest", "Keep", "Castle", "Mountain", "Desert", "Tower", "Swamp"};
	string[] names3 = {"Omisu", "Antiok", "Graey", "Felith", "Fish123", "xX_n00bKiller_Xx", "testbutts"};
	public RectTransform containerRectTransform;
	public string panelType;
    public bool selectionMode = false;
    // Use this for initialization

    void Start () 
	{
        GM = GameObject.FindGameObjectWithTag("GameMgr").GetComponent<GameStateManager>().manager;
		containerRectTransform = gameObject.GetComponent<RectTransform>();

        if (panelType == "Raid")
        {
            raid_icons = Resources.LoadAll<Sprite>("Sprites/RaidIcons");
            req_icons = Resources.LoadAll<Sprite>("Sprites/Symbols");

            List<RaidInstance> raids = GM.raids.GetAllRaids();
            for (int i = 0; i < raids.Count; i++)
            {
                AddRaid(raids[i]);
            }
        }
		if (panelType == "Character") 
		{
            req_icons = Resources.LoadAll<Sprite>("Sprites/Symbols");
            characterSprites = Resources.LoadAll<Sprite>("Sprites/Guildies");

            List<GuildMember> members = GM.members.GetGuildiesList();
            for (int i = 0; i < members.Count; i++)
            {
                AddCharacter(members[i]);
            }
        }

        
    }

    public void AddRaid(RaidInstance r)
    {
        RectTransform rectTrans;

        GameObject newCard = Instantiate(Card);
        rectTrans = newCard.GetComponent<RectTransform>();
        rectTrans.SetParent(this.gameObject.transform);

        float width = containerRectTransform.rect.width;
        float ratio = width / rectTrans.rect.width;
        float height = rectTrans.rect.height * ratio;

        Image[] Images = newCard.GetComponentsInChildren<Image>();
        Text title = newCard.transform.FindChild("Title").gameObject.GetComponent<Text>();
        Text minFame = newCard.transform.FindChild("Fame").gameObject.GetComponent<Text>();

        newCard.name = r.Name + " Card";
        title.text = r.Name;

        minFame.text = r.MinFame.ToString();

        Images[1].sprite = raid_icons[(int)r.RaidClass]; //icon
        Images[2].sprite = req_icons[(int)r.PreReqs[0]]; //Req0
        Images[3].sprite = req_icons[(int)r.PreReqs[1]]; //Req1
        Images[4].sprite = req_icons[(int)r.PreReqs[2]]; //Req2
        Images[5].sprite = req_icons[(int)r.PreReqs[3]]; //Req3
        Images[6].sprite = req_icons[(int)r.PreReqs[4]]; //Req4

        Images[2].gameObject.GetComponent<RaidReqIcon>().req = r.PreReqs[0];
        Images[3].gameObject.GetComponent<RaidReqIcon>().req = r.PreReqs[1];
        Images[4].gameObject.GetComponent<RaidReqIcon>().req = r.PreReqs[2];
        Images[5].gameObject.GetComponent<RaidReqIcon>().req = r.PreReqs[3];
        Images[6].gameObject.GetComponent<RaidReqIcon>().req = r.PreReqs[4];

        newCard.GetComponent<CardController>().setRaidInfo(r.RaidClass, r.MinFame, r.PreReqs, r.Name, r.Description);

        float x = 0;
        float y = 0 - height * num_cards;

        rectTrans.offsetMin = new Vector2(x, y);

        x = rectTrans.offsetMin.x + width;
        y = rectTrans.offsetMin.y + height;
        rectTrans.offsetMax = new Vector2(x, y);
        rectTrans.localScale = new Vector3(1, 1, 1);

        containerRectTransform.offsetMin = new Vector2(containerRectTransform.offsetMin.x, -1 * height * num_cards);

        num_cards++;
    }

    public void AddCharacter(GuildMember m)
    {
        RectTransform rectTrans;

        GameObject newCard = Instantiate(Card);
        rectTrans = newCard.GetComponent<RectTransform>();
        rectTrans.SetParent(this.gameObject.transform);

        float width = containerRectTransform.rect.width;
        float ratio = width / rectTrans.rect.width;
        float height = rectTrans.rect.height * ratio;

        Image[] Images = newCard.GetComponentsInChildren<Image>();
        Text name = newCard.transform.FindChild("Name").gameObject.GetComponent<Text>();
        Text flavor = newCard.transform.FindChild("Flavor").gameObject.GetComponent<Text>();

        newCard.name = m.Name + " Card";
        name.text = m.Name;
        if (m.Name.Length > 8)
        {
            name.fontSize = 4;
        }

        flavor.text = m.Flavor;

        Images[0].sprite = req_icons[(int)m.Ability]; //Ability
        Images[2].sprite = characterSprites[(int)m.Eyes]; //Eyes
        Images[3].sprite = characterSprites[(int)m.Skin]; //Skin
        Images[4].sprite = characterSprites[(int)m.Suit]; //Suit
        Images[5].sprite = characterSprites[(int)m.Weapon]; //Weapon

        newCard.GetComponent<CardController>().setGuildieInfo(m.Eyes, m.Skin, m.Suit, m.Weapon, m.Ability, m.Name, m.Flavor);

        float x = 0;
        float y = 0 - height * num_cards;

        rectTrans.offsetMin = new Vector2(x, y);

        x = rectTrans.offsetMin.x + width;
        y = rectTrans.offsetMin.y + height;
        rectTrans.offsetMax = new Vector2(x, y);
        rectTrans.localScale = new Vector3(1, 1, 1);

        

        containerRectTransform.offsetMin = new Vector2(containerRectTransform.offsetMin.x, -1* height  * num_cards);

        num_cards++;
    }

    public void FadeAll(GameObject exclude)
    {
        foreach (CanvasRenderer child in transform.GetComponentsInChildren<CanvasRenderer>())
        {
            if (child.gameObject.transform.parent.name != exclude.name && child.gameObject.name != "Flavor")
            {
                child.GetComponent<CanvasRenderer>().SetAlpha(0.1f);
            }
        }
    }

    public void UnFadeAll()
    {
        foreach (CanvasRenderer child in transform.GetComponentsInChildren<CanvasRenderer>())
        {
            child.GetComponent<CanvasRenderer>().SetAlpha(100f);
        }
    }
}
