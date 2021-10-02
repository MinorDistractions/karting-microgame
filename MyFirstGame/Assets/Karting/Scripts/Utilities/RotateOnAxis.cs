using UnityEngine;

public class RotateOnAxis : MonoBehaviour
{
    [Tooltip("Applies a rotation of eulerAngles.z degrees around the z-axis, eulerAngles.x degrees around the x-axis, and eulerAngles.y degrees around the y-axis (in that order).")]
    public Vector3 rotationSpeed;

    //JF change this to a coroutine so it only updates every 0.1 seconds instead of every frame?
    void Update()
    {
        transform.Rotate(rotationSpeed);
    }
}
