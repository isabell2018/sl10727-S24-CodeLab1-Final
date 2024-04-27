using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
[CreateAssetMenu
    (
        fileName = "Unit",
        menuName = "Unit",
        order = 0)
]
public class Unit : ScriptableObject
{
    public Vector2Int pos;
    public bool isRevealed = false;
    public GameObject image;
    public bool isSelected = false;

    public UnitType unitType;
    //public bool isRed;
    //public bool isBlank;
    public int rank;
    
    public virtual void Reveal()
    {
        void OnMouseDown()
        {
            isSelected = true;
        }

    }
    
    public void ResetState()
    {
        isSelected = false;
        isRevealed = false;
    }
    
    public enum UnitType
    {
        NONE, RED, BLUE
    }
}