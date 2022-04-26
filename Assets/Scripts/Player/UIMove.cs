using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMove : Joystick
{
    [SerializeField] private Transform player;
    private Vector3 pointA, pointB;

    public void CheckTouch(float speed)
    {
        KeyCode clickMouse = KeyCode.Mouse0;
        Vector3 offset = pointB - pointA;

        if (Input.mousePosition.x < Screen.width / 2)
        {
            if (Input.GetKeyDown(clickMouse))
            {
                background.transform.position = Input.mousePosition;

                background.GetComponent<Image>().enabled = true;
                handle.GetComponent<Image>().enabled = true;
            }

            if (Input.GetKey(clickMouse))
            {
                Movement(speed);
            }

            if (Input.GetKeyUp(clickMouse))
            {
                background.transform.position = Vector3.zero;

                background.GetComponent<Image>().enabled = false;
                handle.GetComponent<Image>().enabled = false;
            }
        }
    }

    private void Movement(float speedDino)
    {
        // задаем движение
        Vector3 movement = new Vector3(Horizontal, 0.0f, Vertical);
        player.GetComponent<Rigidbody>().MovePosition(player.position + movement * speedDino);

        // задаем анимацию движения
        if (Horizontal != 0 || Vertical != 0)
        {
            player.GetComponent<PlayerController>().AnimatorDino().SetBool("isRun", true);
        }
        else
        {
            player.GetComponent<PlayerController>().AnimatorDino().SetBool("isRun", false);
        }

        // поворачиваем персонажа в сторону движения
        if (Vector3.Angle(Vector3.forward, movement) > 1f || Vector3.Angle(Vector3.forward, movement) == 0)
        {
            Vector3 direct = Vector3.RotateTowards(player.transform.forward, movement, speedDino, 0.0f);
            player.transform.rotation = Quaternion.LookRotation(direct);
        }
    }
}
