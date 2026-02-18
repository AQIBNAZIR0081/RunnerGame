using UnityEngine;

public class ObjectSwitcher : MonoBehaviour
{
    [SerializeField]
    private GameObject _person;
    [SerializeField]
    private GameObject _cube;
    [SerializeField]
    private GameObject _car;

    
    public void OnClickPersonButton()
    {
        _person.SetActive(true);
        _cube.SetActive(false);
        _car.SetActive(false);

        SwipeController.Instance.currentLine = 1;
        Debug.Log("DesiredLine after Switch: " + SwipeController.Instance.currentLine);
    }

    public void OnClickCubeButton()
    {
        _person.SetActive(false);
        _cube.SetActive(true);
        _car.SetActive(false);

        SwipeController.Instance.currentLine = 1;
        Debug.Log("DesiredLine after Switch: " + SwipeController.Instance.currentLine);
    }

    public void OnClickCarButton()
    {
        _person.SetActive(false);
        _cube.SetActive(false);
        _car.SetActive(true);

        SwipeController.Instance.currentLine = 1;
        Debug.Log("DesiredLine after Switch: " + SwipeController.Instance.currentLine);
    }
}
