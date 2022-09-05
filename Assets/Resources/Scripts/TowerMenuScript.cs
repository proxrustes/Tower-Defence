using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerMenuScript : MonoBehaviour
{
    GameObject place;
    bool OnButtonClicked;

    public void Initialize(GameObject place, List<TowerSO.MenuItems> towers, TowerSO cur_tower)
    {
        this.place = place;

        GameObject tower_button_prefab = Singleton.GetObject<GameObject>("create tower button");

        for (int i = 0; i < towers.Count; i++)
        {
            GameObject button_obj = Instantiate(tower_button_prefab, transform);
            button_obj.transform.localPosition = new Vector3(Mathf.Sin((Mathf.PI * 2) / (towers.Count + 1) * (i + 1)), -Mathf.Cos((Mathf.PI * 2) / (towers.Count + 1) * (i + 1)), 0) * 38;
            button_obj.transform.localPosition += new Vector3(0, 0, 0.1f);

            var button_script = button_obj.GetComponent<CreateTowerButton>();
            button_script.Initialize(place, towers[i].towerSO, towers[i].cost);

            button_obj.GetComponent<Button>().onClick.AddListener(OnButtonClick);
        }

        if (cur_tower != null)
        {
            GameObject sell_button_prefab = Singleton.GetObject<GameObject>("sell tower button");

            GameObject button_obj = Instantiate(sell_button_prefab, transform);
            button_obj.transform.localPosition += new Vector3(0, -38, 0.1f);

            var sell_button_script = button_obj.GetComponent<SellTowerButton>();
            sell_button_script.Initialize(place, cur_tower);

            button_obj.GetComponent<Button>().onClick.AddListener(OnButtonClick);
        }

        Normalize();
    }

    void Update()
    {
        Normalize();

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            HideMenu();
        }
    }

    public void OnButtonClick()
    {
        OnButtonClicked = true;
    }

    private void HideMenu()
    {
        Destroy(gameObject);
    }

    private void Normalize()
    {
        RectTransform r_transform = gameObject.GetComponent<RectTransform>();
        r_transform.anchoredPosition = Camera.main.WorldToScreenPoint(place.transform.position);

        Vector3 cMin = Camera.main.WorldToScreenPoint(Vector3.zero);
        Vector3 cMax = Camera.main.WorldToScreenPoint(Singleton.GetObject<Grid>("grid").cellSize * 5);

        cMin -= new Vector3(Camera.main.pixelRect.x, Camera.main.pixelRect.y);
        cMax -= new Vector3(Camera.main.pixelRect.x, Camera.main.pixelRect.y);

        Vector3 delta = (cMax - cMin);
        delta = new Vector3(delta.x / r_transform.rect.width, delta.y / r_transform.rect.height, 0);
        r_transform.localScale = delta;
    }
}
