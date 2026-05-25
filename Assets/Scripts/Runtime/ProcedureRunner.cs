using UnityEngine;
using UnityEngine.Events;

public class ProcedureRunner : MonoBehaviour
{
    [SerializeField] private TrainingProcedure procedure;

    private int currentStepIndex = -1;
    private bool isRunning = false;

    public ProcedureStep CurrentStep => 
        (isRunning && currentStepIndex >= 0) ? procedure.steps[currentStepIndex] : null;

    public UnityEvent<ProcedureStep> OnStepStarted;
    public UnityEvent<ProcedureStep> OnStepCompleted;
    public UnityEvent<ProcedureStep, string> OnMistakeMade;
    public UnityEvent<bool> OnProcedureEnded; // bool = passed

    public void StartProcedure()
    {
        // TODO Tuesday
    }

    public void AdvanceStep()
    {
        // TODO Tuesday
    }

    public void FailStep(string reason)
    {
        // TODO Tuesday
    }
}