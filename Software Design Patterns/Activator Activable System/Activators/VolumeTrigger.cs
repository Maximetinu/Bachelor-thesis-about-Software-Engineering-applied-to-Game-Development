using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]
public class VolumeTrigger : Activator
{
    public string[] activatorReactionTags;
    [Range(0.0f, 10.0f)]
    public float delay = 0.0f;

    int objectsInside = 0;
    float radius;

    private void Start()
    {
        radius = GetComponent<SphereCollider>().radius;
    }

    private void Update()
    {
        int previousObjectsInside = objectsInside;
        objectsInside = 0;
        Collider[] collidersInside = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider c in collidersInside)
        {
            if (IsActivatorTag(c.tag))
            {
                objectsInside++;
            }
        }

        if (previousObjectsInside == 0 && objectsInside >= 1)
        {
            if (delay == 0)
                onActivate.Invoke();
            else
                Invoke("OnObjectIn", delay);
        }

        if (previousObjectsInside >= 1 && objectsInside == 0)
        {
            if (delay == 0)
                onDeactivate.Invoke();
            else
                Invoke("OnObjectExit", delay);
        }

        if (objectsInside >= 1)
        {
            whileActive.Invoke();
        }
    }

    void OnObjectIn()
    {
        onActivate.Invoke();
    }

    void OnObjectExit()
    {
        onDeactivate.Invoke();
    }

    bool IsActivatorTag(string tag)
    {
        bool tagIsActivator = false;
        foreach (string t in activatorReactionTags)
        {
            if (t == tag)
            {
                tagIsActivator = true;
                break;
            }
        }
        return tagIsActivator;
    }

    public void DestroyThis()
    {
        Destroy(gameObject);
    }


}
