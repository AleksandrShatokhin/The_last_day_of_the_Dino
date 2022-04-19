using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb_Dino;
    private Animator anim_Dino;

    [SerializeField] private AudioClip audioCastAsteroid, audioJump;

    private bool isCanMove;
    private bool isCanJump;
    [SerializeField] private bool isPickUpLittleAsteroid, isPickUpLittleLizard;
    [SerializeField] private float speedDino;

    [SerializeField] private GameObject asteroid;
    [SerializeField] private GameObject spawnAsteroid;

    private const string isRun = "isRun";
    private const string isPickUp = "isPickUp";
    private const string isThrow = "isThrow";
    private const string isJump = "isJump";

    private void Start()
    {
        rb_Dino = GetComponent<Rigidbody>();
        anim_Dino = GetComponentInChildren<Animator>();

        isCanMove = true;
        isCanJump = false;
        isPickUpLittleAsteroid = false;
        isPickUpLittleLizard = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isCanJump)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        if (isCanMove)
        {
            MoveDino();
        }
    }

    void MoveDino()
    {
        // получаем данные по вводу
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // задаем движение
        Vector3 movement = new Vector3(horizontal, 0.0f, vertical);
        rb_Dino.MovePosition(transform.position + movement * speedDino);

        // поворачиваем персонажа в сторону движения
        if (Vector3.Angle(Vector3.forward, movement) > 1f || Vector3.Angle(Vector3.forward, movement) == 0)
        {
            Vector3 direct = Vector3.RotateTowards(transform.forward, movement, speedDino, 0.0f);
            transform.rotation = Quaternion.LookRotation(direct);
        }

        // задаем анимацию движения
        if (horizontal != 0 || vertical != 0)
        {
            anim_Dino.SetBool(isRun, true);
        }
        else
        {
            anim_Dino.SetBool(isRun, false);
        }
    }

    public void Jump()
    {
        isCanMove = false;
        GameController.GetInstance().AudioGame(audioJump);
        anim_Dino.SetTrigger(isJump);
        rb_Dino.AddForce((transform.up + transform.forward) * 5.0f, ForceMode.Impulse);
        StartRecoverMove();
    }

    // вариант 1
    public bool PickUpObject(bool variable)
    {
        if (!variable)
        {
            isCanMove = variable;
            anim_Dino.SetTrigger(isPickUp);
        }
        else
        {
            isCanMove = variable;
        }

        return isCanMove;
    }

    // вариант 2
    public bool PickUpObject(bool variable, Transform positionObject)
    {
        if (!isPickUpLittleAsteroid)
        {
            isPickUpLittleAsteroid = true;

            // зададим поворот в сторону поднимаемого объекта
            Vector3 relativePosition = positionObject.position - transform.position;
            Quaternion rotationToObject = Quaternion.LookRotation(relativePosition, Vector3.up);
            transform.rotation = rotationToObject;

            if (!variable)
            {
                isCanMove = variable;
                anim_Dino.SetTrigger(isPickUp);
            }
            else
            {
                isCanMove = variable;
            }
        }

        return isCanMove;
    }

    // при поедании ящериц вызывается в lizardcontroller
    public bool PickUpLizard(bool variable, Transform positionLizard)
    {
        // зададим поворот в сторону поднимаемого объекта
        Vector3 relativePosition = positionLizard.position - transform.position;
        Quaternion rotationToLizard = Quaternion.LookRotation(relativePosition, Vector3.up);
        transform.rotation = rotationToLizard;

        isCanMove = variable;
        anim_Dino.SetTrigger(isPickUp);

        return isCanMove;
    }

    public bool IsCanJump(bool variable)
    {
        isCanJump = variable;
        return isCanJump;
    }

    public bool IsPickUpLittleAsteroid()
    {
        return isPickUpLittleAsteroid;
    }

    public bool IsPickUpLittleLizard()
    {
        return isPickUpLittleLizard;
    }

    public void StartRecoverMove()
    {
        StartCoroutine(RecoverMove());
    }

    IEnumerator RecoverMove()
    {
        float delay = 1.4f;

        yield return new WaitForSeconds(delay);
        isCanMove = true;
    }

    IEnumerator DelayThrow()
    {
        float delay = 0.6f;

        yield return new WaitForSeconds(delay);
        Instantiate(asteroid, spawnAsteroid.transform.position, Quaternion.identity);
        GameController.GetInstance().AudioGame(audioCastAsteroid);
    }

    public bool ThrowObject(bool variable, Transform positionObject)
    {
        isPickUpLittleAsteroid = variable;

        // зададим поворот в сторону отверстия в вулкане
        Vector3 relativePosition = positionObject.position - transform.position;
        Quaternion rotationToObject = Quaternion.LookRotation(relativePosition, Vector3.up);
        transform.rotation = rotationToObject;

        isCanMove = false;
        anim_Dino.SetTrigger(isThrow);
        StartCoroutine(DelayThrow());
        StartRecoverMove();

        return isPickUpLittleAsteroid;
    }
}
