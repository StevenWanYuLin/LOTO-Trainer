using UnityEngine;

public class ScoringSystem : MonoBehaviour
{
    public int TotalSteps { get; private set; }
    public int StepsCompleted { get; private set; }
    public int MistakeCount { get; private set; }
    public float ScorePercent => TotalSteps > 0 
        ? Mathf.Max(0f, ((float)StepsCompleted / TotalSteps * 100f) - (MistakeCount * 10f)) 
        : 0f;

    public void Initialise(int totalSteps)
    {
        TotalSteps = totalSteps;
        StepsCompleted = 0;
        MistakeCount = 0;
    }
    public void RecordStepComplete()
    {
        StepsCompleted++;
    }
    public void RecordMistake()
    {
        MistakeCount++;
    }
    public void Reset()
    {
        Initialise(0);
    }
}
