using System;
using System.Collections;
using UnityEngine;

public class ObjectSwitcher : MonoBehaviour
{

    [Header("Lerping Scale")]
    public Vector3 targetScale;
    public float scaleLerpDuration = 1f;
    private Vector3 originalScale;

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
                StartCoroutine(PreviousObjectLerpScale(previousActiveObject);
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
        float elapsedTime = 0f;
        originalScale = obj.transform.localScale;
        while (elapsedTime < scaleLerpDuration)
        {
            float t = elapsedTime / scaleLerpDuration;
            obj.transform.localScale = Vector3.Lerp(targetScale, originalScale, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        obj.transform.localScale = originalScale;
    }

    IEnumerator PreviousObjectLerpScale(GameObject obj)
    {
        float elapsedTime = 0f;
        originalScale = obj.transform.localScale;
        while (elapsedTime < scaleLerpDuration)
        {
            float t = elapsedTime / scaleLerpDuration;
            obj.transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        obj.transform.localScale = originalScale;
    }
}
