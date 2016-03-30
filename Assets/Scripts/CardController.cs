using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CardController : MonoBehaviour 
{
	PanelController parentPanel;
    PanelController otherPanel;

    GuildMember guildie;
    RaidInstance raid;
    List<RaidReq> raidReqs;

    Button b;
    public bool selected = false;

    // Use this for initialization
    void Start()
    {
        parentPanel = GetComponentInParent<PanelController>();
        b = GetComponent<Button>();

        if (parentPanel.panelType == "Raid")
        {
            b.onClick.AddListener(() => RaidCardClick());
            otherPanel = GameObject.FindGameObjectWithTag("CharacterPanel").GetComponent<PanelController>();
        }
        if (parentPanel.panelType == "Character")
        {
            b.onClick.AddListener(() => CharacterCardClick());
            otherPanel = GameObject.FindGameObjectWithTag("RaidPanel").GetComponent<PanelController>();
        }
    }

    void Destroy()
    {
        if (parentPanel.panelType == "Raid")
        {
            b.onClick.RemoveListener(() => RaidCardClick());
        }
        if (parentPanel.panelType == "Character")
        {
            b.onClick.RemoveListener(() => CharacterCardClick());
        }
    }

    public void setGuildieInfo(Character.CharacterEyes e,
                             Character.CharacterSkinTone sk,
                             Character.CharacterSuit su,
                             Character.CharacterWeapon wep,
                             RaidReq abil,
                             string nm,
                             string flv)
    {
        guildie = new GuildMember();
        guildie.Eyes = e;
        guildie.Skin = sk;
        guildie.Weapon = wep;
        guildie.Suit = su;
        guildie.Ability = abil;
        guildie.Name = nm;
        guildie.Flavor = flv;
    }

    public void setRaidInfo(RaidType type,
                            int min,
                            RaidReq[] preRq,
                            string nm,
                            string desc)
    {
        raid = new RaidInstance();
        raid.PreReqs = preRq;
        raid.RaidClass = type;
        raid.MinFame = min;
        raid.Name = nm;
        raid.Description = desc;

        raidReqs = new List<RaidReq>();
        for (int i = 0; i < preRq.Length; i++)
        {
            raidReqs.Add(preRq[i]);
        }
    }

    public void RaidCardClick()
    {
        
        if (selected) //---------This Raid is being de-selected---------
        {
            //set this Raid to unselected
            parentPanel.SelectedCard = null;
            selected = false;

            //Unfade all other raids
            parentPanel.UnFadeAll();

            //un"select" and Unfade all all guildies
            foreach (CardController guildmember in otherPanel.GetComponentsInChildren<CardController>())
            {
                guildmember.selected = false;
            }
            parentPanel.GM.members.ResetRaidTeam();
            otherPanel.UnFadeAll();

            //Remove Guild panel from selection mode
            otherPanel.selectionMode = false;
        }
        else //------------------This Raid is selected------------------
        {
            //Fade all other raids
            parentPanel.FadeAll(gameObject);

            //set up this raid's raidReqs checklist
            raidReqs = new List<RaidReq>();
            for (int i = 0; i < raid.PreReqs.Length; i++)
            {
                raidReqs.Add(raid.PreReqs[i]);
            }

            //If another raid was selected before..
            if (parentPanel.SelectedCard != null && parentPanel.SelectedCard.name != gameObject.name)
            {
                //..set it to not selected
                parentPanel.SelectedCard.GetComponent<CardController>().selected = false;

                //Unfade this Raid
                gameObject.GetComponent<CardController>().unFade();
            }

            //Fade this Raid's RaidReqs
            FadeAllRaidReqs();

            //set this Raid to selected
            selected = true;
            parentPanel.SelectedCard = gameObject;
            
            //un"select" and fade all guildies
            foreach (CardController guildmember in otherPanel.GetComponentsInChildren<CardController>())
            {
                guildmember.selected = false;
            }
            parentPanel.GM.members.ResetRaidTeam();
            otherPanel.FadeAll(gameObject);

            //put Guild panel into selection mode
            otherPanel.selectionMode = true;
        }
    }

    public void CharacterCardClick()
    {
        if (selected) //------------------This Character is Unselected------------------
        {
            //unselect and fade
            selected = false;
            Fade();

            //remove guilde from raid
            parentPanel.GM.members.RemoveFromRaid(guildie);

            //add back to list of needed abilities
            otherPanel.SelectedCard.GetComponent<CardController>().raidReqs.Add(guildie.Ability);

            //fade an associated raid symbol
            otherPanel.SelectedCard.GetComponent<CardController>().FadeRaidReq(guildie.Ability);

            //set selection mode to true
            parentPanel.selectionMode = true;

            //disable Launch Btn
            GameObject.FindGameObjectWithTag("LaunchBtn").GetComponent<Button>().interactable = false;

        }
        else if (parentPanel.selectionMode) //---------This Character is Selected---------
        {
            //ignore selection if mismatching raid icon
            if (otherPanel.SelectedCard.GetComponent<CardController>().raidReqs.Contains(guildie.Ability))
            { 
                //set Character Selected and unfade
                selected = true;
                unFade();

                //Add to Raiders
                parentPanel.GM.members.AddToRaid(guildie);

                //unfade associated raid symbol
                otherPanel.SelectedCard.GetComponent<CardController>().UnfadeRaidReq(guildie.Ability);

                //remove it from needed abilities list
                otherPanel.SelectedCard.GetComponent<CardController>().raidReqs.Remove(guildie.Ability);

                //set selection mode to false if raid full
                parentPanel.selectionMode = (parentPanel.GM.members.GetRaidTeam().Count < 5);

                //check raid ready to activate Launch Btn
                if (parentPanel.GM.members.IsRaidReady())
                {
                    Debug.Log("Raid Ready");
                    GameObject.FindGameObjectWithTag("LaunchBtn").GetComponent<Button>().interactable = true;
                }
            }
        }
        Debug.Log(gameObject + " selected = " + selected);
    }

    public void Fade()
    {
        foreach (CanvasRenderer childElement in GetComponentsInChildren<CanvasRenderer>())
        {
            if (childElement.gameObject.name != "Flavor")
            {
                childElement.GetComponent<CanvasRenderer>().SetAlpha(0.1f);
            }
        }
    }

    public void unFade()
    {
        foreach (CanvasRenderer childElement in GetComponentsInChildren<CanvasRenderer>())
        {
            childElement.SetAlpha(100f);
        }
    }

    public void FadeAllRaidReqs()
    {
        foreach (CanvasRenderer childElement in GetComponentsInChildren<CanvasRenderer>())
        {
            if (childElement.gameObject.name.Substring(0,3) == "Req")
            {
                childElement.GetComponent<CanvasRenderer>().SetAlpha(0.1f);
            }
        }
    }

    public void UnfadeRaidReq(RaidReq req)
    {
        CanvasRenderer CR;
        foreach (RaidReqIcon childElement in GetComponentsInChildren<RaidReqIcon>())
        {
            CR = childElement.gameObject.GetComponent<CanvasRenderer>();

            if (childElement.gameObject.name.Substring(0, 3).Contains("Req") &&
                CR.GetAlpha() - 0.1f < 0.001f &&
                childElement.req == req)
            {
                CR.SetAlpha(1f);
                break;
            }
        }
    }

    public void FadeRaidReq(RaidReq req)
    {
        CanvasRenderer CR;
        RaidReqIcon childElement;
        float alpha = 0f;
        string nm = "";
        RaidReqIcon[] icons = GetComponentsInChildren<RaidReqIcon>();
        for(int i = icons.Length-1; i>=0; i--)
        {
            childElement = icons[i];
            CR = childElement.gameObject.GetComponent<CanvasRenderer>();
            alpha = CR.GetAlpha();
            nm = childElement.gameObject.name.Substring(0, 3);
            if (childElement.gameObject.name.Substring(0, 3).Contains("Req") &&
                Mathf.Abs(CR.GetAlpha() - 1f) < 0.001f &&
                childElement.req == req)
            {
                CR.SetAlpha(0.1f);
                break;
            }
        }
    }

    public void LaunchRaid()
    {
        if(parentPanel.panelType == "Raid")
        {
            RaidData inputs = new RaidData();
            inputs.isWin = false;

            LevelStateManager LvlMgr = GameObject.FindGameObjectWithTag("LevelMgr").GetComponent<LevelStateManager>();
            LvlMgr.StartLevel(inputs);
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        }
    }
}
