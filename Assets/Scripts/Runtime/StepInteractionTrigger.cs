using UnityEngine;

public class StepInteractionTrigger : MonoBehaviour
{
    public ProcedureRunner runner;

    // Call this from the XR Interactable's UnityEvent (SelectEntered or Activated)
    public void OnInteracted()
    {
        Debug.Log($"[StepInteractionTrigger] OnInteracted fired on {gameObject.name} (tag: {gameObject.tag}), runner={runner}, currentStep={runner?.CurrentStep?.targetObjectTag}");

        if (runner == null || runner.CurrentStep == null) return;

        var step = runner.CurrentStep;
        bool tagMatches = string.IsNullOrEmpty(step.targetObjectTag) || gameObject.CompareTag(step.targetObjectTag);
        Debug.Log($"[StepInteractionTrigger] tagMatches={tagMatches}");

        if (tagMatches) runner.AdvanceStep();
        else runner.FailStep("IncorrectTarget");
    }
}