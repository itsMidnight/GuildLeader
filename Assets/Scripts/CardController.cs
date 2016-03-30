using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CardController : MonoBehaviour 
{
	PanelController parentPanel;
    Button b;
    bool selected = false;

    // Use this for initialization
    void Start()
    {
        parentPanel = GetComponentInParent<PanelController>();
        b = GetComponent<Button>();

        if (parentPanel.panelType == "Raid")
        {
            b.onClick.AddListener(() => RaidCardClick());
        }
        if (parentPanel.panelType == "Character")
        {
            b.onClick.AddListener(() => CharacterCardClick());
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

    public void RaidCardClick()
    {
        if (selected)
        {
            selected = false;
        }
        else
        {
            selected = true;
            parentPanel.FadeAll(gameObject);
        }
        Debug.Log(gameObject.name + " selected = " + selected.ToString());
    }

    public void CharacterCardClick()
    {
        if (selected)
        {
            selected = false;
        }
        else
        {
            selected = true;
        }
        Debug.Log(gameObject.name + " selected = " + selected.ToString());
    }
}
