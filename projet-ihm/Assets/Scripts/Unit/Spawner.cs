using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;


public class Spawner : MonoBehaviour
{
    
    public GameObject player;

    [Header("Prefabs")]
    public GameObject soldier;
    public GameObject tank;
    public GameObject plane;

    public GameObject map;

    public void SpawnUnit(int x, int y, int prefabIndex)
    {
        GameObject prefab;
        switch (prefabIndex)
        {
            case 1:
                prefab = soldier;
                break;

            case 2:
                prefab = tank;
                break;

            case 3:
                prefab = plane;
                break;

            // Ajoutez des cas pour d'autres prefabs si nécessaire.

            default:
                Debug.LogError("Invalid prefab index: " + prefabIndex);
                return;
        }
        var unit = Instantiate(prefab, new Vector2(x * 2, y * 2), Quaternion.identity, map.GetComponent<GridManager>().stockUnits.transform);
        unit.GetComponent<UnitScript>().map = map;
        player.GetComponent<GameManager>().playerUnits.Add(unit);
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    
}