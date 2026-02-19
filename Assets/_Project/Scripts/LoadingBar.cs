using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    public int sceneIndexToLoad;
    public Slider loadingBarSlider;
    public Animator loadingBarAnimator;

    private void Start()
    {
        loadingBarAnimator = GetComponent<Animator>();
    }
    
    IEnumerator LoadSceneAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            loadingBarSlider.value = progress;

            yield return null;
        }

    }


    // This method is passed to the animation event.
    public void LoadSceneOnAnimationEvent()
    {
        StartCoroutine(LoadSceneAsync(sceneIndexToLoad));
    }
}
