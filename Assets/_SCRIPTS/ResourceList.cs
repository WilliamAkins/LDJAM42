using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceList : MonoBehaviour {

    [SerializeField]
    private int CurrentAmountOfRefugees = 0;
    private int MaxAmountOfRefugees = 18;
    private int TotalAmountOfRefugeesSaved = 0;
    
    [SerializeField]
    private int CurrentFoodResources = 200;
    [SerializeField]
    private float FoodTimer = 30;
    private int MaxFoodResources = 200;
    private int FoodCost = 2;
    public int AmountOfFoodAvailableToBuy;

    [SerializeField]
    public float CurrentFuelResources = 100;
    private int MaxFuelResources = 100;
    public GameObject Spawn;
    private int FuelCost = 5;
    public float AmountOfFuelAvailabletoBuy;

    [SerializeField]
    private int CurrentMedicineResources = 0;
    private int MaxMedicineResources = 25;
    private int MedicineCost = 10;
    public int AmountOfMedicineAvailabletoBuy;

    [SerializeField]
    private int CurrentAmountOfGold = 100;
    private int AmountOfGoldToAdd;

    [SerializeField]
    private float SponsorPayDayTimer = 120;

    private int Counter = 0;

    private RefugeeCount refugeeCount;

    public AudioManager FeedMe;

    public MessageHandler mh;

    bool outOfFuel = false;
    private void Start()
    {
        refugeeCount = GameObject.Find("UI").transform.Find("UI_boatSeats").GetComponent<RefugeeCount>();
        FeedMe = GameObject.Find("GameManager").GetComponent<AudioManager>();
        mh = GameObject.Find("UI/Messages").GetComponent<MessageHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckAvailability();
        FoodTimer -= Time.deltaTime;
        if (FoodTimer <= 0)
        {
            mh.AddMessage((CurrentAmountOfRefugees * 2) + " food given to refugees");
            feedTheRefugees();
            FoodTimer = 30;
            //FeedMe.PlayClip(4);
        }
        SponsorPayDayTimer -= Time.deltaTime;
        if (SponsorPayDayTimer <= 0)
        {
            Payday();
            SponsorPayDayTimer = 120;
            mh.AddMessage("You have been paid " + AmountOfGoldToAdd + "!"); 

        }
        if (CurrentAmountOfRefugees >= 1)
            if (Counter >= 3)
            {
                CurrentAmountOfGold += 100;
                Counter = 0;
            }

        if (CurrentFuelResources <= 0 && !outOfFuel)
        {
            Instantiate(Resources.Load("EndGame"));
            outOfFuel = true;
        }
    }


    public void AddRefugee(int RefugeeToAdd)
    {
        CurrentAmountOfRefugees += RefugeeToAdd;

        refugeeCount.updateSeatSprites();
    }

    public int getCurrentFoodResources()
    {
        return CurrentFoodResources;
    }

    public void setCurrentFoodResources(int i)
    {
        CurrentFoodResources += i;
        if (CurrentFoodResources >= MaxFoodResources)
            CurrentFoodResources = MaxFoodResources;
    }

    public void feedTheRefugees()
    {
        CurrentFoodResources -= (CurrentAmountOfRefugees * 2);
    }

    public float getCurrentFuelResources()
    {
        return CurrentFuelResources;
    }

    public void setCurrentFuelResources(float i)
    {
        CurrentFuelResources += i;

        if (CurrentFuelResources >= MaxFuelResources)
            CurrentFuelResources = MaxFuelResources;
    }
    public int getCurrentRemainingSpace()
    {
        return MaxAmountOfRefugees - CurrentAmountOfRefugees;
    }

    public int getCurrentMedicineResources()
    {
        return CurrentMedicineResources;
    }

    public void setCurrentMedicineResources(int i)
    {
        CurrentMedicineResources += i;
        if (CurrentMedicineResources >= MaxMedicineResources)
            CurrentMedicineResources = MaxMedicineResources;
    }

    public float getCurrentFoodTimer()
    {
        return FoodTimer;
    }

    public float getCurrentPaydayTimer()
    {
        return SponsorPayDayTimer;
    }

    public int getCurrentGoldAmount()
    {
        return CurrentAmountOfGold;
    }

    public void setTotalRefugees()
    {
        TotalAmountOfRefugeesSaved += CurrentAmountOfRefugees;
        Counter++;
        CheckAvailability();
        CurrentAmountOfGold += AmountOfGoldToAdd;
        AmountOfGoldToAdd = 0;
        CurrentAmountOfRefugees = 0;

        refugeeCount.resetSeatSprites();
    }

    public int getTotalRefugees()
    {
        return TotalAmountOfRefugeesSaved;
    }

    public void Payday()
    {
        AmountOfGoldToAdd += Random.Range(250, 300);
    }
    
    public void PayForFuel(int quantity)
    {
        while (CurrentAmountOfGold - (FuelCost * quantity) < 0)
            quantity--;
        CurrentAmountOfGold -= (FuelCost * quantity);
        setCurrentFuelResources(quantity);
    }

    public void PayForFood(int quantity)
    {
        while (CurrentAmountOfGold - (FoodCost * quantity) < 0)
            quantity--;
        CurrentAmountOfGold -= (FoodCost * quantity);
        setCurrentFoodResources(quantity);
    }

    public void PayForMedicine(int quantity)
    {
        while (CurrentAmountOfGold - (MedicineCost * quantity) < 0)
            quantity--;

        CurrentAmountOfGold -= (MedicineCost * quantity);
        setCurrentMedicineResources(quantity);
    }

    public void CheckAvailability()
    {
        AmountOfFuelAvailabletoBuy = (MaxFuelResources - CurrentFuelResources);
        AmountOfFoodAvailableToBuy = (MaxFoodResources - CurrentFoodResources);
        AmountOfMedicineAvailabletoBuy = (MaxMedicineResources - CurrentMedicineResources);
    }

    public int getMaxAmountOfRefugees()
    {
        return MaxAmountOfRefugees;
    }

    public int getCurrentAmountOfRefugees()
    {
        return CurrentAmountOfRefugees;
    }
}
