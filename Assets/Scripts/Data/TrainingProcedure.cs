using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewTrainingProcedure", menuName = "LOTO/Training Procedure")]
public class TrainingProcedure : ScriptableObject
{
    [Header("Procedure Identity")]
    public string procedureId;          // e.g. "LOTO_ELECTRICAL_BASIC"
    public string procedureName;        // "Electrical Lockout/Tagout — Basic"
    public string regulatoryReference;  // "OSHA 1910.147"

    [Header("Steps")]
    public List<ProcedureStep> steps;   // ordered — index 0 = step 1

    [Header("Pass Criteria")]
    [Range(0, 100)]
    public int passingScorePercent;     // e.g. 80
    public int maxMistakesAllowed;      // -1 = unlimited (training mode)

    public int GetTotalPossiblePoints()
    {
        int total = 0;
        foreach (var step in steps)
            total += step.pointValue;
        return total;
    }
}