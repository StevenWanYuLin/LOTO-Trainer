# LOTO VR Trainer — Daily Log

*Append 1–2 lines per working day. Date format: YYYY-MM-DD.*

*This file is interview gold. "I can show you my commit history and a daily log going back to May 2026."*

---

## Format

```
### YYYY-MM-DD
- What I did
- What broke / what I learned
- Blocker, if any
```

---

## 2026-05

### 2026-05-13
- Project plan finalized with Claude. Headset arrives end of June (friend's Quest 3) — revised plan to do architecture phase headset-less, VR layer end-June onwards.
- Created PLAN.md, THIS-WEEK.md, LOG.md as living docs.
- Next: start W1 tasks — Unity install.

### 2026-05-14
- Unity 6 + XRI installed, OpenXR configured, Meta Quest build target set.
- XR scene running clean in editor, no errors.
- GitHub repo live, first commit pushed.
- Next: read OSHA 1910.147 this week, schema work starts May 18.

### 2026-05-18
- Created ProcedureStep.cs and TrainingProcedure.cs, committed to main
- Updated timeline with vacation blocks (Jul 20 - Aug 2, Aug 17 - Aug 23)

### 2026-05-19

- Created Assets/Data/ folder structure (Steps/, Procedures/)
- Created and filled 6 ProcedureStep assets (Step_01 through Step_06)

### 2026-05-20

- Created LOTO_Procedure TrainingProcedure asset in Assets/Data/Procedures/
- Wired all 6 ProcedureStep assets into Steps list (Elements 0–5, in order)
- Set Passing Score Percent to 80, Regulatory Reference to OSHA 1910.147
- Set Procedure Id to LOTO_001

### 2026-05-21
- Created docs/ folder and wrote procedure-schema.md
- Covers: why ScriptableObjects, enum reference table, OSHA 1910.147 step mapping, how schema feeds ProcedureRunner

### 2026-05-25
- Created Assets/Scripts/Runtime/ folder
- Created ProcedureRunner.cs skeleton: state fields, CurrentStep property, UnityEvents, empty method stubs
- Compiles clean, no Console errors
- Added ProcedureRunner GameObject to SampleScene, LOTO_Procedure asset wired into Inspector slot
- Next: implement StartProcedure(), AdvanceStep(), FailStep() logic

### 2026-05-26
- Implemented StartProcedure(), AdvanceStep(), FailStep() in ProcedureRunner.cs
- FailStep stays on current step (no advance) — correct training sim behaviour
- Added IsComplete and CurrentStepIndex properties
- Added Canvas UI with Btn_Start, Btn_Advance, Btn_Fail wired to ProcedureRunner
- Added OnEnable debug listeners to confirm events firing
- Fixed field name mismatch: stepName → displayName
- Tested: Start, Advance across 2 steps, Fail staying on current step — all correct
- Running ahead — Thu will be full 6-step end-to-end + edge case testing

### 2026-05-29
- Full 6-step end-to-end run confirmed: Passed: True
- Edge cases validated: double-start guard, advance before start, fail on last step, advance after end
- Fixed double-start bug: added isRunning guard to StartProcedure()
- W3 complete

## 2026-06

### 2026-06-01
- Created ScoringSystem.cs with TotalSteps, StepsCompleted, MistakeCount, ScorePercent
- Initialise() resets counters on call — safe for multi-run sessions
- Added ScoringSystem component to ProcedureRunner GameObject
- Added _scoring SerializedField to ProcedureRunner, wired in Inspector
- Next: MistakeDetector.cs tomorrow

### 2026-06-02
- Created MistakeDetector.cs in Assets/Scripts/Runtime/
- Subscribes to ProcedureRunner.OnMistakeMade via AddListener/RemoveListener
- Tracks full MistakeRecord history (stepIndex, stepId, timestamp)
- Smoke tested: mistake logged correctly on FailStep, step did not advance

### 2026-06-03
- Resolved duplicate ScoringSystem field in ProcedureRunner (_scoring removed)
- Fixed ScoringSystem call mismatch: replaced RecordStepScore() with RecordStepComplete() / RecordMistake()
- Added scoringSystem.Initialise(procedure.steps.Count) to StartProcedure()
- Wired ScoringSystem and MistakeDetector into ProcedureRunner
- Ran all 6 edge case tests — all passed, no crashes, scoring correct
- W4 complete

### 2026-06-08
- Created SessionLogger.cs (MonoBehaviour, Runtime folder)
- Implemented Initialise(), RecordStepComplete(), RecordMistake(), ExportJSON(), ExportCSV(), GetSessionSummary()
- Wired SessionLogger into ProcedureRunner — Initialise on start, RecordStepComplete/RecordMistake on events, GetSessionSummary on procedure end
- Smoke tested full 6-step run — summary output confirmed: 6 steps, 5 mistakes, 5.55s duration

### 2026-06-19
- Built Canvas UI: instruction text, score text, mistake text, Start/Advance/Fail buttons
- Created UIController.cs, subscribed to ProcedureRunner events (OnStepStarted, OnStepCompleted, OnMistakeMade, OnProcedureEnded)
- Found and fixed bug: ScorePercent didn't factor in MistakeCount, and pass/fail was always true regardless of mistakes
- Fixed ProcedureRunner.AdvanceStep() to compute Passed from mistakeDetector.TotalMistakes
- Fixed ScoringSystem.ScorePercent to apply mistake penalty
- Found and fixed bug: MistakeDetector.Reset() was never called in StartProcedure(), causing mistake count to persist across runs
- Verified full desktop loop end-to-end: clean run (100%, Passed: True) and mistake run (70%, Passed: False), confirmed reset works correctly across multiple runs

### 2026-06-26
- Blocked out ElectricalPanel_Scene: Panel_Housing (cube, scale 0.81/1.21/0.24)
- Added Breaker_Switch_Main (distinct size/color) + 3x Breaker_Switch (regular)
- Added LOTO_Tag_Point at main breaker handle, scaled down to read as lock point
- Placed 6 step markers (sphere proxies) matching ProcedureStep instruction text:
  Marker_NotifyAffectedEmployees, Marker_EquipmentShutdown, Marker_EnergyIsolation,
  Marker_ApplyLockoutDevice, Marker_ReleaseRestrainStoredEnergy, Marker_VerifyIsolation
- Verified panel scale against capsule reference (1.21m height, ~60% of 2m capsule — chest/head height range, matches OSHA panel references)
- Removed temporary capsule scale-check object

### 2026-07-03
- Android build pipeline setup — turned into a full debugging session
- Unity Hub's module installer is broken on current Hub version — Add/remove modules option doesn't exist for Unity 6, so went manual instead
- Installed JDK 17 (Temurin) and Android Studio (SDK + NDK) outside Unity Hub entirely
- Hit NDK version mismatch: Android Studio defaults to NDK 30, Unity 6000.1.17f1 requires r27c (27.2.12479018) specifically — installed exact version via SDK Manager with Show Package Details ticked
- Fixed Minimum API Level: was defaulted to API 24 (Nougat), needed 29 (Android 10) for Quest 3
- Hit missing CMake 3.22.1 on first build attempt — installed via SDK Manager
- Pointed Unity's External Tools (JDK/SDK/NDK) manually at real install paths instead of relying on Unity's bundled versions
- Switched to Meta Quest build profile (separate from plain Android in Unity 6's Build Profiles system)
- First Android build kicked off, ~10-15 min compile time (expected — cold Gradle cache + no build history yet)
- Learned: Unity Hub module management flow changed significantly in Unity 6, don't rely on it — manual JDK/SDK/NDK install via Android Studio is more reliable
- Blocker resolved. Pipeline proven end-to-end before headset arrives tomorrow.

### 2026-07-05
- Headset arrived. Added Rigidbody + XR Grab Interactable to LOTO_Tag_Point (untested on-device)
- Spent most of the day on dev mode/ADB setup instead:
  - Registered dev account under wrong dashboard (Wearables Dev Center, for smart glasses) — fixed by switching to Meta Horizon Developer Dashboard (developers.meta.com/horizon/manage)
  - Installed Oculus ADB drivers, fixed PATH for adb
  - Found headset was still on friend's account from his coursework — dev mode only works for the account that owns the headset, not a secondary profile
  - Tried Quest Link as a shortcut (skip ADB) — blocked by ongoing Meta-side Link outage
  - Swapped headset to my own account, mid-pairing when stopped
- Blocker: pairing/dev mode not yet confirmed. Next: finish pairing, check dev mode toggle, adb devices, Build and Run, test grab interaction on-device

### 2026-07-09

- Wired 6 Unity Tags (AffectedEmployee, MachineSwitch, EnergyIsolationPoint, LockoutPoint, StoredEnergyPoint, VerificationPanel) matching each ProcedureStep's targetObjectTag, applied to scene objects
- Retired duplicate proxy markers (Marker_EquipmentShutdown, Marker_ApplyLockoutDevice) now that real geometry (Breaker_Switch_Main, LOTO_Tag_Point) covers those steps
- Wrote StepInteractionTrigger.cs — bridges XRI Select Entered / Activated events to ProcedureRunner.AdvanceStep() / FailStep("IncorrectTarget"), tag-matched against CurrentStep
- Wired the trigger onto all 6 interactable objects
- Unhooked Btn_Advance/Btn_Fail debug listeners from ProcedureRunner — procedure state now only driven by real interactions, not manual UI clicks
- Spent most of the day on Meta XR Simulator setup instead of testing the above:
  * No Meta menu in Unity — project never had Meta XR Core SDK installed, only Unity's own XRI/OpenXR
  * Asset Store install blocked by repeated 404s (school account), switched to scoped registry (npm.developer.oculus.com) in manifest.json instead — worked
  * Core SDK 203.0.0 installed clean, Meta menu appeared, ran Project Setup Tool fixes
  * Simulator app install failed via Unity's built-in downloader (file-write error to Downloads), installed manually instead — got v201.0
  * Session instantly exits on Play: version mismatch between Core SDK (203.0.0) and Simulator app (v201.0)
  * Tried Preferences > Meta XR Simulator version selector — still serves v201 regardless of selection
  * Confirmed this is an open, currently-unresolved Meta bug (matches multiple community reports + an active investigation on Meta's own feedback tracker), not a local config issue
- Decision: dropped Simulator for today, will test StepInteractionTrigger wiring directly on physical Quest 3 tomorrow instead
- Blocker: Meta XR Simulator version mismatch — upstream bug, no client-side fix found. Not blocking tomorrow's headset test.