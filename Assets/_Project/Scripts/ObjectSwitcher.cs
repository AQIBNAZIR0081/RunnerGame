using System;
using System.Collections;
using UnityEngine;

public class ObjectSwitcher : MonoBehaviour
{

    [Header("Lerping Scale")]
    public Vector3 targetScale;
    public float scaleLerpDuration = 0.5f;


    private Vector3 currentScale;
    private float elapsedTime = 0f;
    private Vector3 initialScaleOfEachObject;


    [SerializeField]
    private GameObject[] characterContainer;
    private GameObject previousActiveObject;

    private void Start()
    {
        foreach (var obj in characterContainer)
        {
            initialScaleOfEachObject = obj.transform.localScale;
             Debug.Log("Initial Scale of " + obj.name + " is " + initialScaleOfEachObject);
        }
    }


    private void SwitchObjectCheck()
    {
        for (int i = 0; i < characterContainer.Length; i++)
        {
            if (characterContainer[i].activeInHierarchy)
            {
                previousActiveObject = characterContainer[i];
                Debug.Log("Object " + previousActiveObject.name + " local sale " + previousActiveObject.transform.localScale);

                previousActiveObject.SetActive(false);
            }
           
        }
    }


    public void OnClickPersonButton()
    {
        SwitchObjectCheck();
        ActivateObject(0);
    }

    public void OnClickBulldozerButton()
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
        StartCoroutine(NewActiveObjectLerpScale(characterContainer[activeObjectIndex]));
        characterContainer[activeObjectIndex].SetActive(true);
    }


    IEnumerator NewActiveObjectLerpScale(GameObject obj)
    {
        currentScale = obj.transform.localScale;

        while (elapsedTime < scaleLerpDuration)
        {
            float t = elapsedTime / scaleLerpDuration;
            obj.transform.localScale = new Vector3(0, 0, 0);

            Debug.Log("Lerping " + obj.name + " local sale " + obj.transform.localScale);

            obj.transform.localScale = Vector3.Lerp(targetScale, currentScale, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0;
        Debug.Log("Object " + obj.name + " local sale " + obj.transform.localScale + " after lerp");
    }

}
