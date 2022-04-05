using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
   NavMeshAgent agent;
   [SerializeField] GameObject target;
   private void Start()
   {
       agent = GetComponent<NavMeshAgent>();

   }

   private void update()
   
   {
     FindTarget();
   }

   private void FindTarget()
   {
       agent.SetDestination(target.transform.position);
   }
}
