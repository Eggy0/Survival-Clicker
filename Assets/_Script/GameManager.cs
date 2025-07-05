using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject win;
    public GameObject gameOver;
    public static GameManager instance;
    public static Manager manager => Manager.instance;

    [Header("Resources")]
    [SerializeField] private uint wood;
    [SerializeField] private uint stone;
    [SerializeField] private uint iron;
    [SerializeField] private uint tools;
    [SerializeField] private uint food;
    [SerializeField] private uint gold;
    [SerializeField] private uint daysElapsed;

    [Header("Civilization settings")]
    [SerializeField] private uint houses; //4 people per 1 house
    [SerializeField] private uint farms; //2 per
    [SerializeField] private uint ironMines; //3 per
    [SerializeField] private uint goldMines; //3 per
    [SerializeField] private uint quarries; //3 per
    [SerializeField] private uint woodcutterHuts; //1 per
    [SerializeField] private uint smithy; //1 per

    [Header("Workers")]
    [SerializeField] private uint farmers; //2 per
    [SerializeField] private uint ironMiners; //3 per
    [SerializeField] private uint goldMiners; //3 per
    [SerializeField] private uint quarryWorkers; //3 per
    [SerializeField] private uint woodcutters; // 1 per
    [SerializeField] private uint blacksmiths; // 1 per
    [SerializeField] private uint unemployed; // 1 per

    [Header("Buttons")]
    [SerializeField] Button houseButton;
    [SerializeField] Button farmButton;
    [SerializeField] Button ironMineButton;
    [SerializeField] Button goldMineButton;
    [SerializeField] Button quarryButton;
    [SerializeField] Button woodcutterButton;
    [SerializeField] Button smithyButton;

    [Header("Text settings - Buildings")]
    [SerializeField] TMP_Text houseText;
    [SerializeField] TMP_Text farmText;
    [SerializeField] TMP_Text ironMineText;
    [SerializeField] TMP_Text goldMineText;
    [SerializeField] TMP_Text quarryText;
    [SerializeField] TMP_Text woodcutterText;
    [SerializeField] TMP_Text smithyText;

    [Header("Text settings - Statistics")]
    [SerializeField] TMP_Text populationText;
    [SerializeField] TMP_Text woodText;
    [SerializeField] TMP_Text stoneText;
    [SerializeField] TMP_Text ironText;
    [SerializeField] TMP_Text goldText;
    [SerializeField] TMP_Text foodText;
    [SerializeField] TMP_Text toolsText;
    [SerializeField] TMP_Text daysText;

     private TMP_Text _houses;
     private TMP_Text _farms;
     private TMP_Text _ironMines;
     private TMP_Text _goldMines;
     private TMP_Text _quarries;
     private TMP_Text _woodcutterHuts;
     private TMP_Text _smithy;

     private TMP_Text _farmers;
     private TMP_Text _ironMiners;
     private TMP_Text _goldMiners;
     private TMP_Text _quarryWorkers;
     private TMP_Text _woodcutters;
     private TMP_Text _blacksmiths; 
     private TMP_Text _unemployed; 
     private TMP_Text _total; 

    private float timer; //General timer
    private float populationTimer; //Timer for rolling new people
    private bool otherStates; //Prevent the pause menu if a different panel is active

    public void ReturnToMenu()
    {
        manager.SwitchScene("MainMenu");
    }

    private void UpdateTime()
    {
        timer += Time.deltaTime;
        populationTimer += Time.deltaTime;

        if (timer >= 10)
        {
            daysElapsed++;
            timer = 0;
            //Production of resources
            ProduceFood(3);
            ProduceWood(5);
            ProduceStone(3);
            ProduceIron(3);
            ProduceGold(2);
            ProduceTools(1);

            //Consume food
            ConsumeFood(2);
            Debug.Log("A day has elapsed");
            if (daysElapsed >= 30)
            {
                Win();
            }
        }

        if (populationTimer >= 3)
        {
            RollPerson(6,4);
            populationTimer = 0;
        }
    }

    private void UpdateText()
    {
        populationText.text = $"Population: {GetTotalPopulation()} / {GetMaxPopulation()}\n    Workers: {GetTotalPopulation()-unemployed}\n    Unemployed: {unemployed}";
        woodText.text = $"Wood: {wood}";
        foodText.text = $"Food: {food}";
        stoneText.text = $"Stone: {stone}";
        ironText.text = $"Iron: {iron}";
        goldText.text = $"Gold: {gold}";
        toolsText.text = $"Tools: {tools}";
        daysText.text = $"Days elapsed: {daysElapsed}";

        houseText.text = $"Houses: {houses}";
        farmText.text = $"Farms: {farms}";
        ironMineText.text = $"Iron Mines: {ironMines}";
        goldMineText.text = $"Gold Mines: {goldMines}";
        quarryText.text = $"Quarries: {quarries}";
        woodcutterText.text = $"Woodcut. Huts: {woodcutterHuts}";
        smithyText.text = $"Smithies: {smithy}";

    }

    private void Win()
    {
        otherStates = true;

        manager.TogglePanel(win);

        _houses = GameObject.Find("House").GetComponent<TMP_Text>();
        _farms = GameObject.Find("WoodcutterHuts").GetComponent<TMP_Text>();
        _ironMines = GameObject.Find("Quarries").GetComponent<TMP_Text>();
        _goldMines = GameObject.Find("IMine").GetComponent<TMP_Text>();
        _quarries = GameObject.Find("GMine").GetComponent<TMP_Text>();
        _woodcutterHuts = GameObject.Find("Smithy").GetComponent<TMP_Text>();
        _smithy = GameObject.Find("Farms").GetComponent<TMP_Text>();

        _farmers = GameObject.Find("Farmers").GetComponent<TMP_Text>();
        _ironMiners = GameObject.Find("IMiner").GetComponent<TMP_Text>();
        _goldMiners = GameObject.Find("GMiner").GetComponent<TMP_Text>();
        _quarryWorkers = GameObject.Find("QuarryWorkers").GetComponent<TMP_Text>();
        _woodcutters = GameObject.Find("Woodcutters").GetComponent<TMP_Text>();
        _blacksmiths = GameObject.Find("Blacksmiths").GetComponent<TMP_Text>();
        _unemployed = GameObject.Find("Unemployed").GetComponent<TMP_Text>();
        _total = GameObject.Find("Total").GetComponent<TMP_Text>();


        _houses.text = houses.ToString();
        _farms.text = farms.ToString();
        _ironMines.text = ironMines.ToString();
        _goldMines.text = goldMines.ToString();
        _quarries.text = quarries.ToString();
        _woodcutterHuts.text = woodcutterHuts.ToString();
        _smithy.text = smithy.ToString();

        _farmers.text = farmers.ToString();
        _ironMiners.text = ironMiners.ToString();
        _goldMiners.text = goldMiners.ToString();
        _quarryWorkers.text = quarryWorkers.ToString();
        _woodcutters.text = woodcutters.ToString();
        _blacksmiths.text = blacksmiths.ToString();
        _unemployed.text = unemployed.ToString();
        _total.text = GetTotalPopulation().ToString();
    }
    private void Lose()
    {
        otherStates = true;
        manager.TogglePanel(gameOver);
    }

    private uint GetTotalPopulation()
    {
        uint totalPopulation = 
            farmers +
            ironMiners +
            goldMiners +
            quarryWorkers +
            woodcutters +
            blacksmiths +
            unemployed;
        return totalPopulation;
}
    private uint GetMaxPopulation()
    {
        uint maxPopulation = houses * 4;
        return maxPopulation;
    }
    /// <summary>
    /// Remove a person
    /// </summary>

    /// <summary>
    /// Roll for a random person
    /// </summary>
    /// <param name="max">Maximum number to roll</param>
    /// <param name="threshold">The threshold that the randomly generated number must meet to make the roll successful</param>
    private void RollPerson(int max, int threshold)
    {
        Debug.Log($"Current population {GetTotalPopulation()}; max population {GetMaxPopulation()}");
        if (GetTotalPopulation() < GetMaxPopulation())
        {
            Debug.Log($"Rolling");
            int rand = UnityEngine.Random.Range(0,max+1);
            if (rand >= threshold)
            {
                Debug.Log("Rolled for a new person");
                unemployed++;
            }
            else
            {
                Debug.Log("Roll failed");
            }
        }
        else
        {
            Debug.Log("Maximum capacity");
        }
    }

    /// <summary>
    /// Population food consumption
    /// </summary>
    /// <param name="foodConsumed">Amount of food consumed by a single person</param>
    /// 
    private void ConsumeFood(uint foodConsumed)
    {
        int remainingFood = (int)food; //To prevent underflowing, use a signed integer for the calculation
        remainingFood -= (int)foodConsumed * (int)GetTotalPopulation();

        if (remainingFood < 0) //Check if remaining food would go into the negatives
        {
            food = 0; //Set this to 0 because we don't want it to underflow
            Lose();
        }
        else
        {
            food = (uint)remainingFood; //Set to this if the check has passed
        }
    }

    private void ProduceFood (uint foodProduced)
    {
        food += foodProduced * (uint)Mathf.RoundToInt(farmers * 1.5f);
        food += (uint)Mathf.RoundToInt((foodProduced * unemployed)/2);
    }

    //TODO: Make "BuildItem" a class
    private bool BuildItem(uint woodCost, uint stoneCost, uint ironCost, uint foodCost, uint assignWorkers, ref uint workerType, ref uint buildingType)
    {
        bool canBuild = wood >= woodCost && stone >= stoneCost && iron >= ironCost && food >= foodCost && unemployed >= assignWorkers;
        if (canBuild)
        {
            wood -= woodCost;
            stone -= stoneCost;
            iron -= ironCost;
            food -= foodCost;
            unemployed -= assignWorkers;
            workerType += assignWorkers;
            buildingType++;

            Debug.Log("Building successful");
        }
        else
        {
            string missingResources = "";

            if (wood < woodCost)
            {
                missingResources += $"{Mathf.Abs(woodCost - wood)} wood";
            }
            if (stone < stoneCost)
            {
                missingResources += $"{Mathf.Abs(stoneCost - stone)} stone";
            }
            if (iron < ironCost)
            {
                missingResources += $"{Mathf.Abs(ironCost - iron)} iron";
            }
            if (food < foodCost)
            {
                missingResources += $"{Mathf.Abs(foodCost - food)} food";
            }
            if (unemployed < assignWorkers)
            {
                missingResources += $"{Mathf.Abs(assignWorkers - unemployed)} available worker(s)";
            }

            Debug.Log($"Not enough resources. You are missing: {missingResources}");
        }
            return canBuild;
    }
    private bool BuildItem(uint woodCost, uint stoneCost, uint ironCost, uint foodCost, ref uint buildingType) //No workers assigned
    {
        bool canBuild = wood >= woodCost && stone >= stoneCost && iron >= ironCost && food >= foodCost;
        if (canBuild)
        {
            wood -= woodCost;
            stone -= stoneCost;
            iron -= ironCost;
            food -= foodCost;
            buildingType++;

            Debug.Log("Building successful");
        }
        else
        {
            string missingResources = "";

            if (wood < woodCost)
            {
                missingResources += $"{Mathf.Abs(woodCost - wood)} wood";
            }
            if (stone < stoneCost)
            {
                missingResources += $"{Mathf.Abs(stoneCost - stone)} stone";
            }
            if (iron < ironCost)
            {
                missingResources += $"{Mathf.Abs(ironCost - iron)} iron";
            }
            if (food < foodCost)
            {
                missingResources += $"{Mathf.Abs(foodCost - food)} food";
            }

            Debug.Log($"Not enough resources. You are missing: {missingResources}");
        }
            return canBuild;
    }

    private void ProduceWood(uint woodProduced)
    {
        wood += woodProduced * woodcutters;
    }
    private void ProduceStone(uint stoneProduced)
    {
        stone += stoneProduced * quarryWorkers;
    }
    private void ProduceIron(uint ironProduced)
    {
        iron += ironProduced * ironMiners;
    }
    private void ProduceGold(uint goldProduced)
    {
        gold += goldProduced * goldMiners;
    }
    private void ProduceTools(uint toolsProduced)
    {
        tools += toolsProduced * blacksmiths;
    }

    public void BuildFarm()
    {
        BuildItem(10, 0, 0, 5, 2, ref farmers, ref farms);
    }
    public void BuildIronMine()
    {
        BuildItem(35, 20, 0, 30, 3, ref ironMiners, ref ironMines);
    }
    public void BuildGoldMine()
    {
        BuildItem(50, 35, 15, 30, 3, ref goldMiners, ref goldMines);
    }
    public void BuildQuarry()
    {
        BuildItem(25, 0, 0, 30, 3, ref quarryWorkers, ref quarries);
    }
    public void BuildWoodcutterHut()
    {
        BuildItem(15, 0, 1, 10, 2, ref woodcutters, ref woodcutterHuts);
    }
    public void BuildSmithy()
    {
        BuildItem(50, 30, 25, 30, 1, ref blacksmiths, ref smithy);
    }
    public void BuildHouse()
    {
        BuildItem(10, 0, 0, 10, ref houses);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Debug.Assert(manager != null, "There is no manager!");
        pausePanel.SetActive(false);

        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !otherStates)
        {
            pausePanel.SetActive(!pausePanel.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.K) && !otherStates)
        {
            Win();
        }

        UpdateTime();
        UpdateText();
    }
}
