using UnityEngine;

public class ContinuousRotation : MonoBehaviour
{

    public Vector3 rotationAxis = new Vector3(0, 1.0f, 0);
    public float Speed;
    public bool rotateAlways = false;

    private void Rotate(float factor = 1.0f)
    {
        transform.Rotate(rotationAxis * Speed * factor, Space.World);
    }

    private void Update()
    {
        if (rotateAlways)
        {
            Rotate();
        }
    }
}
