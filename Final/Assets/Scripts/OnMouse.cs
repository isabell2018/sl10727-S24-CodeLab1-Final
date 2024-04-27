using UnityEngine;

public class OnMouse : MonoBehaviour
{
    public GameObject prefab; // Reference to the prefab to instantiate
    public GameObject prefab2; // Reference to the selected prefab

    private GameObject[,] instantiatedPrefabs; // Reference to the instantiated prefabs

    void Start()
    {
        instantiatedPrefabs = new GameObject[GenerateMap.instance.gridWidth, GenerateMap.instance.gridHeight];
    }

    void OnMouseDown()
    {
        // If there is already a selected item, destroy it
        GameObject existingPrefab2 = GameObject.FindGameObjectWithTag("select");
        if (existingPrefab2 != null)
        {
            Destroy(existingPrefab2);
        }

        // Find existing prefabs at the clicked position and destroy them
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int x = Mathf.RoundToInt(mousePosition.x);
        int y = Mathf.RoundToInt(mousePosition.y);

        if (instantiatedPrefabs[x, y] != null)
        {
            Destroy(instantiatedPrefabs[x, y]);
        }

        // Instantiate selected icon at the clicked position
        if (x >= 0 && x < GenerateMap.instance.gridWidth && y >= 0 && y < GenerateMap.instance.gridHeight)
        {
            instantiatedPrefabs[x, y] = Instantiate(prefab2, new Vector2(x, y), Quaternion.identity);
        }
    }
    

}