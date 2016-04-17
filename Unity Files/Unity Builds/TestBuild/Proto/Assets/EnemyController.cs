using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
    public int damage;
    public int health;
    public float Speed;
    public Animator anim;
    public Transform SpellLocation;
    public GameObject Spell;
    public bool isMoving;
    public bool canAttack;
    public bool isAttacking;
    public bool playerWithInAttackingDistance;
    public float attackDistance;
    public float detectionDistance;
    public GameObject Player;
    public GameObject Gore;
    public GameObject Ragdoll;
    public float distance;
	// Use this for initialization
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        distance = Vector3.Distance(transform.position, Player.transform.position);
        AnimationFunction();
        Attack();
        DetectAndChase();
        if(health <= 0)
        {
            
            Instantiate(Gore, gameObject.transform.position, Gore.transform.rotation);
            Instantiate(Ragdoll, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Weapon")
        {
            health -= 1;
        }
    }
    void DetectAndChase()
    {
        if(distance < detectionDistance)
        {
            gameObject.transform.LookAt(Player.transform);
            if(!isAttacking)
            {
                isMoving = true;
                gameObject.transform.Translate(Vector3.forward * Time.deltaTime * Speed);
            }
            else { isMoving = false; }
        }
        
    }
    void Attack()
    {
        if (distance < attackDistance)
        {
            canAttack = true;
        }
        else canAttack = false;
        if(canAttack && !isAttacking)
        {
            StartCoroutine("AttackSeq");
        }
    }
    IEnumerator AttackSeq() // Destory object in time
    {
        isAttacking = true;
        bool Attacked = false;
        for (float f = 0.0f; f <= 2; f += 0.1f)
        {
            if(f >= 1 && !Attacked)
            {
                if (Spell != null)
                {
                    Instantiate(Spell, SpellLocation.transform.position, transform.rotation);
                }
                Attacked = true;
            }
            yield return new WaitForSeconds(0.1f); // wait for animation to be in the position to do damage.
        }
        isAttacking = false;
    }
    void AnimationFunction()
    {
        if(isAttacking)
        {
            anim.Play("Attack");
        }
        if(isMoving)
        {
            anim.Play("Walk");
            anim.speed = 5;
        }
        else { anim.speed = 1; }
        if(!isMoving && !isAttacking)
        {
            anim.Play("Idle");
        }
    }
}
