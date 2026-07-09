using UnityEngine;

public class StepInteractionTrigger : MonoBehaviour
{
    public ProcedureRunner runner;

    // Call this from the XR Interactable's UnityEvent (SelectEntered or Activated)
    public void OnInteracted()
    {
        if (runner == null || runner.CurrentStep == null) return;

        var step = runner.CurrentStep;
        bool tagMatches = string.IsNullOrEmpty(step.targetObjectTag) || gameObject.CompareTag(step.targetObjectTag);

        if (tagMatches) runner.AdvanceStep();
        else runner.FailStep("IncorrectTarget");
    }
}