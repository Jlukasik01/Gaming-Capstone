using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public bool IsAttacking;
    public bool IsIdle;
    public bool isMoving = true;
    public bool isSprinting;
    public bool invincable;
    public GameObject Weapon;
    public GameObject []Mesh;
    public int ActiveMesh =1;
    Animator animations;
    public int health;
    public int maxHealth;
    public int damaged;
    public int mana;
    public int maxMana;
    public float stamina;
    public float maxStamina;
    public int defense; //how much extra/less the player takes when hit
    public int spellDamage; //how much extra the player's spells hit
    public int weaponDamage; //how much extra the player's weapons hit
    public int manaRegenRate; //how fast the player's mana regenerates
    public int staminaRegenRate; //how fast the player's stamina regenerates every .1 seconds
    public bool regeneratingManaStamina;
    public float moveSpeed = 1;
    public float sprintSpeedMultiplier = 2;
    public float attackTime = 1;
    public float DamageTimer = 1;
    public int souls;
    private Collider Hitbox;
    public Collider AttackBox;
    public GameObject[] Spells = new GameObject[4]; // store spell game objects here 
    public float animationWalkSpeed = 2;
    private Vector3 MoveDir;
    public int[] LevelUpCosts;
    

    void OnStart()
    {
        MoveDir = Vector3.zero;
        animations = Mesh[ActiveMesh].GetComponent<GameObject>().GetComponent<Animator>();
        Hitbox = GetComponent<Collider>();
        maxHealth = health;
    }

    void Update()
    {
        Weapon = GetComponent<IntController>().Weapon;
        if (animations == null || animations != Mesh[ActiveMesh].GetComponent<Animator>())
        {
            animations = Mesh[ActiveMesh].GetComponent<Animator>();
        }
        //Debug.Log(animations);
        rotation();// Player faces mouse DIR
        animationFunction();
        if (!IsAttacking)
        {
            attackFunction();
            CheckMove();
        }

        if (health > maxHealth)
        {
            health = maxHealth;
        }

        if (regeneratingManaStamina == true)
        {
            mana += manaRegenRate;
            stamina += staminaRegenRate;
            if (mana > maxMana)
            {
                mana = maxMana;
            }
            if(stamina > maxStamina)
            {
                stamina = maxStamina;
            }
            StartCoroutine("Regen");
        }

        LevelUpMesh();
        sprintFunction();
    }

    public void equip() { Weapon = GetComponent<IntController>().Weapon; }

    void sprintFunction()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && stamina > 0)
        {
            isSprinting = true;
            moveSpeed = moveSpeed * sprintSpeedMultiplier;
            animationWalkSpeed = animationWalkSpeed * sprintSpeedMultiplier;
        }
        if ((Input.GetKeyUp(KeyCode.LeftShift) && isSprinting) || (stamina <= 0 && isSprinting))
        {
            isSprinting = false;
            moveSpeed = moveSpeed / sprintSpeedMultiplier;
            animationWalkSpeed = animationWalkSpeed / sprintSpeedMultiplier;
        }
        if (isSprinting)
        {
            stamina -= Time.deltaTime * 0.5f;
        }
        else
        { stamina += Time.deltaTime * 0.2f; }
        if(stamina > maxStamina)
        {
            stamina = maxStamina;
        }

    }
    void LevelUpMesh()
    {
        //lvl one check
        if(souls > LevelUpCosts[0])
        {
            ActiveMesh = 1;
            Mesh[0].gameObject.SetActive(false);
            Mesh[ActiveMesh].gameObject.SetActive(true);
        }
        else if (souls < LevelUpCosts[0]) // go to lvl zero
        {
            ActiveMesh =0;
            Mesh[1].gameObject.SetActive(false);
            Mesh[ActiveMesh].gameObject.SetActive(true);
        }




    }
    void attackFunction()
    {
        if (Input.GetMouseButton(0))
        {
            if (IsAttacking == false && Weapon != null)
            {
                if (Weapon.tag == "Weapon")
                {
                    IsAttacking = true;
                    StartCoroutine("Attack", attackTime);
                }
                else if (Weapon.tag == "Item")
                {
                    GetComponent<IntController>().useItem();
                }
                else
                {
                    IsAttacking = true;
                    StartCoroutine("Attack", attackTime);
                }
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
        //Debug.Log(gameObject.transform.rotation.y);
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

        if (IsAttacking) // does attacks depending on the players weapons using Weapon Controller
        {
            if (Weapon != null && Weapon.tag == "Weapon")
            {
                if (Weapon.GetComponent<WeaponController>().WeaponType == "Sword")
                {
                    Weapon.GetComponent<WeaponController>().ActivateCollider();
                    animations.speed = 3;
                    animations.Play("Melee");
                }
                else if (Weapon.GetComponent<WeaponController>().WeaponType == "Bow")
                {
                    animations.speed = 3;
                    animations.Play("Bow");
                }
                else if (Weapon.GetComponent<WeaponController>().WeaponType == "Magic")
                {
                    animations.speed = 3;
                    animations.Play("Spell");
                }

            }
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

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {

            if (IsAttacking)
            {
                // if enemy is in hit box take damage
            }
        }

        if (other.gameObject.tag == "Wall")
        {
            // make sure you dont go through wall
        }

        if (other.gameObject.tag == "Loot")
        {
            // add to inv
        }

    }

    IEnumerator Attack(float attackTime) // Attack
    {
        IsAttacking = true; // do damage
        for (float f = 0.0f; f <= 0.5f; f += 0.1f)
        {
            if (f == 0.4f)
            {
                if (Weapon != null)
                {
                    if (Weapon.GetComponent<WeaponController>().WeaponType == "Bow")
                    {
                        Instantiate(Weapon.GetComponent<WeaponController>().projectile, Weapon.transform.position, gameObject.transform.rotation);
                    }
                }

            }
            yield return new WaitForSeconds(0.1f); // wait for animation to be in the position to do damage.
        }

        IsAttacking = false;
    }

    IEnumerator Damage(float DamageTimer) // Take Damage make invinvible
    {
        invincable = true;
        for (float f = 0f; f <= DamageTimer; f += 0.1f)
        {

            yield return new WaitForSeconds(0.1f); // can't take damage until timer ends
        }

        invincable = false;
    }

    //Timer for regenerating mana and stamina
    IEnumerator Regen()
    {
        regeneratingManaStamina = false;
        yield return new WaitForSeconds(0.1f);
        regeneratingManaStamina = true;
    }

    public void TakeDamage(int damage)
    {
        if (!invincable)
        {
            health -= damage;
            StartCoroutine("Damage", DamageTimer);
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

