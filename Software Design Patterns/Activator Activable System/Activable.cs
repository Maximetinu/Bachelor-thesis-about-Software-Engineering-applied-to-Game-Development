using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Activable : MonoBehaviour, IActivable
{
    // public Activator activator;

    // protected virtual void Start()
    // {
    //     activator.onActivate.AddListener(Activate);
    //     activator.onDeactivate.AddListener(Deactivate);
    //     activator.whileActive.AddListener(WhileActive);
    // }

    public abstract void Activate();
    public abstract void WhileActive();
    public abstract void Deactivate();
}

public interface IActivable
{
    void Activate();
    void WhileActive();
    void Deactivate();
}
