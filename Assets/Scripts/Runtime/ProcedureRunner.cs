using UnityEngine;
using UnityEngine.Events;

public class ProcedureRunner : MonoBehaviour
{
    [Header("Systems")]
    public ScoringSystem scoringSystem;
    public MistakeDetector mistakeDetector;
    public SessionLogger logger;

    [SerializeField] private TrainingProcedure procedure;

    private int currentStepIndex = -1;
    private bool isRunning = false;
    public bool IsComplete => !isRunning && currentStepIndex >= (procedure?.steps.Count ?? 0);
    public int CurrentStepIndex => currentStepIndex;

    public ProcedureStep CurrentStep =>
        (isRunning && currentStepIndex >= 0) ? procedure.steps[currentStepIndex] : null;

    public UnityEvent<ProcedureStep> OnStepStarted;
    public UnityEvent<ProcedureStep> OnStepCompleted;
    public UnityEvent<ProcedureStep, string> OnMistakeMade;
    public UnityEvent<bool> OnProcedureEnded;

    public void StartProcedure()
    {
        if (isRunning) return;
        if (procedure == null || procedure.steps.Count == 0) return;

        logger?.Initialise();
        scoringSystem?.Initialise(procedure.steps.Count); // add this line
        isRunning = true;
        currentStepIndex = 0;
        OnStepStarted?.Invoke(CurrentStep);
    }

    public void AdvanceStep()
    {
        if (!isRunning) return;

        OnStepCompleted?.Invoke(CurrentStep);
        logger?.RecordStepComplete(CurrentStep);
        scoringSystem?.RecordStepComplete();
        currentStepIndex++;

        if (currentStepIndex >= procedure.steps.Count)
        {
            isRunning = false;
            OnProcedureEnded?.Invoke(true);
            logger?.GetSessionSummary();
            Debug.Log(logger?.GetSessionSummary()); // ← add this
            return;
        }

        OnStepStarted?.Invoke(CurrentStep);

    }

    public void FailStep(string reason)
    {
        if (!isRunning) return;
        OnMistakeMade?.Invoke(CurrentStep, reason);
        logger?.RecordMistake(CurrentStep, reason);
        scoringSystem?.RecordMistake();
    }

    void OnEnable()
    {
        OnStepStarted.AddListener(step => Debug.Log($"Step started: {step.displayName}"));
        OnStepCompleted.AddListener(step => Debug.Log($"Step completed: {step.displayName}"));
        OnMistakeMade.AddListener((step, reason) => Debug.Log($"Mistake on {step.displayName}: {reason}"));
        OnProcedureEnded.AddListener(passed => Debug.Log($"Procedure ended. Passed: {passed}"));
    }
}