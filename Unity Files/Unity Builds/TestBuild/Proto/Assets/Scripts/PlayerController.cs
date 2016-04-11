using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public bool IsAttacking;
    public bool IsIdle;
    public bool isMoving = true;
    public bool invincable;
    public GameObject Weapon;
    public GameObject Mesh;
    Animator animations;
    public int health;
    public int damaged;
    public int mana;
    public float moveSpeed = 1;
    public float attackTime = 1;
    public float DamageTimer = 1;
    private Collider Hitbox;
    public Collider AttackBox;
    public GameObject[] Spells = new GameObject[4]; // store spell game objects here 
    public float animationWalkSpeed = 2;

    private Vector3 MoveDir;


    void OnStart()
    {
        MoveDir = Vector3.zero;
        animations = Mesh.GetComponent<GameObject>().GetComponent<Animator>();

        Hitbox = GetComponent<Collider>();
    }
    void Update()
    {
        if (animations == null)
        {
            animations = Mesh.GetComponent<Animator>();
        }

        Debug.Log(animations);
        rotation();// Player faces mouse DIR
        animationFunction();
        if (!IsAttacking)
        {
            attackFunction();
            CheckMove();
        }

    }
    void attackFunction()
    {
        if (Input.GetMouseButton(0))
        {
            if (IsAttacking == false)
            {
                IsAttacking = true;
                StartCoroutine("Attack", attackTime);
            }
        }
    }
    void CheckMove()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                MoveDir += Vector3.forward;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                MoveDir += Vector3.forward * -1;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                MoveDir += Vector3.right;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                MoveDir += Vector3.right * -1;
            }
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            if (Input.GetKeyUp(KeyCode.W))
            {
                MoveDir -= Vector3.forward;
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                MoveDir -= Vector3.forward * -1;
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                MoveDir -= Vector3.right;
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                MoveDir -= Vector3.right * -1;
            }
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.W))
            {
                //move up
                gameObject.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.World);
            }
            if (Input.GetKey(KeyCode.S))
            {
                //move Down
                gameObject.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime * -1, Space.World);
            }
            if (Input.GetKey(KeyCode.A))
            {
                //move left
                gameObject.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime * -1, Space.World);
            }
            if (Input.GetKey(KeyCode.D))
            {
                //move up
                gameObject.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.World);
            }

        }
        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W))
        {

            MoveDir = Vector3.zero;
        }
        if (MoveDir != Vector3.zero)
        {
            isMoving = true;
        }
        else isMoving = false;
        Debug.Log(gameObject.transform.rotation.y);
    }
    void animationFunction()
    {
        if (!IsAttacking)
        {
            if (isMoving)
            {
                if (!CheckIfMovingFoward())
                {
                    animations.Play("WalkB");
                    animations.speed = animationWalkSpeed;

                }
                else {
                    animations.Play("Walk");
                    animations.speed = animationWalkSpeed;
                }
                //walking backwards
            }
        }

        if (IsAttacking)
        {
            animations.speed = 3;
            animations.Play("Melee");
        }

        if (!IsAttacking && !isMoving) { animations.Play("Idle"); animations.speed = 1; }

    }
    bool CheckIfMovingFoward()
    {
        if (MoveDir == Vector3.forward)
        {
            if (gameObject.transform.rotation.y < 0)
            {
                return true;
            }
        }
        if (MoveDir == -Vector3.forward)
        {
            if (gameObject.transform.rotation.y > 0)
            {
                return true;
            }
        }
        if (MoveDir == Vector3.right)
        {
            if (gameObject.transform.rotation.y < -0.66f || gameObject.transform.rotation.y > 0.72f)
            {
                return true;
            }
        }
        if (MoveDir == -Vector3.right)
        {
            if (gameObject.transform.rotation.y > -0.66f || gameObject.transform.rotation.y < 0.72f)
            {
                return true;
            }
        }
        return false;

    }
    void OnCollisionEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {

            if (IsAttacking)
            {
                // if enemy is in hit box take damage
            }
        }
        if (other.tag == "Wall")
        {
            // make sure you dont go through wall
        }
        if (other.tag == "Loot")
        {
            // add to inv
        }

    }

    IEnumerator Attack(float attackTime) // Attack
    {
        IsAttacking = true; // do damage
        for (float f = 0f; f <= 0.5f; f += 0.5f)
        {

            yield return new WaitForSeconds(0.5f); // wait for animation to be in the position to do damage.
        }

        IsAttacking = false;


    }

    IEnumerator Damage(float DamageTimer) // Take Damage make invinvible
    {
        invincable = true;
        for (float f = 0f; f <= DamageTimer; f -= 0.1f)
        {

            yield return new WaitForSeconds(0.1f); // can't take damage until timer ends
        }

        invincable = false;
    }
    public void TakeDamage(int damage)
    {
        if (!invincable)
        {
            health -= damage;
            StartCoroutine("Damage", DamageTimer);
        }
        else
        {
            return;
        }
    }
    void rotation() // player faces mouse.
    {
        //rotation
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 5.23f;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, -angle, 0));
    }
    private void spell()
    {

    }
}

