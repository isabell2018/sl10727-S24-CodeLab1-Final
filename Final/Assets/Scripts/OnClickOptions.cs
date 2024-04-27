using UnityEngine;

public class OnClickOptions : MonoBehaviour
{
    public Unit unit;
    private GameObject[,] instantiatedPrefabs; // Reference to the instantiated prefabs

    void Start()
    {
        instantiatedPrefabs = new GameObject[GenerateMap.instance.gridWidth, GenerateMap.instance.gridHeight];
    }

    bool CheckMove(Vector2Int item, Vector2Int target)
    {
        Debug.Log(item+", target:"+target);
        return (item.x == target.x) && (target.y - 1 == item.y || target.y + 1 == item.y) ||
               (item.y == target.y) && (target.x - 1 == item.x || target.x + 1 == item.x);
    }
    
    private void OnMouseDown()
    {
        // // Get the position of the current object
        // Vector3 currentPosition = transform.position;
        // Vector2Int gridPosition = new Vector2Int(Mathf.RoundToInt(currentPosition.x), Mathf.RoundToInt(currentPosition.y));
        //
        // // Store the instantiated prefab
        // GameObject clickedObject = GenerateMap.instance.grid[gridPosition.x, gridPosition.y];
        //
        // if (instantiatedPrefabs[gridPosition.x, gridPosition.y] == null)
        // {
        //     instantiatedPrefabs[gridPosition.x, gridPosition.y] = clickedObject;
        // }
        //
        // // x and y are mouse positions of current prefab
        // Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // int x = Mathf.RoundToInt(mousePosition.x);
        // int y = Mathf.RoundToInt(mousePosition.y);
        //
        // //////////////////FUNCTIONS BEGINS HERE/////////////////////
        //
        // // Get the unit assigned to the clicked object
        //unit = GetComponent<OnClickOptions>().unit;

        NewUnit item = GetComponent<NewUnit>();
        if (!item.isRevealed)
        {
            if(GameManager.instance.movingUnit != null) return;
            Debug.Log("Reveal!");
            item.isRevealed = true;
            item.OnReveal();
            // if (instantiatedPrefabs[x, y] != null)
            // {
            //     Destroy(instantiatedPrefabs[x, y]);
            // }
            // instantiatedPrefabs[x, y] = Instantiate(unit.image, new Vector3(x, y, 0f), Quaternion.identity);
            GameManager.instance.isRedTurn = !GameManager.instance.isRedTurn;
            return;
        }
        else
        {
            if (GameManager.instance.movingUnit == null)
            {
                if (isSameColor(GameManager.instance.isRedTurn, item))
                {
                    // ClearSelection();
                    // unit.isSelected = true;
                    GameManager.instance.movingUnit = item;
                    Debug.Log("selecting unit: " + item.name);
                }
            }
            else
            {
                if (!isSameColor(GameManager.instance.isRedTurn, item))
                {
                    //atacking
                    if (CheckMove(GameManager.instance.movingUnit.posOnGrid, item.posOnGrid))
                    {
                        if (item.isEmpty)
                        {
                            Debug.Log("moving unit: " + GameManager.instance.movingUnit.name);
                            (GameManager.instance.movingUnit.posOnGrid, item.posOnGrid) = (item.posOnGrid, GameManager.instance.movingUnit.posOnGrid);
                            (GameManager.instance.movingUnit.transform.position, item.transform.position) = (item.transform.position, GameManager.instance.movingUnit.transform.position);
                            //move
                            // GenerateMap.instance.grid[unit.pos.x, unit.pos.y] = GameManager.instance.movingUnit.image;
                            // GenerateMap.instance.grid[GameManager.instance.movingUnit.pos.x,
                            //     GameManager.instance.movingUnit.pos.y] = null;//blank image;
                            // GameManager.instance.movingUnit.pos = new Vector2Int(unit.pos.x, unit.pos.y);
                            //
                            // instantiatedPrefabs[unit.pos.x, unit.pos.y] = GameManager.instance.movingUnit.image;
                            // instantiatedPrefabs[GameManager.instance.movingUnit.pos.x,
                            //     GameManager.instance.movingUnit.pos.y] = null;//blank image;
                            // GameManager.instance.movingUnit.image.transform.position = new Vector3(GameManager.instance.movingUnit.pos.x,GameManager.instance.movingUnit.pos.y);
                            //
                            GameManager.instance.isRedTurn = !GameManager.instance.isRedTurn;
                        }
                        else
                        {
                            //moving unit is us, unit is the target
                            if(item.rank == 1 && GameManager.instance.movingUnit.rank == 6)
                            {
                                Debug.Log("6 cant kill 1");
                                //do nothing
                            }
                            else if (item.rank <= GameManager.instance.movingUnit.rank || item.rank == 6 && GameManager.instance.movingUnit.rank == 1)
                            {
                                Debug.Log(GameManager.instance.movingUnit.name+" is killing unit: " + item.name);

                                //kill unit, move to unit pos
                                //destroy the tareget
                                item.Die();
                                (GameManager.instance.movingUnit.posOnGrid, item.posOnGrid) = (item.posOnGrid, GameManager.instance.movingUnit.posOnGrid);
                                (GameManager.instance.movingUnit.transform.position, item.transform.position) = (item.transform.position, GameManager.instance.movingUnit.transform.position);

                                // GenerateMap.instance.grid[unit.pos.x, unit.pos.y] = GameManager.instance.movingUnit.image;
                                // GenerateMap.instance.grid[GameManager.instance.movingUnit.pos.x,
                                //     GameManager.instance.movingUnit.pos.y] = null;//blank image;
                                // GameManager.instance.movingUnit.pos = new Vector2Int(unit.pos.x, unit.pos.y);
                                //
                                // instantiatedPrefabs[unit.pos.x, unit.pos.y] = GameManager.instance.movingUnit.image;
                                // instantiatedPrefabs[GameManager.instance.movingUnit.pos.x,
                                //     GameManager.instance.movingUnit.pos.y] = null;//blank image;
                                // GameManager.instance.movingUnit.image.transform.position = new Vector3(GameManager.instance.movingUnit.pos.x,GameManager.instance.movingUnit.pos.y);
                                //
                                GameManager.instance.isRedTurn = !GameManager.instance.isRedTurn;
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("false movement!");

                    }
                    //GameManager.instance.movingUnit.pos
                }
                GameManager.instance.movingUnit = null;
            }
        }
        /*
        // Get the original unit before destroy
        Unit originalUnit = unit;

        //if the current unit is not selected, else keep it selected
        if (!unit.isSelected)
        {
            unit.isSelected = true;
            //if unrevealed, reveal unrevealed unit and pass the turn
            if (!unit.isRevealed)
            {
                Debug.Log("Reveal!");
                unit.isRevealed = true;
                if (instantiatedPrefabs[x, y] != null)
                {
                    Destroy(instantiatedPrefabs[x, y]);
                }
                //replace the default image with the real unit image stored in unit
                instantiatedPrefabs[x, y] = Instantiate(unit.image, new Vector3(x, y, 0f), Quaternion.identity);
                // Assign the unit variable of the new prefab
                var clickedObjectOnClickOptions = instantiatedPrefabs[x, y].GetComponent<OnClickOptions>();
                if (clickedObjectOnClickOptions != null)
                {
                    clickedObjectOnClickOptions.unit = originalUnit;
                }
                GameManager.instance.isRedTurn = !GameManager.instance.isRedTurn;
            }
            // if revealed, check the unit selected is the same color as the acting side
            else if (isSameColor(GameManager.instance.isRedTurn, unit))
            {
                //when no other units selected and this unit is not blank, select this revealed unit
                if ((SearchSelection() == new Vector2Int(-1, -1)) && !unit.isBlank)
                {
                    ClearSelection();
                    unit.isSelected = true;
                }
                //else when no other units selected and this unit is blank, clear all selection
                else if ((SearchSelection() == new Vector2Int(-1, -1)) && unit.isBlank)
                {
                    ClearSelection();
                }
                //now there's already a self unit selected before, named A
                //and now we select this unit named B
                //we're ready to execute B's actions compared to A
                else
                {
                    //get variables of A
                    Vector2Int otherSelectionPosition = SearchSelection();
                    int i = otherSelectionPosition.x;
                    int j = otherSelectionPosition.y;
                    Unit selectedUnit = instantiatedPrefabs[i, j].GetComponent<OnClickOptions>().unit;

                    //if B is not next to A, clear selection.
                    if (!isNextTo(x, y, i, j))
                    {
                        ClearSelection();
                    }
                    //now B is a unit next to A
                    else
                    {
                        //if B is blank next A, move: A becomes blank and B = A. Pass turn.
                        if (unit.isBlank)
                        {
                            unit = selectedUnit;
                            selectedUnit.isBlank = true;
                            selectedUnit.image = blankImage;
                            ClearSelection();
                            GameManager.instance.isRedTurn = !GameManager.instance.isRedTurn;
                        }
                        //now B is a non-blank unit next to A
                        else
                        {
                            //if B is the same color unit as A, clear selection.
                            if (unit.isRed == selectedUnit.isRed)
                            {
                                ClearSelection();
                            }
                            //now B is a different color unit next to A
                            else
                            {
                                //if B is a higher rank unit next to A,
                                //except that A is rank 6 and B is rank 1,
                                //clear selection.
                                if ((unit.rank > selectedUnit.rank) || ((unit.rank != 6) && (selectedUnit.rank != 1)))
                                {
                                    ClearSelection();
                                }
                                //else B is replaceable by A.
                                //B = A, and A = blank. Pass turn.
                                else
                                {
                                    unit = selectedUnit;
                                    selectedUnit.isBlank = true;
                                    selectedUnit.image = blankImage;
                                    ClearSelection();
                                    GameManager.instance.isRedTurn = !GameManager.instance.isRedTurn;
                                }
                            }
                        }
                    }
                }
            }
            //when the unit selected is not the same color as the acting side, clear selection
            else
            {
                ClearSelection();
            }
        }*/
    }

    //clear all selections on map; all isSelected = false
    public void ClearSelection()
    {
        Debug.Log("ClearSelectionCalled");
        for (int x = 0; x < GenerateMap.instance.gridWidth; x++)
        {
            for (int y = 0; y < GenerateMap.instance.gridHeight; y++)
            {
                if (instantiatedPrefabs[x, y] != null)
                {
                    instantiatedPrefabs[x, y].GetComponent<OnClickOptions>().SetSelected(false);
                }
            }
        }
    }

    //search if there are one selection on map; return its position or (-1,-1) if null
    public Vector2Int SearchSelection()
    {
        Debug.Log("SearchSelectionCalled");
        for (int x = 0; x < GenerateMap.instance.gridWidth; x++)
        {
            for (int y = 0; y < GenerateMap.instance.gridHeight; y++)
            {
                if (instantiatedPrefabs[x, y] != null && instantiatedPrefabs[x, y].GetComponent<OnClickOptions>().IsSelected())
                {
                    return new Vector2Int(x, y);
                }
            }
        }
        return new Vector2Int(-1, -1); // Return (-1, -1) if no selection is found
    }

    //check if the unit selected is from the current operating color/side
    public bool isSameColor(bool isRedTurn, NewUnit unit)
    {
        Debug.Log("IsSameColorCalled");
        if (isRedTurn)
        {
            return unit.unitType == Unit.UnitType.RED;
        }
        else
        {
            return unit.unitType == Unit.UnitType.BLUE;
        }
    }

    public bool isNextTo(int x, int y, int i, int j)
    {
        Debug.Log("isNextToCalled");
        //A is to the left of B
        if ((i == x - 1) && (j == y))
        {
            return true;
        }
        //right
        else if ((i == x + 1) && (j == y))
        {
            return true;
        }
        //up
        else if ((i == x) && (j == y + 1))
        {
            return true;
        }
        //down
        else if ((i == x) && (j == y - 1))
        {
            return true;
        }

        return false;
    }

    public void SetUnit(Unit _unit)
    {
        unit = _unit;
    }

    public Unit GetUnit()
    {
        return unit;
    }

    public void SetSelected(bool selected)
    {
        unit.isSelected = selected;
    }

    public bool IsSelected()
    {
        return unit.isSelected;
    }
}
