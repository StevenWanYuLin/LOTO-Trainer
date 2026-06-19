using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [Header("References")]
    public ProcedureRunner procedureRunner;
    public ScoringSystem scoringSystem;
    public MistakeDetector mistakeDetector;

    [Header("UI Elements")]
    public TextMeshProUGUI instructionText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI mistakeText;

    void OnEnable()
    {
        procedureRunner.OnStepStarted.AddListener(HandleStepStarted);
        procedureRunner.OnStepCompleted.AddListener(HandleStepCompleted);
        procedureRunner.OnMistakeMade.AddListener(HandleMistakeMade);
        procedureRunner.OnProcedureEnded.AddListener(HandleProcedureEnded);
    }

    void OnDisable()
    {
        procedureRunner.OnStepStarted.RemoveListener(HandleStepStarted);
        procedureRunner.OnStepCompleted.RemoveListener(HandleStepCompleted);
        procedureRunner.OnMistakeMade.RemoveListener(HandleMistakeMade);
        procedureRunner.OnProcedureEnded.RemoveListener(HandleProcedureEnded);
    }

    void HandleStepStarted(ProcedureStep step)
    {
        instructionText.text = step.instruction; // adjust field name if different
        RefreshScoreAndMistakes();
    }

    void HandleStepCompleted(ProcedureStep step)
    {
        RefreshScoreAndMistakes();
    }

    void HandleMistakeMade(ProcedureStep step, string reason)
    {
        RefreshScoreAndMistakes();
    }

    void HandleProcedureEnded(bool success)
    {
        instructionText.text = success ? "Procedure Complete" : "Procedure Failed";
        RefreshScoreAndMistakes();
    }

    void RefreshScoreAndMistakes()
    {
        scoreText.text = $"Score: {scoringSystem.ScorePercent:F0}%";
        mistakeText.text = $"Mistakes: {mistakeDetector.TotalMistakes}";
    }
}