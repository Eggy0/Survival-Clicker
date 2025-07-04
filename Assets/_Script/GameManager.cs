using UnityEngine;

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
    [SerializeField] private int daysElapsed;

    [Header("Civilization settings")]
    [SerializeField] private int houses; //4 people per 1 house
    [SerializeField] private int farms; //2 per
    [SerializeField] private int ironMines; //3 per
    [SerializeField] private int goldMines; //3 per
    [SerializeField] private int quarries; //3 per
    [SerializeField] private int woodcutterHuts; //1 per
    [SerializeField] private int smithingShop; //1 per


    [SerializeField] private int farmers; //2 per
    [SerializeField] private int ironMiners; //3 per
    [SerializeField] private int goldMiners; //3 per
    [SerializeField] private int quarryWorkers; //3 per
    [SerializeField] private int woodcutters; // 1 per
    [SerializeField] private int blacksmiths; // 1 per
    [SerializeField] private int unemployed; // 1 per


    private float timer;
    private float populationTimer;
    [SerializeField] private int totalPopulation;

    public void ReturnToMenu()
    {
        manager.SwitchScene("MainMenu");
    }

    private void UpdateTime()
    {
        timer += Time.deltaTime;
        populationTimer += Time.deltaTime;

        if (timer >= 60)
        {
            daysElapsed++;
            timer = 0;
            if (daysElapsed % 3 == 0 && daysElapsed > 0 /* because zero divided by a number equals zero*/)
            {
                RemovePerson();
            }
            ProduceFood(5);
            ConsumeFood(1);
            Debug.Log("A day has elapsed");
        }

        if (populationTimer >= 20)
        {
            RollPerson(6,4);
            populationTimer = 0;
        }
    }

    private int GetTotalPopulation()
    {
        int totalPopulation = 
            farmers +
            ironMiners +
            goldMiners +
            quarryWorkers +
            woodcutters +
            blacksmiths +
            unemployed;
        return totalPopulation;
}

    /// <summary>
    /// Population food consumption
    /// </summary>
    /// <param name="foodConsumed">Amount of food consumed by a single person</param>
    /// 
    private void ConsumeFood(int foodConsumed)
    {
        food -= foodConsumed * totalPopulation;
    }

    private void ProduceFood (int foodProduced)
    {
        food += foodProduced * farmers;
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
        wood += woodProduced * woodcutters;
    }
    private void ProduceStone(int stoneProduced)
    {
        stone += stoneProduced * quarryWorkers;
    }
    private void ProduceIron(int ironProduced)
    {
        iron += ironProduced * ironMiners;
    }
    private void ProduceGold(int goldProduced)
    {
        gold += goldProduced * goldMiners;
    }

    private void BuildFarm()
    {
        if (BuildCost(10, 0, 0, 5))
        {
            farms++;
            farmers += 2;
            unemployed -= 2;
        }

    }
    private void BuildIronMine()
    {
        if (BuildCost(35, 20, 0, 30))
        {
            ironMines++;
            ironMiners += 3;
            unemployed -= 3;
        }
    }
    private void BuildGoldMine()
    {
        if (BuildCost(50, 35, 15, 30))
        {
            goldMines++;
            goldMiners += 3;
            unemployed -= 3;
        }
    }
    private void BuildQuarry()
    {
        if (BuildCost(25, 0, 0, 30))
        {
            quarries++;
            quarryWorkers += 3;
            unemployed -= 3;
        }
    }
    private void BuildWoodcutterHut()
    {
        if (BuildCost(15, 0, 1, 10))
        {
            woodcutterHuts++;
            woodcutters++;
            unemployed--;
        }
    }
    private void BuildSmithingShop()
    {
        if (BuildCost(50, 30, 25, 30))
        {
            smithingShop++;
            blacksmiths++;
            unemployed--;
        }
    }
    private int GetMaxPopulation()
    {
        int maxPopulation = houses * 4;
        Debug.Log($"Max population {maxPopulation}");
        return maxPopulation;
    }
    /// <summary>
    /// Remove a person
    /// </summary>
    private void RemovePerson()
    {
        if (totalPopulation > GetMaxPopulation())
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
        if (totalPopulation <= GetMaxPopulation())
        {
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
        totalPopulation = GetTotalPopulation();
    }
}
