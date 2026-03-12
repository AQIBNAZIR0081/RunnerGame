using Unity.Cinemachine;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private CinemachineCamera _camera;
    [SerializeField] private GameObject[] _targets;

    private void Start()
    {
        if(_camera == null) {

            _camera = FindAnyObjectByType<CinemachineCamera>();
        }
    }

    private void LateUpdate()
    {
        for (int i = 0; i < _targets.Length; i++)
        {
            if (_targets[i] != null && _targets[i].activeInHierarchy)
            {
                _camera.Follow = _targets[i].transform;
                _camera.LookAt = _targets[i].transform;
                break;
            }
        }
    }
}
