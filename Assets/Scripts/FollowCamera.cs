using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target; // The target object to follow
    public Vector3 offset; // The offset from the target position

    // Update is called once per frame
    private void LateUpdate()
    {
        transform.position = target.position + offset;
    }
}
