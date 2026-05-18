using UnityEngine;
using System.Collections.Generic;

public enum ActionType
{
    Notify,           // inform affected personnel
    Shutdown,         // power down the equipment
    Isolate,          // operate energy isolation device
    Lockout,          // apply lockout device + personal lock
    ReleaseEnergy,    // release / restrain stored energy
    Verify            // verify zero-energy state
}

public enum VerificationType
{
    TagPresent,       // a lockout tag exists on the target
    LockPresent,      // a physical lock is on the isolator
    SwitchPosition,   // switch/breaker is in correct position
    MeterReading,     // voltage meter reads zero
    ButtonPress       // press-to-test confirms no power
}

public enum MistakeTrigger
{
    WrongOrder,       // step attempted before prerequisite
    WrongTool,        // used incorrect tool/device
    SkippedStep,      // moved past without completing
    IncorrectTarget,  // interacted with wrong object
    MissingVerify     // skipped verification sub-step
}

[CreateAssetMenu(fileName = "NewProcedureStep", menuName = "LOTO/Procedure Step")]
public class ProcedureStep : ScriptableObject
{
    [Header("Identity")]
    public string stepId;                        // e.g. "LOTO_03_ISOLATE"
    public int stepOrder;                        // 1–6
    public string displayName;                   // shown in trainer UI
    [TextArea(2, 5)] public string instruction;  // shown to trainee in headset

    [Header("Required Action")]
    public ActionType requiredAction;
    public string targetObjectTag;               // tag on the GameObject to interact with
    public string requiredToolTag;               // leave empty if no specific tool needed

    [Header("Verification")]
    public bool requiresVerification;
    public VerificationType verificationType;
    public string verificationTargetTag;

    [Header("Scoring")]
    public int pointValue;                       // base points for completing correctly
    public float timeLimitSeconds;               // 0 = no limit
    public List<MistakeTrigger> penalisedMistakes;

    [Header("Feedback")]
    [TextArea(1, 3)] public string successFeedback;
    [TextArea(1, 3)] public string failureFeedback;
}