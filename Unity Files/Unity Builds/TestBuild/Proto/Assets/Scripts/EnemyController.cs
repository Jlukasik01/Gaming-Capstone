using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
    public int baseDamage;
    public int damage;
    public int baseHealth;
    public int health;
    public float Speed;
    public float attackDelay = 1;
    public int soulValue;
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
    private GameObject lootTable;
    public BoxCollider MeleeCollider;
    public bool meleeOn = false;
    public bool canMoveAndAttack = false;
    public float attackPause = 0;
    public bool canMove = true;
    bool AttackPaused;
    public float attackTime= 2f;
    public bool secondaryAttack = false;
    public GameObject SecondaryAttackLoc;
    public float SecondaryDistance = 0;
    public bool AttackingSecondary= false;
    public int arrayIndex; //spot where it appears on the EnemySpawnerController array. Only 1 enemy can have 1 number. Lower the number, lower level it starts to spawn at. Only needs to be applied to enemies in Resources/EnemiesToLoad

	// Use this for initialization
	void Start () {
        AttackPaused = false;
        meleeOn = false;
        Player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponentInChildren<Animator>();
        lootTable = GameObject.FindGameObjectWithTag("LootTable");
        if (Spell == null)
        { MeleeCollider = GetComponent<BoxCollider>(); }

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
            Instantiate(lootTable.GetComponent<LootController>().dropItem(), transform.position, transform.rotation);
            Player.GetComponent<PlayerController>().souls += soulValue; 
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Weapon")
        {
            health -= Player.GetComponent<IntController>().Weapon.GetComponent<WeaponController>().damage;
        }
        
    }
    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if(MeleeCollider != null && meleeOn)
            {
                other.GetComponent<PlayerController>().health -= damage;
                meleeOn = false;
            }
        }
    }
    void takeDamage(int damage)
    {
        health -= damage;
    }
    void DetectAndChase()
    {
        if(distance < detectionDistance)
        {
            gameObject.transform.LookAt(Player.transform);
            if((canMove && !isAttacking )|| (canMoveAndAttack && canMove) && !AttackPaused)
            {
                isMoving = true;
                gameObject.transform.Translate(Vector3.forward * Time.deltaTime * Speed);
            }
            else { isMoving = false;
               gameObject.transform.Translate(transform.position*0);
            }
        }
        
    }

    void Attack()
    {
        if (distance < attackDistance && !AttackPaused)
        {
            canAttack = true;
        }
        else canAttack = false;
        if(canAttack && !isAttacking)
        {
            if(distance<SecondaryDistance && secondaryAttack)
            {
                StartCoroutine("secondaryAttackSeq", attackDelay);
            }
            else StartCoroutine("AttackSeq", attackDelay);
        }
    }

    IEnumerator AttackSeq(float AttackDelay) // Destory object in time
    {
        isAttacking = true;
        bool Attacked = false;
        for (float f = 0.0f; f <= 2; f += 0.1f)
        {
            if(canMoveAndAttack)
            {
                meleeOn = true;
            }
            if (f >= attackDelay && !Attacked)
            {
                if (Spell != null) // cast spell if spell
                {
                    Instantiate(Spell, SpellLocation.transform.position, SpellLocation.transform.rotation);
                }
                else // else do melee
                {
                    meleeOn = true;
                }
                Attacked = true;
            }
            else { if (!canMoveAndAttack) { meleeOn = false; } } // cancel melee hitbox if enemy meleed

            yield return new WaitForSeconds(0.1f); // wait for animation to be in the position to do damage.
        }
        StartCoroutine("attackPuaseTimer", attackPause);
        isAttacking = false;
    }
    IEnumerator attackPuaseTimer(float attackPauseTime)
    {

        anim.Play("Idle");
        anim.speed = 1;
        for (float f = 0.0f; f <= attackPauseTime; f+= 0.1f)
        {
            AttackPaused = true;
            canAttack = false;
            canMove = false;
            isMoving = false;
            yield return new WaitForSeconds(0.1f); 
        }
        AttackPaused = false;
        canAttack = true;
        canMove = true;
    }
    IEnumerator secondaryAttackSeq(float AttackDelay) // Destory object in time
    {
        isAttacking = true;
        AttackingSecondary = true;
        bool Attacked = false;
        for (float f = 0.0f; f <= 2; f += 0.1f)
        {
            if(!Attacked)
            {
                SecondaryAttackLoc.GetComponent<SecondaryAttackController>().isActive = true;
                SecondaryAttackLoc.GetComponent<SecondaryAttackController>().callSpell();
                Attacked = true;
            }
            yield return new WaitForSeconds(0.1f); // wait for animation to be in the position to do damage.
        }
        AttackingSecondary = false;
        StartCoroutine("attackPuaseTimer", attackPause);
        isAttacking = false;
    }
    void AnimationFunction()
    {
        if(isAttacking && !AttackingSecondary)
        {
            anim.Play("Attack");
            if(canMoveAndAttack)
            {
                anim.speed = 5;
            }
            else { anim.speed = 1; }
        }
        if(isMoving && !isAttacking)
        {
            anim.Play("Walk");
            anim.speed = 5;
        }
        
        if((!isMoving && !isAttacking) || !canMove)
        {
            anim.Play("Idle");
            anim.speed = 1;
        }
        if(AttackingSecondary)
        {
            anim.Play("MeleeAttack");
            anim.speed = 1;
        }
    }
}
