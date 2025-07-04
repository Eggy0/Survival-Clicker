using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject pausePanel;
    public Manager manager => Manager.instance;

    [Header("Resources")]
    [SerializeField] private int wood;
    [SerializeField] private int stone;
    [SerializeField] private int iron;
    [SerializeField] private int tools;
    [SerializeField] private int food;
    [SerializeField] private int gold;
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
    [SerializeField] TMP_Text daysText;

    private float timer;
    private float populationTimer;

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
            RemovePerson();
            //Production of resources
            ProduceFood(5);
            ProduceWood(3);
            ProduceStone(3);
            ProduceIron(3);
            ProduceGold(2);

            //Consume food
            ConsumeFood(1);
            Debug.Log("A day has elapsed");
            if (food <= 0)
            {
                Debug.Log("Everyone has starved. Oops.");
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
        daysText.text = $"Days elapsed: {daysElapsed}";

        houseText.text = $"Houses: {houses}";
        farmText.text = $"Farms: {farms}";
        ironMineText.text = $"Iron Mines: {ironMines}";
        goldMineText.text = $"Gold Mines: {goldMines}";
        quarryText.text = $"Quarries: {quarries}";
        woodcutterText.text = $"Woodcut. Huts: {woodcutterHuts}";
        smithyText.text = $"Smithies: {smithy}";

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
    private void RemovePerson()
    {
        if (GetTotalPopulation() > GetMaxPopulation())
        {
            Debug.Log("Too many people");
            unemployed--;
        }
    }

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
            int rand = Random.Range(0,max+1);
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
    private void ConsumeFood(int foodConsumed)
    {
        food -= foodConsumed * (int)GetTotalPopulation();
    }

    private void ProduceFood (int foodProduced)
    {
        food += foodProduced * (int)farmers;
        food += Mathf.RoundToInt((foodProduced * unemployed)/2);
    }

    //TODO: Make "BuildCost" a class
    private bool BuildCost(int woodCost, int stoneCost, int ironCost, int foodCost)
    {
        bool canBuild = wood >= woodCost && stone >= stoneCost && iron >= ironCost && food >= foodCost;
        if (canBuild)
        {
            wood -= woodCost;
            stone -= stoneCost;
            iron -= ironCost;
            food -= foodCost;
            Debug.Log("Building successful");
        }
        else
        {
            string missingResources = "";

            if (wood < woodCost)
            {
                missingResources += $"{Mathf.Abs(woodCost - wood)} wood; ";
            }
            if (stone < stoneCost)
            {
                missingResources += $"{Mathf.Abs(stoneCost - stone)} stone; ";
            }
            if (iron < ironCost)
            {
                missingResources += $"{Mathf.Abs(ironCost - iron)} iron; ";
            }
            if (food < foodCost)
            {
                missingResources += $"{Mathf.Abs(foodCost - food)} food";
            }

            Debug.Log($"Not enough resources. You are missing: {missingResources}");
        }
            return canBuild;
    }

    private void ProduceWood(int woodProduced)
    {
        wood += woodProduced * (int)woodcutters;
    }
    private void ProduceStone(int stoneProduced)
    {
        stone += stoneProduced * (int)quarryWorkers;
    }
    private void ProduceIron(int ironProduced)
    {
        iron += ironProduced * (int)ironMiners;
    }
    private void ProduceGold(int goldProduced)
    {
        gold += goldProduced * (int)goldMiners;
    }

    public void BuildFarm()
    {
        if (BuildCost(10, 0, 0, 5))
        {
            farms++;
            farmers += 2;
            unemployed -= 2;
        }

    }
    public void BuildIronMine()
    {
        if (BuildCost(35, 20, 0, 30))
        {
            ironMines++;
            ironMiners += 3;
            unemployed -= 3;
        }
    }
    public void BuildGoldMine()
    {
        if (BuildCost(50, 35, 15, 30))
        {
            goldMines++;
            goldMiners += 3;
            unemployed -= 3;
        }
    }
    public void BuildQuarry()
    {
        if (BuildCost(25, 0, 0, 30))
        {
            quarries++;
            quarryWorkers += 3;
            unemployed -= 3;
        }
    }
    public void BuildWoodcutterHut()
    {
        if (BuildCost(15, 0, 1, 10))
        {
            woodcutterHuts++;
            woodcutters += 2;
            unemployed -= 2;
        }
    }
    public void BuildSmithy()
    {
        if (BuildCost(50, 30, 25, 30))
        {
            smithy++;
            blacksmiths++;
            unemployed--;
        }
    }
    public void BuildHouse()
    {
        if (BuildCost(5, 0, 0, 5))
        {
            houses++;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Assert(manager != null, "There is no manager!");
        pausePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausePanel.SetActive(!pausePanel.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            BuildFarm();
        }
        UpdateTime();
        UpdateText();
        //totalPopulation = GetTotalPopulation();
    }
}
