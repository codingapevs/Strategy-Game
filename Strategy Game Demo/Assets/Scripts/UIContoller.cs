using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIContoller : MonoBehaviour {

    public GameObject alertBox;
    public Text nameText;
    public Image image;
    public GameObject production;
    private static UIContoller _instance;
    public static UIContoller Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SpawnUnit()
    {
        BuildingManager.Instance.SpawnUnit();
    }


    public void ShowInfo(Building building)
    {
        ControllerManager.Instance.unitMode = false;
        nameText.text = building.name;
        image.gameObject.SetActive(true);
        image.overrideSprite = building.buildingSprite;
        if(building.spawnUnit != null)
        {
            production.SetActive(true);
            production.transform.Find("Image").GetComponent<Image>().overrideSprite = building.spawnUnit.GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            production.SetActive(false);
        }
    }

    public void ShowAlertBox()
    {
        alertBox.SetActive(true);
        StartCoroutine(Hide(alertBox));

    }

    private IEnumerator Hide(GameObject obj)
    {
        yield return new WaitForSeconds(1);
        obj.SetActive(false);
    }

    
}
