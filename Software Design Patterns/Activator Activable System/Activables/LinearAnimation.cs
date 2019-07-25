using UnityEngine;

public class LinearAnimation : Activable
{
    public Vector3 activatedPosition;

    [Range(0.1f, 10.0f)]
    public float speed = 1.0f;
    public bool activateOnlyOnce = false;

    Vector3 originalPosition;
    Vector3 currentTargetPosition;

    void Start()
    {
        originalPosition = transform.position;
        currentTargetPosition = originalPosition;
    }

    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, currentTargetPosition, step);
    }

    public override void Activate()
    {
        currentTargetPosition = originalPosition + activatedPosition;
    }

    public override void Deactivate()
    {
        if(!activateOnlyOnce)
            currentTargetPosition = originalPosition;
    }

    public override void WhileActive() { /* Do Nothing */}

}