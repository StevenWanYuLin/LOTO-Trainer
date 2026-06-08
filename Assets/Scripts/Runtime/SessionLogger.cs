using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;

public class SessionLogger : MonoBehaviour
{
    public string sessionId;
    public DateTime startTime;
    public DateTime endTime;
    public List<string> steps;
    public List<string> mistakes;

    public void Initialise()
    {
        sessionId = Guid.NewGuid().ToString();
        startTime = DateTime.Now;
        steps = new List<string>();
        mistakes = new List<string>();
    }

    public void RecordStepComplete(ProcedureStep step)
    {
        steps.Add($"{step.displayName} - {DateTime.Now}");
    }

    public void RecordMistake(ProcedureStep step, string mistake)
    {
        mistakes.Add($"{step.displayName} - {mistake}");
    }

    public void ExportJSON(string filePath)
    {
        SessionData newData = new SessionData()
        {
            sessionId = sessionId,
            startTime = startTime.ToString(),
            endTime = endTime.ToString(),
            steps = steps,
            mistakes = mistakes
        };
        string json = JsonUtility.ToJson(newData);
        File.WriteAllText(filePath, json);
    }

    public void ExportCSV(string filePath)
    {
        File.WriteAllText(filePath,
            sessionId + ", " + startTime + ", " + endTime + "\n" +
            "Steps\n" + string.Join("\n", steps) + "\n" +
            "Mistakes\n" + string.Join("\n", mistakes));
    }

    public string GetSessionSummary()
    {
        endTime = DateTime.Now;
        return "Total steps completed: " + steps.Count + 
            " ,Total mistakes: " + mistakes.Count + 
            " ,Session duration: " + (endTime - startTime).TotalSeconds;
    }

    [Serializable]
    public class SessionData
    {
        public string sessionId;
        public string startTime;
        public string endTime;
        public List<string> steps;
        public List<string> mistakes;
    }
}