using UnityEngine;

public class Scorer : MonoBehaviour
{
    [SerializeField] private int score = 0;
    public int GetScore ()
    {
        return score;
    }

    public void AddScore(int amount)
    {
        score += amount;
    }
}
