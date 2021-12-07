using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievmentManager : MonoBehaviour
{
    public List<Achievment> achievments;

    public int integer;
    public float floating_point;

    public bool AchievmentUnlocked(string achievmentName)
    {
        bool result = false;
        if (achievments == null)
            return false;
        Achievment[] achievmentsArray = achievments.ToArray();
        Achievment a = Array.Find(achievmentsArray, ach => achievmentName == ach.title);

        if(a == null)
            return result;

        result = a.achieved;
        return result;
    }

    private void Start()
    {
        InitializeAchevments();
    }

    private void InitializeAchevments()
    {
        if (achievments != null)
            return;

        achievments = new List<Achievment>();
        achievments.Add(new Achievment("First Kill!", "Get your first kill in the game.", (object o) => integer == 1));
        achievments.Add(new Achievment("Five Kills!", "Get five kills", (object o) => integer == 5));
    }

    private void Update()
    {
        CheckAchievmentCompletion();
    }

    private void CheckAchievmentCompletion()
    {
        if (achievments == null)
            return;
        foreach (var acheivment in achievments)
        {
            acheivment.UpdateCompletion();
        }
    }
}

public class Achievment
{
    public Achievment(string title, string description, Predicate<object> requirement)
    {
        this.title = title;
        this.description = description; 
        this.requirement = requirement;
    }

    public string title;
    public string description;
    public Predicate<object> requirement;

    public bool achieved;

    public void UpdateCompletion()
    {
        if (achieved)
            return;

        if (RequirementsMet())
        {
            Debug.Log($"{title}: {description}");
            achieved = true;
        }
    }

    public bool RequirementsMet()
    {
        return requirement.Invoke(null);
    }
}
