using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb_Dino;
    private Animator anim_Dino;

    private bool isCanMove;
    [SerializeField] private float speedDino;

    private const string isRun = "isRun";
    private const string isPickUp = "isPickUp";

    private void Start()
    {
        rb_Dino = GetComponent<Rigidbody>();
        anim_Dino = GetComponentInChildren<Animator>();

        isCanMove = true;
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
        rb_Dino.velocity = movement * speedDino;

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

        return isCanMove;
    }
}
