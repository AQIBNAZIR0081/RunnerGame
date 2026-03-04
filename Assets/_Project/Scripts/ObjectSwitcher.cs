using System;
using System.Collections;
using UnityEngine;

public class ObjectSwitcher : MonoBehaviour
{

    [Header("Lerping Scale")]
    public float lerpScaleDuration = 0.5f;


    private Vector3 targetScale = new Vector3(0, 0, 0);
    private Vector3 currentScale;
    private float elapsedTime = 0f;


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
        StartCoroutine(ObjectLerpScale(characterContainer[activeObjectIndex]));
        characterContainer[activeObjectIndex].SetActive(true);
    }


    IEnumerator ObjectLerpScale(GameObject obj)
    {
        currentScale = obj.transform.localScale;

        while (elapsedTime < lerpScaleDuration)
        {
            float t = elapsedTime / lerpScaleDuration;
            //obj.transform.localScale = new Vector3(0, 0, 0);

            Debug.Log("Lerping " + obj.name + " local sale " + obj.transform.localScale);

            obj.transform.localScale = Vector3.Lerp(targetScale, currentScale, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0;
    }

}
