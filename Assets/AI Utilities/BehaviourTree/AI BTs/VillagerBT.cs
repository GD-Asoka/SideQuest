using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerBT : BehaviourTree.Tree
{
    public Transform[] waypoints;

    public static float speed = 1f;
    public static float fovRange = 6f;
    public static float attackRange = 2f;
    public static float cooldown = 10f;
    public bool canAttack, foodTrashed;
    public static VillagerBT instance;
    public static Transform trasher;
    public Villager villager;

    private void Awake()
    {
        if (instance)
            Destroy(instance.gameObject);        
        else
            instance = this;
    }

    private void OnEnable()
    {
        //TrashCounter.onFoodTrashed += FoodTrashedAction;
    }
    
    private void OnDisable()
    {
        //TrashCounter.onFoodTrashed -= FoodTrashedAction;
    }

    protected override void Start()
    {
        base.Start();
        Invoke(nameof(LateInvoke), 1);
    }

    private void LateInvoke()
    {
        Waypoint[] temp = GameManager.instance.shortestPath.waypoints;
        for (int i = 0; i < temp.Length; i++)
        {
            waypoints[i] = temp[i].transform;
        }
    }

    protected override Node SetupTree()
    {
        Node root = new Selector
            (new List<Node>
                {
                    new Sequence
                    (new List<Node>
                        {
                            new CheckTowerInRange(transform),
                            //new task attack
                        }
                    ),
                    new Sequence
                    (new List<Node>
                        {
                            new CheckTowerInRange(transform),
                            new TaskPatrol(transform, waypoints),
                        }
                    ),
                    //new TaskIdle(transform, waypoints),
                }
            ) ;
        
        return root;
    }

    // Custom method to check if the attack is on cooldown
    private NODESTATE CanAttack()
    {
        Debug.Log(canAttack);
        if (canAttack)
        {
            return NODESTATE.SUCCESS;
        }
        else
        {
            return NODESTATE.FAILURE;
        }
    }
    
    private NODESTATE CanMove()
    {
        if (canAttack)
        {
            return NODESTATE.SUCCESS;
        }
        else
        {
            return NODESTATE.FAILURE;
        }
    }
}
