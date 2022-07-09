using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimeGirl : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] GameObject player;
    CapsuleCollider Capsulecollider;
    Animator anim;
    Rigidbody rb;
    

    bool shouldFallow;

    private void Awake() 
    {
        agent = GetComponent<NavMeshAgent>();
        Capsulecollider = GetComponent<CapsuleCollider>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");

    } 

    private void Start()
    {
        shouldFallow = true;
    }
   
    private void FixedUpdate()
    {
        if (shouldFallow)
        {
            agent.SetDestination(player.transform.position);
            Vector3 targetPostition = new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z);
            this.transform.LookAt(targetPostition);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Bullet"))
        {
            if(agent.enabled)
            {
                agent.isStopped = true;
                agent.ResetPath();
                shouldFallow = false;
                anim.SetTrigger("Died");
                agent.enabled = false;
                rb.AddForce(Vector3.up * 250);
                Capsulecollider.enabled = false;

                Destroy(gameObject, 5);
            }
            gameObject.tag = "Finish";
        }
            
    }
    

}
