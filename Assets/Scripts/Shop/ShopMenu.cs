using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ShopMenu : MonoBehaviour
{
    [System.Serializable]
    class ShopItem
    {
        public Sprite image;
        public int price;
        public bool isPurchased = false;
    }

    GameObject ItemTemplate;
    GameObject g;

    [SerializeField] Transform Content;
    [SerializeField] List<ShopItem> ShopItemList;

    Button buyButton;

    // Start is called before the first frame update
    void Start()
    {
        ItemTemplate = Content.GetChild(0).gameObject;

        int len = ShopItemList.Count;
        for (int i = 0; i < len; i++)
        {
            g = Instantiate(ItemTemplate, Content);

            g.transform.GetChild(0).GetComponent<Image>().sprite = ShopItemList[i].image;
            g.transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>().text = ShopItemList[i].price.ToString();
            g.transform.GetChild(1).GetComponent<Button>().interactable = ShopItemList[i].isPurchased;

            buyButton = g.transform.GetChild(1).GetComponent<Button>();
            buyButton.interactable = !ShopItemList[i].isPurchased;
            buyButton.AddEventListener(i, onShopButtonClicked);
        }

        Destroy(ItemTemplate);
    }

    void onShopButtonClicked(int itemIndex)
    {
        if (Game.Instance.HasEnoughCoins(ShopItemList[itemIndex].price))
        {
            Game.Instance.UseCoins(ShopItemList[itemIndex].price);
            // purchase item
            ShopItemList[itemIndex].isPurchased = true;

            // disable Button
            buyButton = Content.GetChild(itemIndex).GetChild(1).GetComponent<Button>();
            buyButton.interactable = false;
            buyButton.transform.GetChild(0).GetComponent<TMP_Text>().text = "PURCHASED";

            // change ui coin text
            Game.Instance.UpdateAllCoinsUIText();
        }
        else
        {
            print("You don't have enough money");
        }
    }
}
