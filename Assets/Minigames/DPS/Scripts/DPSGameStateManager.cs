using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DPSGameStateManager : MonoBehaviour
{
    public int EnemiesSeen;
    public int Score;

    private Text text;
    private Button butt;

    void Start()
    {
        text = GetComponentInChildren<Text>();
        butt = GetComponentInChildren<Button>();
        text.enabled = false;
        butt.gameObject.SetActive(false);
    }

    void Update()
    {
        if(EnemiesSeen > 10)
        {
            text.enabled = true;
            butt.gameObject.SetActive(true);
        }
    }
}