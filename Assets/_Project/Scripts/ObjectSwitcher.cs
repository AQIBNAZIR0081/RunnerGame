using System;
using System.Collections;
using UnityEngine;

public class ObjectSwitcher : MonoBehaviour
{

    [Header("Lerping Scale")]
    public float lerpScaleDuration = 0.5f;

    private Vector3 originalScale;
    private float elapsedTime = 0f;


    [SerializeField]
    private GameObject[] characterContainer;
    private GameObject previousActiveObject;


    private void Start() {
        for (int i = 0; i < characterContainer.Length; i++)
        {
            if (characterContainer[i].activeInHierarchy)
            {
                previousActiveObject = characterContainer[i];
                break;
            }
        }
    }

    private void SwitchObjectCheck()
    {
        for (int i = 0; i < characterContainer.Length; i++)
        {
            if (characterContainer[i].activeInHierarchy)
            {
                previousActiveObject = characterContainer[i];
                previousActiveObject.SetActive(false);
                break;
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

    private void ActivateObject(int activeObjectIndex) {
        // stop and ongoing lerp coroutine before starting a new one
        StopAllCoroutines();

        GameObject newObject = characterContainer[activeObjectIndex];

        // store the originalScale of the new object before setting it to zero for the lerp
        originalScale = newObject.transform.localScale;

        // set position to match previous active object position
        if (previousActiveObject != null) {
            newObject.transform.position = previousActiveObject.transform.position;
        }

        // start with zero scale
        newObject.transform.localScale = Vector3.zero;

        // Activate the object 
        newObject.SetActive(true);

        // Start the lerp coroutine
        StartCoroutine(ObjectLerpScale(newObject));
    }


    IEnumerator ObjectLerpScale(GameObject obj)
    {
        elapsedTime = 0f;

        while (elapsedTime < lerpScaleDuration)
        {
            float t = elapsedTime / lerpScaleDuration;

            Debug.Log("Lerping " + obj.name + " local sale " + obj.transform.localScale);

            obj.transform.localScale = Vector3.Lerp(Vector3.zero, originalScale, t);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Ensure the final scale is set to the original scale
        obj.transform.localScale = originalScale;

        elapsedTime = 0f;
    }

}
