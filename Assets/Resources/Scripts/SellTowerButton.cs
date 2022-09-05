using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellTowerButton : MonoBehaviour
{
    private GameObject place;
    private TowerSO towerSO;

    public void Initialize(GameObject place, TowerSO towerSO)
    {
        this.place = place;
        this.towerSO = towerSO;

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;

            if (child.name == "text")
            {
                child.GetComponent<Text>().text = towerSO.cost.ToString();
            }
        }
    }

    public void OnClick()
    {
        Money.Deposit(towerSO.cost);
        EmptyPlaceScript.Spawn(place.transform.position);
        Destroy(place);
    }
}
