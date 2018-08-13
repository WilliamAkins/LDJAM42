using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class resourceManager : MonoBehaviour {
    [SerializeField]
    GameObject UICanvas;

    [SerializeField]
    ResourceList boat;

    TextMeshProUGUI space;
    TextMeshProUGUI fuel;
    TextMeshProUGUI medicine;
    TextMeshProUGUI food;
    TextMeshProUGUI foodTimer;
    TextMeshProUGUI refugeesSaved;
    TextMeshProUGUI PaydayTimer;
    TextMeshProUGUI Gold;
    TextMeshProUGUI SavedPercent;

    private SpawnRefugees spawnRefugees;



    // Use this for initialization
    void Start () {
		if(UICanvas == null)
            UICanvas = GameObject.Find("UI");

        if (boat == null)
            boat = GameObject.FindGameObjectWithTag("boat").GetComponent<ResourceList>();

        space = UICanvas.transform.Find("UI_boatSeats/Seating").GetComponent<TextMeshProUGUI>();
        fuel = UICanvas.transform.Find("TopBar/Fuel").GetComponent<TextMeshProUGUI>();
        medicine = UICanvas.transform.Find("TopBar/Medicine").GetComponent<TextMeshProUGUI>();
        food = UICanvas.transform.Find("TopBar/Food").GetComponent<TextMeshProUGUI>();
        //foodTimer = UICanvas.transform.Find("Stats/FoodTimer").GetComponent<TextMeshProUGUI>();
        //refugeesSaved = UICanvas.transform.Find("Stats/RefugeesSaved").GetComponent<TextMeshProUGUI>();
        //PaydayTimer = UICanvas.transform.Find("Stats/PaydayTimer").GetComponent<TextMeshProUGUI>();
        Gold = UICanvas.transform.Find("TopBar/Currency").GetComponent<TextMeshProUGUI>();

        SavedPercent = UICanvas.transform.Find("TopBar/SavedPercent").GetComponent<TextMeshProUGUI>();

        spawnRefugees = GameObject.Find("GameManager").GetComponent<SpawnRefugees>();
    }

    // Update is called once per frame 
    void Update () {
        float percentVal = Mathf.Floor(((float)boat.getTotalRefugees() / (float)spawnRefugees.getSpawnedRefugees()) * 100);

        space.text = boat.getCurrentAmountOfRefugees() + "/" + boat.getMaxAmountOfRefugees();
        fuel.text = boat.getCurrentFuelResources().ToString("0.0");
        medicine.text = boat.getCurrentMedicineResources().ToString("0.0");
        food.text = boat.getCurrentFoodResources().ToString("0.0");
        //foodTimer.text = boat.getCurrentFoodTimer().ToString("0.0");
        //refugeesSaved.text = boat.getTotalRefugees().ToString("0");
        //PaydayTimer.text = boat.getCurrentPaydayTimer().ToString("0.0");
        Gold.text = boat.getCurrentGoldAmount().ToString("0");

        SavedPercent.text = percentVal + "%";
    }
}
