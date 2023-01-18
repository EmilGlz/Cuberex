using UnityEngine;

public class SkinSelector : MonoBehaviour
{
    public int currentModelIndex = 0;
    public GameObject[] models;

    // Start is called before the first frame update
    void Start()
    {
        currentModelIndex = PlayerPrefs.GetInt("SelectedSkin", 0);
        foreach (GameObject plane in models)
            plane.SetActive(false);

        models[currentModelIndex].SetActive(true);
    }
}

