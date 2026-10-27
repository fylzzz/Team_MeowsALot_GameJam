using UnityEngine;

public class ScoreCalc : MonoBehaviour
{
    public const int PointsPerMonster = 1000;
    public const int MaxTimePoints = 10000;
    public const float MaxTimeSeconds = 180; //in seconds

    public static int TimePoints(float seconds)
    {
        float t = Mathf.Clamp01(1f - seconds / MaxTimeSeconds);
        return Mathf.RoundToInt(t * MaxTimePoints);
    }

    public static int MonsterPoints(int monstersCollected)
    {
        return monstersCollected * PointsPerMonster;
    }

    public static int Total(float seconds, int monstersCollected)
    {
        return TimePoints(seconds) + MonsterPoints(monstersCollected);
    }

}
    