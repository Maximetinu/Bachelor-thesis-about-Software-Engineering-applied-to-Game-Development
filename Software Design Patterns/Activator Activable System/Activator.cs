using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Activator : MonoBehaviour
{
    [SerializeField]
    private List<Activable> activableTargets;

    [Space(20)]
    public UnityEvent onActivate;
    public UnityEvent whileActive;
    public UnityEvent onDeactivate;

    protected virtual void Awake()
    {
        AddTargetsToEvents();
    }

    public void Register(Activable a)
    {
        onActivate.AddListener(a.Activate);
        whileActive.AddListener(a.WhileActive);
        onDeactivate.AddListener(a.Deactivate);
    }

    public void Unregister(Activable a)
    {
        onActivate.RemoveListener(a.Activate);
        whileActive.RemoveListener(a.WhileActive);
        onDeactivate.RemoveListener(a.Deactivate);
    }

    void AddTargetsToEvents()
    {
        foreach (Activable a in activableTargets)
        {
            this.Register(a);
        }
    }

    [ContextMenu("Test activate")]
    void Activate()
    {
        onActivate.Invoke();
    }

    [ContextMenu("Test deactivate")]
    void Deactivate()
    {
        onDeactivate.Invoke();
    }
}
