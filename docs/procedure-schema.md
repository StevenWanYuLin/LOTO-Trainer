# LOTO-Trainer Procedure Schema

## Overview

This document describes the ScriptableObject schema used to define training procedures
in LOTO-Trainer, and how that data feeds into the runtime execution system.

## Why ScriptableObjects Instead of Hardcoded Data

Procedure definitions are stored as ScriptableObject assets rather than hardcoded in C#.
This means individual steps and full procedures can be swapped, reordered, or edited
directly in the Unity Inspector without touching any runtime code. A different LOTO
scenario (e.g. hydraulic press vs electrical panel) only requires a new set of assets —
the ProcedureRunner logic stays identical. This is the same pattern used in data-driven
systems like dialogue trees and quest definitions in commercial games.

## Schema Structure

Two ScriptableObject types define a procedure:

**`ProcedureStep`** — one asset per step. Each step defines:
- `stepId` / `stepOrder` / `displayName` — identity fields used in session export
- `instruction` — the text shown to the trainee during that step
- `requiredAction` (ActionType) — what physical interaction the trainee must perform
- `targetObjectTag` — the Unity tag on the GameObject they must interact with
- `verification` (VerificationType) — how completion is confirmed by the system
- `mistakes` (List<MistakeRule>) — conditions that register as errors during this step
- `maxScore` / `penaltyPerMistake` — scoring parameters per step

**`TrainingProcedure`** — one asset per scenario. Holds an ordered list of
`ProcedureStep` references plus pass criteria (`passingScorePercent`) and
metadata (`procedureId`, `regulatoryReference`).

## Enum Reference

| Enum | Values | Purpose |
|---|---|---|
| `ActionType` | Notify, Shutdown, Isolate, Lockout, ReleaseEnergy, Verify | What physical action the trainee performs |
| `VerificationType` | TagPresent, LockPresent, SwitchPosition, MeterReading, ButtonPress | How the system confirms step completion |
| `MistakeTrigger` | WrongOrder, WrongTool, SkippedStep, IncorrectTarget, MissingVerify | What constitutes an error at a given step |

## OSHA 1910.147 Mapping

| Step | Asset | OSHA Clause |
|---|---|---|
| 1 | Step_01_Notify | 1910.147(d)(1) — notify affected employees |
| 2 | Step_02_Shutdown | 1910.147(d)(2) — equipment shutdown |
| 3 | Step_03_Isolate | 1910.147(d)(3) — energy isolation |
| 4 | Step_04_Lockout | 1910.147(d)(4) — apply lockout device |
| 5 | Step_05_Release | 1910.147(d)(5) — release stored energy |
| 6 | Step_06_Verify | 1910.147(d)(6) — verify de-energised state |

## How This Feeds ProcedureRunner

At runtime, `ProcedureRunner` loads the assigned `TrainingProcedure` asset and
iterates through `steps[]` in order. For each step it reads `requiredAction` to
know which interaction to wait for, monitors `mistakes` to detect errors in real
time, and calls the scoring system on step completion. Because all behaviour is
driven by the asset data, adding a new procedure type requires zero code changes.