using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    private TowerSpawn towerSpawner;

    private Slider baseSlider;

    [SerializeField]
    private GameObject shopGO;

    [SerializeField]
    private TextMeshProUGUI[] itemAmountsTexts;

    [SerializeField]
    private TextMeshProUGUI goldAmountText;

    [SerializeField]
    private TextMeshProUGUI healthAmountText;

    public static UI instance; //Singleton

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;

        shopGO.SetActive(false);
        GameObject player = GameObject.FindWithTag("Player");
        towerSpawner = player.GetComponent<TowerSpawn>();

        baseSlider = GameObject.Find("baseSlider").GetComponent<Slider>();

        baseSlider.maxValue = baseSlider.value = PlayerManager.instance.Health;
    }

    public void SelectItem(int itemIndex)
    {
        towerSpawner.TowerIndex = itemIndex;
    }

    public void ItemPurchaseButtons(int itemPrice)
    {
        if (!(PlayerManager.instance.Gold >= itemPrice)) { return; }
        
        int itemIndex = 0;

        switch (itemPrice)
        {
            case 100:
                itemIndex = 0; break;
            case 200:
                itemIndex = 1; break;
            case 300:
                itemIndex = 2; break;
            default:
                return;
        }

        PlayerManager.instance.Gold = -itemPrice;
        towerSpawner.TowerIndex = itemIndex;
        towerSpawner.setItemAmount(itemIndex);
    }

    public void setItemAmountText(int index, int amount)
    {
        if (index < itemAmountsTexts.Length)
            itemAmountsTexts[index].text = amount.ToString();
    }

    public void setHealthText(int hp)
    {
        healthAmountText.text = hp.ToString();
        baseSlider.value = hp;
    }

    public void setGoldText(float gold = 5)
    {
        goldAmountText.text = gold.ToString();
    }

    public void toggleShop()
	{
        shopGO.SetActive(!shopGO.activeSelf);
	}
}
