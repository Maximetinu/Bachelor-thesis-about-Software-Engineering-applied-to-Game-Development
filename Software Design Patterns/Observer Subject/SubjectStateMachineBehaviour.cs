///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2017 GambusinoLabs - All Rights Reserved
// Unauthorized copying of this file, via any medium is strictly prohibited
// Proprietary and confidential
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System.Collections.Generic;
using UnityEngine;
//Invokes the notificaton method
public abstract class SubjectStateMachineBehaviour : StateMachineBehaviour
{
    //A list with observers that are waiting for something to happen
    List<Observer> observers = new List<Observer>();

    //Send notifications if something has happened
    public void Notify()
    {
        for (int i = 0; i < observers.Count; i++)
        {
            //Notify all observers even though some may not be interested in what has happened
            //Each observer should check if it is interested in this event
            observers[i].OnNotify();
        }
    }

    //Add observer to the list
    public void AddObserver(Observer observer)
    {
        observers.Add(observer);
    }

    //Remove observer from the list
    public void RemoveObserver(Observer observer)
    {
        observers.Remove(observer);
    }
}