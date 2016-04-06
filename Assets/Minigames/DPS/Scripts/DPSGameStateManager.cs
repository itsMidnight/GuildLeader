using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DPSGameStateManager : MonoBehaviour
{
    public float EnemiesSeen;
    public float Hits;
    public float TimeLeft;
    public int Level;
    public int EnemiesAllowed;

    private int percentage;


    private Text displayText;
    private Button button;
    private bool runOnce = true;
    private Enemy enemy;

    public enum Toughness
    {
        Easy = 1,
        Normal = 2,
        Hard = 3,
    }
    
    public enum Score
    {
        Poor = 0,
        Average = 1,
        Excellent = 2, 
    }

    void Start()
    {
        displayText = GetComponentInChildren<Text>();
        button = GetComponentInChildren<Button>();
        enemy = FindObjectOfType<Enemy>();
        GetComponentsInChildren<Respawn>().ToList().ForEach(e => e.Level = this.Level);

        displayText.enabled = true;
        button.gameObject.SetActive(false);
    }

    void Update()
    {
        if (TimeLeft > 0)
        {
            TimeLeft -= Time.deltaTime;
            displayText.lineSpacing = 1;
            displayText.text = "Difficulty: " + Enum.GetValues(typeof(Toughness)).GetValue(Level) + Environment.NewLine + "Game starts in: " + Mathf.Round(TimeLeft);
        }
        else if (runOnce)
        {
            displayText.enabled = false;
            runOnce = false;
            enemy.Run(Level);
        }
        else
        {
            try
            {
                percentage = Mathf.RoundToInt(Hits/(EnemiesSeen - 1)*100);
            }
            catch (DivideByZeroException)
            {
                //Ignore divide by zero exceptions
            }

            if (EnemiesSeen > EnemiesAllowed)
            {
                FindObjectsOfType<Enemy>().ToList().ForEach(e => Destroy(e.gameObject));
                displayText.text = "Game Over!" + Environment.NewLine +
                                   "Your score: " + percentage + "%" + Environment.NewLine +
                                   "Your rating: " + CalculateRating(percentage).ToString();
                displayText.enabled = true;
                button.gameObject.SetActive(true);
            }
        }
    }

    private Score CalculateRating(int percentage)
    {
        if (percentage < 75)
        {
            return Score.Poor;
        }
        else if (percentage > 85)
        {
            return Score.Excellent;
        }
        else
        {
            return Score.Average;
        }
    }
}