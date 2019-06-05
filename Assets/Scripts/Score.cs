using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private static int score;
    private static int highScore;
    private static int charges;
    private static int kills;
    private static Vector2 boatPos;
    private static bool[] subSlots = new bool[6]
    {
        true, true, true, true, true, true
    };

    //[SerializeField]
    public Text scoreField;

    //[SerializeField]
    public Text chargesField;

    //[SerializeField]
    public Text KillsField;

    public static int GetHighScore()
    {
        return highScore;
    }

    public static void SetBoatPos(Vector2 _boatPos)
    {
        boatPos = _boatPos;
    }

    public static Vector2 GetBoatPos()
    {
        return boatPos;
    }

    public static void AddScore(int depth)
    {
        score += 70 - ((depth + 5) * 10);
        kills++;
        if (score > highScore)  // Check if there is a new high score
            highScore = score;  // every time the player get some points
    }

    public static void SetCharges(int no)
    {
        charges += no;
    }

    public static int MyCharges()
    {
        return charges;
    }

    private void Update()
    {
        scoreField.text = "Score: " + score.ToString();
        chargesField.text = "Charges: " + charges.ToString();
        KillsField.text = "Kills: " + kills.ToString();
    }

    public static bool ReadSlot(int depth)
    {
        return subSlots[depth+4];  // +4 because calling function uses sub depth, and that starts with -4
    }

    public static void SetSlot(int depth, bool value)
    {
        subSlots[depth+4] = value;
    }

    public static void Reset()
    {
        // Reset slots
        for(int i=0; i<6; i++)
        {
            subSlots[i] = true;
        }
        // Reset charges
        charges = 4;
        // Reset kills
        kills = 0;
        // Reset score
        score = 0;
    }
}
