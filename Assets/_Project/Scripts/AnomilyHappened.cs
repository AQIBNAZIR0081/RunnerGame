using UnityEngine;

public class AnomilyHappened : MonoBehaviour
{
    public GameObject[] bolders;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (GameObject a in bolders)
        {
            a.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (GameObject a in bolders)
            {
                a.SetActive(true);
            }
        }
    }

}
