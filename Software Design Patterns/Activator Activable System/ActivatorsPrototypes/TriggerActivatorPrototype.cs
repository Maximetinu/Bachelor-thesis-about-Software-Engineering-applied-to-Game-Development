using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]
public class TriggerActivatorPrototype : MonoBehaviour
{
    public string[] activatorTags;
    [Range(0.0f, 10.0f)]
    public float delay = 0.0f;

    [Header("Se ejecutará cuando el objeto se pose sobre el trigger")]
    public UnityEvent objectIn;

    [Header("Se ejecutará constantemente mientras el objeto esté sobre el trigger")]
    public UnityEvent objectStays;

    [Header("Se ejecutará cuando el objeto salga del trigger")]
    public UnityEvent objectExit;

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
                objectIn.Invoke();
            else
                Invoke("OnObjectIn", delay);
        }

        if (previousObjectsInside >= 1 && objectsInside == 0)
        {
            if (delay == 0)
                objectExit.Invoke();
            else
                Invoke("OnObjectExit", delay);
        }

        if (objectsInside >= 1)
        {
            objectStays.Invoke();
        }
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (IsActivatorTag(other.tag))
    //     {
    //         if (objectsInside == 0)
    //         {
    //             if (delay == 0)
    //                 objectIn.Invoke();
    //             else
    //                 Invoke("OnObjectIn", delay);

    //         }
    //     }
    // }

    // private void OnTriggerStay(Collider other)
    // {
    //     if (IsActivatorTag(other.tag))
    //     {
    //         objectStays.Invoke();
    //     }
    // }

    // private void OnTriggerExit(Collider other)
    // {
    //     if (IsActivatorTag(other.tag))
    //     {
    //         if (objectsInside == 0)
    //         {
    //             if (delay == 0)
    //                 objectExit.Invoke();
    //             else
    //                 Invoke("OnObjectExit", delay);
    //         }
    //     }
    // }

    void OnObjectIn()
    {
        objectIn.Invoke();
    }

    void OnObjectExit()
    {
        objectExit.Invoke();
    }

    bool IsActivatorTag(string tag)
    {
        bool tagIsActivator = false;
        foreach (string t in activatorTags)
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
