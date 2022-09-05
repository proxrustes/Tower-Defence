using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateTowerButton : MonoBehaviour
{
    private GameObject place;
    private TowerSO towerSO;
    private uint cost;

    public void Initialize(GameObject place, TowerSO towerSO, uint cost)
    {
        this.place = place;
        this.towerSO = towerSO;
        this.cost = cost;

        transform.GetComponent<Image>().sprite = towerSO.icon;

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject obj = transform.GetChild(i).gameObject;

            if (obj.name == "text")
            {
                obj.GetComponent<Text>().text = cost.ToString();
            }
        }
    }

    public void OnClick()
    {
        if (Money.Pay(cost))
        {
            GameObject new_place_obj = Instantiate(towerSO.prefab, place.transform.position, place.transform.rotation, place.transform.parent);
            Tower new_tower = new_place_obj.GetComponent<Tower>();
            new_tower.Initialize(towerSO);
            Destroy(place);
        }
    }
}
