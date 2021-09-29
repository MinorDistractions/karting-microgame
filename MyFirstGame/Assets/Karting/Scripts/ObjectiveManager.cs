using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    //JF lookup instantiating vs declaring
    //https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/how-to-initialize-objects-by-using-an-object-initializer
    private List<Objective> m_Objectives = new List<Objective>();

    //JF find out what => means here, why not =?
    public List<Objective> Objectives => m_Objectives;

    public static Action<Objective> RegisterObjective;

    public void OnEnable()
    {
        RegisterObjective += OnRegisterObjective;
    }
    
    public bool AreAllObjectivesCompleted()
    {
        if (m_Objectives.Count == 0)
            return false;

        for (int i = 0; i < m_Objectives.Count; i++)
        {
            // pass every objectives to check if they have been completed
            if (m_Objectives[i].isBlocking())
            {
                // break the loop as soon as we find one uncompleted objective
                return false;
            }
        }

        // found no uncompleted objective
        return true;
    }

    public void OnRegisterObjective(Objective objective)
    {
        m_Objectives.Add(objective);
    }
}
