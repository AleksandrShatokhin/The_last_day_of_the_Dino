using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceForBlowing : MonoBehaviour
{
    [SerializeField] private Animator anim_Device;
    [SerializeField] private AudioClip audioBoom;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerController>().Jump();
            anim_Device.SetTrigger("isClick");
            GameController.GetInstance().Counter(-5);
            GameController.GetInstance().GetShakeCamera();
            GameController.GetInstance().AudioGame(audioBoom);
        }
    }
}
