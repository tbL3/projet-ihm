using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;


public class Spawner : MonoBehaviour
{
    public int teamNum;
    public GameObject player;

    [Header("Prefabs")]
    public GameObject soldier;
    public GameObject tank;
    public GameObject plane;
    public GameObject reinforcedSoldier;
    public GameObject reinforcedTank;
    public GameObject reinforcedPlane;

    public GridManager map;

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
            case 4:
                prefab = reinforcedSoldier;
                break;

            case 5:
                prefab = reinforcedTank;
                break;

            case 6:
                prefab = reinforcedPlane;
                break;

            // Ajoutez des cas pour d'autres prefabs si nécessaire.

            default:
                Debug.LogError("Invalid prefab index: " + prefabIndex);
                return;
        }
        Debug.Log(prefab.name);
        var unit = Instantiate(prefab, new Vector2(x * 2, y * 2), Quaternion.identity, map.stockUnits.transform);
        unit.GetComponent<UnitScript>().target = unit.transform.position;
        unit.GetComponent<UnitScript>().map = map;
        unit.GetComponent<UnitScript>().currentHealthPoints = unit.GetComponent<UnitScript>().maxHealthPoints;
        unit.GetComponent<UnitScript>().x = x;
        unit.GetComponent<UnitScript>().y = y;
        unit.GetComponent<UnitScript>().tileBeingOccupied = map.GetTileAt(x, y);
        unit.GetComponent<UnitScript>().tileBeingOccupied.GetComponent<Tile>().tileOccupied = true;
        player.GetComponent<GameManager>().playerUnits.Add(unit);
    }
    // Use this for initialization
    void Start()
    {
        if(teamNum == 2)
        {
            SpawnUnit(6, 4, 1);
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    
}