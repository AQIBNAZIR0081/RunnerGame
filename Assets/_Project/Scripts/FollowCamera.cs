using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Camera _camera;
    public Vector3 offset; // The offset from the target position


    private void Start()
    {
        _camera = Camera.main;
    }

    private void LateUpdate()
    {
        _camera.transform.position = transform.position + offset;
    }
}
