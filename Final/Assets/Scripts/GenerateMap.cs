using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenerateMap : MonoBehaviour
{
    public int gridWidth = 6;
    public int gridHeight = 6;

    public GameObject[,] grid;
    public GameObject prefab;

    public Unit unit;
    
    public List<Unit> Units = 
        new List<Unit>();
    public static GenerateMap instance;
    
    private bool isGridGenerated = false;

    public GameObject unitPrefab;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return; // Exit from the Awake method if another instance exists
        }
    }
    
    void Start()
    {
        GenerateList(); 
    }

    public void GenerateList()
    {
        Debug.Log("GenerateList called");
        //Randomly fill the list Units with 36 Unit scriptableObjs (different everytime this function is called.)
        //There are 8 B1 and R1s, 4 B2 and R2s, 2 B3 and R3s, 2 B4 and R4s, 1 B5 and R5s and 1 B6 and R6s.
        List<Unit> availableUnits = new List<Unit>();
        availableUnits = Units;
        
        
        // Shuffle availableUnits list
        for (int i = 0; i < availableUnits.Count; i++)
        {
            Unit temp = availableUnits[i];
            int randomIndex = Random.Range(i, availableUnits.Count);
            availableUnits[i] = availableUnits[randomIndex];
            availableUnits[randomIndex] = temp;
        }

        // Add units to the Units list
        for (int i = 0; i < gridWidth * gridHeight; i++)
        {
            Units[i] = availableUnits[i];
        }
        
        Generate();
    }
    
    
    
    public void Generate()
    {
        Debug.Log("Generate called");
        // Reset grid to null to ensure Generate is called only once
        grid = null;

        // Generate the grid
        grid = new GameObject[gridWidth, gridHeight];

        //fill the grid with prefabs that appear in the corresponding
        //position they occupy in the grid
        int counter = 0;
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                grid[x, y] = Instantiate<GameObject>(unitPrefab);
                grid[x, y].transform.position = new Vector2(x, y);
                grid[x, y].GetComponent<NewUnit>().SetUp(Units[counter]);
                grid[x, y].GetComponent<NewUnit>().posOnGrid = new Vector2Int(x, y);
                
                counter++;
                // grid[x, y] = Instantiate<GameObject>(prefab);
                // grid[x, y].transform.position = new Vector2(x, y);
                //
                // // Set unit properties
                // unit = Units[x * gridHeight + y];
                //
                // // Set the unit of the OnClickOptions script attached to the prefab
                // grid[x, y].GetComponent<OnClickOptions>().SetUnit(unit);
                // unit.pos = new Vector2Int(x, y);
            }
        }
    }

    
}
