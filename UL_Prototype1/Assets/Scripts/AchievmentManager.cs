using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievmentManager : MonoBehaviour
{
    [SerializeField]private GameObject UI;

    public List<Achievment> achievments;

    private int _score;

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
        Obstacle.destroyedEvent += AddScore;
    }

    private void InitializeAchevments()
    {
        if (achievments != null)
            return;

        achievments = new List<Achievment>();
        achievments.Add(new Achievment("First Blood!", "Destroy your first Obstacle.", (object o) => _score == 1));
        achievments.Add(new Achievment("Five Kills!", "Destroy five Obstacles", (object o) => _score == 5));
        achievments.Add(new Achievment("TENtaKILL!", "Destroy ten Obstacles", (object o) => _score == 10));
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
            bool achievedNow = acheivment.UpdateCompletion();
            if (achievedNow)
            {
                UI.GetComponent<UI>().ShowAchievment(acheivment.title, acheivment.description);
            }
        }
    }
    public void AddScore()
    {
        _score++;
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

    public bool UpdateCompletion()
    {
        if (achieved)
            return false;

        if (RequirementsMet())
        {
            achieved = true;
            return true;
        }

        return false;
    }

    public bool RequirementsMet()
    {
        return requirement.Invoke(null);
    }
}
