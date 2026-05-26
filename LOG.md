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
