using UnityEngine;

public class ObjectSwitcher : MonoBehaviour
{
    [SerializeField]
    private GameObject[] characterContainer;

    private GameObject previousActiveObject;

    private void SwitchObjectCheck()
    {
        for (int i = 0; i < characterContainer.Length; i++)
        {

            if (characterContainer[i].activeInHierarchy)
            {
                previousActiveObject = characterContainer[i];
                previousActiveObject.SetActive(false);
            }
        }
    }


    public void OnClickPersonButton()
    {
        SwitchObjectCheck();
        ActivateObject(0);
    }

    public void OnClickCubeButton()
    {
        SwitchObjectCheck();
        ActivateObject(1);
    }

    public void OnClickCarButton()
    {
        SwitchObjectCheck();
        ActivateObject(2);
    }

    private void ActivateObject(int activeObjectIndex)
    {
        characterContainer[activeObjectIndex].transform.position = previousActiveObject.transform.position;
        characterContainer[activeObjectIndex].SetActive(true);
    }
}
