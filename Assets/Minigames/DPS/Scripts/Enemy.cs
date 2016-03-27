using UnityEngine;
using System.Collections;
using System.Linq;
public class Enemy : MonoBehaviour
{
    public int Force;
    public bool First;

    // Use this for initialization
    void Start()
    {
        if (First)
        {
            Go();
        }
    }

    public void Go()
    {
        var character = GetComponentInChildren<Character>();
        character.Init();
		character.InitializeLookAndFeel(character.GetRandomSkinTone(), character.GetRandomSuit(), character.GetRandomWeapon(), character.GetRandomEyes());
        character.MoveAcrossScreen(Force);
    }
}