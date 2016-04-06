using UnityEngine;
using System.Collections;
using System.Linq;
public class Enemy : MonoBehaviour
{
    public int Force;
    private DPSGameStateManager gsManager;

    // Use this for initialization
    void Start()
    {
        gsManager = FindObjectOfType<DPSGameStateManager>();
        gsManager.EnemiesSeen++;
    }

    public void Run(float level)
    {
        var character = GetComponentInChildren<Character>();
        character.Init();

        var transform = character.GetComponent<Transform>();
        transform.position = new Vector3(-15, 0.2f, 0);

        character.InitializeLookAndFeel(character.GetRandomSkinTone(), character.GetRandomSuit(), character.GetRandomWeapon(), character.GetRandomEyes());
        character.MoveAcrossScreen(Force * Mathf.Pow(1.75f, level));
    }
}