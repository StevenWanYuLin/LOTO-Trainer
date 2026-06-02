using System.Collections.Generic;
using UnityEngine;

public class MistakeDetector : MonoBehaviour
{
    [Header("Dependencies")]
    public ProcedureRunner procedureRunner;

    // Full mistake history — each entry is (stepIndex, stepId, timestamp)
    public List<MistakeRecord> MistakeHistory { get; private set; } = new();

    public int TotalMistakes => MistakeHistory.Count;

    private void OnEnable()
    {
        if (procedureRunner != null)
            procedureRunner.OnMistakeMade.AddListener(HandleMistakeMade);
    }

    private void OnDisable()
    {
        if (procedureRunner != null)
            procedureRunner.OnMistakeMade.RemoveListener(HandleMistakeMade);
    }

    private void HandleMistakeMade(ProcedureStep step, string reason)
    {
        var record = new MistakeRecord
        {
            stepIndex = procedureRunner.CurrentStepIndex,
            stepId = step.stepId,
            timestamp = Time.time
        };

        MistakeHistory.Add(record);
        Debug.Log($"[MistakeDetector] Mistake #{TotalMistakes} on step {step.stepId} at t={record.timestamp:F2}s");
    }

    public void Reset()
    {
        MistakeHistory.Clear();
    }
}

[System.Serializable]
public class MistakeRecord
{
    public int stepIndex;
    public string stepId;
    public float timestamp;
}