using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isRedTurn = true;
    //public bool isMoving;
    public NewUnit movingUnit;

    public TextMeshProUGUI turnText;
    public int RedUnit = 18;
    public int BlueUnit = 18;

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
        }
    }
    
    // List of all units
    public List<Unit> allUnits = new List<Unit>();

    void Start()
    {
        // Reset the state of each unit
        foreach (Unit unit in allUnits)
        {
            unit.ResetState();
        }
    }

    private void Update()
    {
        //Change bg color and text based on turns
        if (isRedTurn)
        {
            turnText.text = "Red's Turn!";
            Camera.main.backgroundColor = new Color(0.25f,0f,0f,0.1f);
        }
        else
        {
            turnText.text = "Blue's Turn!";
            Camera.main.backgroundColor = new Color(0f,0.125f,0.25f,0.1f);
        }

        if (RedUnit == 1 || BlueUnit == 1)
        {
            
        }
        
        //Cancel selection on mouse right click
        if (Input.GetMouseButton(1))
        {
            
        }
        
        //
        
        
    }
}
