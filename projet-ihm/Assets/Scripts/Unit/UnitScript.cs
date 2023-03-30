using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering;
using Unity.VisualScripting;

public class UnitScript : MonoBehaviour
{
    public int teamNum;
    public int x;
    public int y;
    public GridManager map;
    
    //Meta defining play here
    public Queue<int> movementQueue;
    public Queue<int> combatQueue;
    //This global variable is used to increase the units movementSpeed when travelling on the board

    public float visualMovementSpeed;
    public List<Node> currentPath = null;
    public List<Node> attackPath = null;

    public UnitScript enemy;
    public bool shouldAttack;
    public bool canAttack;
    //Animator
    public Animator animator;

    public Vector2 target;
    private float t;
    public Tile tileBeingOccupied;
    public Tile previousTile;

    public GameObject damagedParticle;
    //UnitStats
    public string unitName;
    public int moveRange = 2;
    public float remainingMove;
    public int attackRange = 1;
    public int attackDamage = 1;
    public int maxHealthPoints = 5;
    public int currentHealthPoints;
    public Sprite unitSprite;


    [Header("UI Elements")]
    //Unity UI References
    public Canvas healthBarCanvas;
    public TMP_Text hitPointsText;
    public Image healthBar;

    public Canvas damagePopupCanvas;
    public TMP_Text damagePopupText;
    public Image damageBackdrop;

    public GameObject RangeCollider;


    //This may change in the future if 2d sprites are used instead
    //public Material unitMaterial;
    //public Material unitWaitMaterial;

    //public tileMapScript map;
    public float moveSpeedTime = 10f;


    //Enum for unit states
    public enum movementStates
    {
        Unselected,
        Selected,
        Moved,
        Wait
    }
    public movementStates unitMoveState;

    public static Dictionary<int, Object> unitTypes = new Dictionary<int, Object>();
    //Pathfinding

    //public List<Node> path = null;

    //Path for moving unit's transform
    //public List<Node> pathForMovement = null;
    public bool completedMovement = false;

    private void Start()
    {


    }

    private void Awake()
    {
        RangeCollider = GameObject.Find("MoveRange");

        RangeCollider.GetComponent<BoxCollider2D>().size = new Vector2(2 + 4 * moveRange, 2 + 4 * moveRange);
    }

    private void Update()
    {
        t += Time.deltaTime / moveSpeedTime;
        transform.position = Vector2.MoveTowards(transform.position, target, moveSpeedTime);
        if (currentPath != null)
        {

            int currNode = 0;

            while (currNode < currentPath.Count - 1)
            {

                Vector2 start = map.TileCoordToWorldCoord(currentPath[currNode].x, currentPath[currNode].y);
                Vector2 end = map.TileCoordToWorldCoord(currentPath[currNode + 1].x, currentPath[currNode + 1].y);

                Debug.DrawLine(start, end, Color.red);

                currNode++;
                
            }
            
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null && map.selectedUnit == this)
            {
                
                enemy = hit.collider.gameObject.GetComponent<UnitScript>();                          
                if (enemy != null && enemy.teamNum != teamNum)
                {
                    shouldAttack = true;                    
                }
                else
                {
                    enemy = null;
                }
            }
        }
        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape))
        {
            map.selectedUnit = null;
        }
        MoveNextTile();
        Attack(enemy);
    }

    public void MoveToAttack(int x, int y)
    {
        attackPath = new List<Node>();
        map.GeneratePathTo(x, y);
        
        attackPath.AddRange(currentPath.GetRange(currentPath.Count - attackRange, attackRange));
        currentPath.RemoveRange(currentPath.Count - attackRange, attackRange);
        foreach (Node node in currentPath)
        {
            Debug.Log(node.toString());
        }

    }
    public void Attack(UnitScript enemy)
    {
        if (shouldAttack == true && enemy != null && canAttack == true)
        {            
            MoveToAttack(enemy.x, enemy.y);
            int distance = attackPath.Count;
    
            if (distance <= attackRange && currentPath.Count == 1)
            {
                if (this != enemy)
                {
                    enemy.currentHealthPoints -= attackDamage;
                }
                if (enemy.currentHealthPoints <= 0)
                {
                    Destroy(enemy.gameObject);
                }
            }
            
        }
        canAttack = false;
        shouldAttack = false;
    }

    private void OnMouseDown()
    {
        if(map.selectedUnit == null || map.selectedUnit.teamNum == teamNum)
        {
            // Faites quelque chose avec l'objet cliqué
            Debug.Log("Objet cliqué : " + gameObject);
            map.selectedUnit = this;
            map.showUnitRange();
            
        }
    }
    public void OnNewTurn()
    {
        Debug.Log("Click");
        remainingMove = moveRange;
        if(enemy != null)
        {
            canAttack = true;
            shouldAttack = true;
        }
        else
        {
            canAttack = true;
            shouldAttack = false;
        }
    }
    public void MoveNextTile()
    {
        while (remainingMove > 0 && currentPath != null)
        {
            if (currentPath.Count == 1)
            {
                // We only have one tile left in the path, and that tile MUST be our ultimate
                // destination -- and we are standing on it!
                // So let's just clear our pathfinding info.
                currentPath = null;
            }

            if (currentPath == null)
                return;

            // Get cost from current tile to next tile
            
            remainingMove -= map.CostToEnterTile(currentPath[0].x, currentPath[0].y, currentPath[1].x, currentPath[1].y);
            
            
            // Move us to the next tile in the sequence
            x = currentPath[1].x;
            y = currentPath[1].y;
            
            // Remove the old "current" tile
            currentPath.RemoveAt(0);
            
        }
        target = new Vector2(x * 2, y * 2);
        
    }



    /*public void LateUpdate()
    {
        healthBarCanvas.transform.forward = Camera.main.transform.forward;
        //damagePopupCanvas.transform.forward = Camera.main.transform.forward;
        holder2D.transform.forward = Camera.main.transform.forward;
    }
    
    public void MoveNextTile()
    {
        if (path.Count == 0)
        {
            return;
        }
        else
        {
            StartCoroutine(moveOverSeconds(transform.gameObject, path[path.Count - 1]));
        }

    }


    public void moveAgain()
    {

        path = null;
        setMovementState(0);
        completedMovement = false;
        gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;
        setIdleAnimation();
        //gameObject.GetComponentInChildren<Renderer>().material = unitMaterial;
    }
    */
    public movementStates getMovementStateEnum(int i)
    {
        if (i == 0)
        {
            return movementStates.Unselected;
        }
        else if (i == 1)
        {
            return movementStates.Selected;
        }
        else if (i == 2)
        {
            return movementStates.Moved;
        }
        else if (i == 3)
        {
            return movementStates.Wait;
        }
        return movementStates.Unselected;

    }
    public void setMovementState(int i)
    {
        if (i == 0)
        {
            unitMoveState = movementStates.Unselected;
        }
        else if (i == 1)
        {
            unitMoveState = movementStates.Selected;
        }
        else if (i == 2)
        {
            unitMoveState = movementStates.Moved;
        }
        else if (i == 3)
        {
            unitMoveState = movementStates.Wait;
        }


    }
    public void updateHealthUI()
    {
        healthBar.fillAmount = (float)currentHealthPoints / maxHealthPoints;
        hitPointsText.SetText(currentHealthPoints.ToString());
    }
    public void dealDamage(int x)
    {
        currentHealthPoints = currentHealthPoints - x;
        updateHealthUI();
    }
    public void wait()
    {

        gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.gray;
        //gameObject.GetComponentInChildren<Renderer>().material = unitWaitMaterial;
    }
    public void changeHealthBarColour(int i)
    {
        if (i == 0)
        {
            healthBar.color = Color.blue;
        }
        else if (i == 1)
        {

            healthBar.color = Color.red;
        }
    }

    public IEnumerator checkIfRoutinesRunning()
    {
        while (combatQueue.Count > 0)
        {

            yield return new WaitForEndOfFrame();
        }

        Destroy(gameObject);

    }
    public IEnumerator fadeOut()
    {

        combatQueue.Enqueue(1);
        //setDieAnimation();
        //yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Renderer rend = GetComponentInChildren<SpriteRenderer>();

        for (float f = 1f; f >= .05; f -= 0.01f)
        {
            Color c = rend.material.color;
            c.a = f;
            rend.material.color = c;
            yield return new WaitForEndOfFrame();
        }
        combatQueue.Dequeue();


    }



    public IEnumerator displayDamageEnum(int damageTaken)
    {

        combatQueue.Enqueue(1);

        damagePopupText.SetText(damageTaken.ToString());
        damagePopupCanvas.enabled = true;
        for (float f = 1f; f >= -0.01f; f -= 0.01f)
        {

            Color backDrop = damageBackdrop.GetComponent<Image>().color;
            Color damageValue = damagePopupText.color;

            backDrop.a = f;
            damageValue.a = f;
            damageBackdrop.GetComponent<Image>().color = backDrop;
            damagePopupText.color = damageValue;
            yield return new WaitForEndOfFrame();
        }

        //damagePopup.enabled = false;
        combatQueue.Dequeue();

    }
    /*public void resetPath()
    {
        path = null;
        completedMovement = false;
    }*/
    public void displayDamage(int damageTaken)
    {
        damagePopupCanvas.enabled = true;
        damagePopupText.SetText(damageTaken.ToString());
    }
    public void disableDisplayDamage()
    {
        damagePopupCanvas.enabled = false;
    }

    public void setSelectedAnimation()
    {

        animator.SetTrigger("toSelected");
    }
    public void setIdleAnimation()
    {
        animator.SetTrigger("toIdle");
    }
    public void setWalkingAnimation()
    {
        animator.SetTrigger("toWalking");
    }

    public void setAttackAnimation()
    {
        animator.SetTrigger("toAttacking");
    }
    public void setWaitIdleAnimation()
    {

        animator.SetTrigger("toIdleWait");
    }

    public void setDieAnimation()
    {
        animator.SetTrigger("dieTrigger");
    }
}