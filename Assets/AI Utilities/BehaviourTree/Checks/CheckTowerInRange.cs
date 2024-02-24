using BehaviourTree;
using UnityEngine;

public class CheckTowerInRange : Node
{
    private Transform transform; 
    private LayerMask enemyLayer = 1 << 7;
    private LayerMask foodLayer = 1 << 9;

    public CheckTowerInRange(Transform transform)
    {
        this.transform = transform;
    }

    public override NODESTATE Evaluate()
    {
        Castle c = GameManager.instance.castle;

        if(!c)
        {
            state = NODESTATE.FAILURE;
            return state;
        }
        else
        {
            if(Vector3.Distance(transform.position, c.transform.position) <= VillagerBT.attackRange)
            {
                state = NODESTATE.SUCCESS;
                return state;
            }
            else
            {
                state = NODESTATE.FAILURE;
                return state;
            }
        }
    }
}
