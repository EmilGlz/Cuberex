using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public int currentModelIndex = 0;

    public GameObject[] models;

    public SkinInfo[] skinInfo;

    [SerializeField] Button buyButton;
    [SerializeField] TMP_Text priceText;

    private int totalCoin;

    [SerializeField] GameObject[] visualModels;

    // Start is called before the first frame update
    void Start()
    {
        totalCoin = (int)PlayerPrefs.GetFloat("CoinSave");

        foreach (SkinInfo model in skinInfo)
        {
            if (model.price == 0)
                model.isUnlocked = true;
            else
                model.isUnlocked = PlayerPrefs.GetInt(model.title, 0) != 0;
        }

        currentModelIndex = PlayerPrefs.GetInt("SelectedSkin", 0);
        foreach (GameObject plane in models)
            plane.SetActive(false);

        models[currentModelIndex].SetActive(true);

        // Visual Models
        foreach (GameObject visualPlane in visualModels)
            visualPlane.SetActive(true);

        visualModels[currentModelIndex].SetActive(false);
    }

    private void Update()
    {
        UpdateUi();

        if (SwipeManager.swipeRight)
        {
            ChangePrevious();
        }
        else if (SwipeManager.swipeLeft)
        {
            ChangeNext();
        }
    }

    public void ChangeNext()
    {
        models[currentModelIndex].SetActive(false);

        // Visual Models
        visualModels[currentModelIndex].SetActive(true);

        currentModelIndex++;

        if (currentModelIndex == models.Length)
            currentModelIndex = 0;

        models[currentModelIndex].SetActive(true);

        // Visual Models
        visualModels[currentModelIndex].SetActive(false);


        SkinInfo si = skinInfo[currentModelIndex];
        if (!si.isUnlocked)
            return;

        PlayerPrefs.SetInt("SelectedSkin", currentModelIndex);
    }

    public void ChangePrevious()
    {
        models[currentModelIndex].SetActive(false);

        // Visual Models
        visualModels[currentModelIndex].SetActive(true);

        currentModelIndex--;

        if (currentModelIndex == -1)
            currentModelIndex = models.Length - 1;

        models[currentModelIndex].SetActive(true);

        // Visual Models
        visualModels[currentModelIndex].SetActive(false);

        SkinInfo si = skinInfo[currentModelIndex];
        if (!si.isUnlocked)
            return;

        PlayerPrefs.SetInt("SelectedSkin", currentModelIndex);
    }

    public void UnlockCar()
    {
        SkinInfo si = skinInfo[currentModelIndex];

        totalCoin -= si.price;
        PlayerPrefs.SetFloat("CoinSave", totalCoin);

        PlayerPrefs.SetInt(si.title, 1);
        PlayerPrefs.SetInt("SelectedSkin", currentModelIndex);

        si.isUnlocked = true;
    }

    private void UpdateUi()
    {
        SkinInfo si = skinInfo[currentModelIndex];

        if (si.isUnlocked)
        {
            buyButton.gameObject.SetActive(false);
            priceText.gameObject.SetActive(false);
        }
        else
        {
            buyButton.gameObject.SetActive(true);
            priceText.gameObject.SetActive(true);
            priceText.SetText(si.price.ToString());

            if (PlayerPrefs.GetFloat("CoinSave") > si.price)
            {
                buyButton.interactable = true;
            }
            else
            {
                buyButton.interactable = false;
            }
        }
    }
}
