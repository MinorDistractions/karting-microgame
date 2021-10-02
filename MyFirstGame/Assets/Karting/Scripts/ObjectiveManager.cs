using System;
using System.Collections.Generic;
using UnityEngine;
// Holds a list of the current objectives (which are added at the start) 
// Checks if all objectives are complete (gameflow manager checks each fame and ends game if true)
// Adds objectives as they register at the start, not sure if new objectives can be added during gameplay
// OnEnable() is called once at the start of the game, changing to Start() seems to work too

public class ObjectiveManager : MonoBehaviour
{

    private List<Objective> m_Objectives = new List<Objective>();

    //JF Find out what => means here, why not = ?
    public List<Objective> Objectives => m_Objectives;

    //JF Does this really need to be an Action<> ? why not just a method
    public static Action<Objective> RegisterObjective;

    //JF Why do we need to do this inside OnEnable() or Start() ?
    public void OnEnable()
    {
        RegisterObjective += OnRegisterObjective;
    }
    
    public bool AreAllObjectivesCompleted()
    {
        if (m_Objectives.Count == 0) //JF if there were no objevtives they can't have just been completed and the game shouldn't stop
            return false;

        //JF why not a foreach here?
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
