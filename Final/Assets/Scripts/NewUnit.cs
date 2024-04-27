using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class NewUnit : MonoBehaviour
{
    public Vector2Int posOnGrid;
    public bool isRevealed;
    //public Sprite unRevealed, blank;
    public SpriteRenderer cover;
    public Unit.UnitType unitType;
    public int rank;

    public bool isEmpty;
    // Start is called before the first frame update
    void Start()
    {
        isRevealed = false;
        cover.color = Color.gray;
    }
    
    public void SetUp(Unit unit)
    {
        unitType = unit.unitType;
        rank = unit.rank;
        GetComponent<SpriteRenderer>().color = unit.image.GetComponent<SpriteRenderer>().color;
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite =
            unit.image.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
    }
    public void OnReveal()
    {
        isRevealed = true;
        cover.enabled = false;
    }
    
    public void Die()
    {
        isEmpty = true;
        cover.enabled = true;
        cover.color = Color.white;
        unitType = Unit.UnitType.NONE;
    }
}
