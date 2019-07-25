using UnityEngine;

public class ActivableAnimationPrototype : MonoBehaviour
{
    public Vector3 activatedPosition;

    [Range(0.1f, 10.0f)]
    public float Speed = 1.0f;

    Vector3 originalPosition;
    Vector3 currentTargetPosition;

    void Start()
    {
        originalPosition = transform.position;
        currentTargetPosition = originalPosition;
    }

    void Update()
    {
        float step = Speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, currentTargetPosition, step);
    }

    public void Activate()
    {
        currentTargetPosition = originalPosition + activatedPosition;
    }

    public void Deactivate()
    {
        currentTargetPosition = originalPosition;
    }

    public bool IsInActivatedPosition()
    {
        return (transform.position == activatedPosition);
    }

}