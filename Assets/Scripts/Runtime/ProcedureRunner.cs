using UnityEngine;
using UnityEngine.Events;

public class ProcedureRunner : MonoBehaviour
{
    [SerializeField] private TrainingProcedure procedure;
    [SerializeField] private ScoringSystem _scoring;

    private int currentStepIndex = -1;
    private bool isRunning = false;
    public bool IsComplete => !isRunning && currentStepIndex >= (procedure?.steps.Count ?? 0);
    public int CurrentStepIndex => currentStepIndex;

    public ProcedureStep CurrentStep => 
        (isRunning && currentStepIndex >= 0) ? procedure.steps[currentStepIndex] : null;

    public UnityEvent<ProcedureStep> OnStepStarted;
    public UnityEvent<ProcedureStep> OnStepCompleted;
    public UnityEvent<ProcedureStep, string> OnMistakeMade;
    public UnityEvent<bool> OnProcedureEnded; // bool = passed

    public void StartProcedure()
    {
        if (isRunning) return; // guard against double-start
        if (procedure == null || procedure.steps.Count == 0) return;
        isRunning = true;
        currentStepIndex = 0;
        OnStepStarted?.Invoke(CurrentStep);
    }

    public void AdvanceStep()
    {
        if (!isRunning) return;

        OnStepCompleted?.Invoke(CurrentStep);

        currentStepIndex++;

        if (currentStepIndex >= procedure.steps.Count)
        {
            isRunning = false;
            OnProcedureEnded?.Invoke(true); // completed all steps = passed
            return;
        }

        OnStepStarted?.Invoke(CurrentStep);
    }

    public void FailStep(string reason)
    {
        if (!isRunning) return;
        OnMistakeMade?.Invoke(CurrentStep, reason);
        // Step does NOT advance — trainee must redo the current step
    }

    void OnEnable()
    {
        OnStepStarted.AddListener(step => Debug.Log($"Step started: {step.displayName}"));
        OnStepCompleted.AddListener(step => Debug.Log($"Step completed: {step.displayName}"));
        OnMistakeMade.AddListener((step, reason) => Debug.Log($"Mistake on {step.displayName}: {reason}"));
        OnProcedureEnded.AddListener(passed => Debug.Log($"Procedure ended. Passed: {passed}"));
    }
}