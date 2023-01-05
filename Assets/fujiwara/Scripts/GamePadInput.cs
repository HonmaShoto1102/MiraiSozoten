using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class GamePadInput : MonoBehaviour
{
    [SerializeField] private UnityEvent Press_EnterEvent = new UnityEvent();
    [SerializeField] private UnityEvent Press_BackEvent = new UnityEvent();
    [SerializeField] private UnityEvent Press_LeftEvent = new UnityEvent();
    [SerializeField] private UnityEvent Press_RightEvent = new UnityEvent();
    [SerializeField] private UnityEvent Press_UpEvent = new UnityEvent();
    [SerializeField] private UnityEvent Press_DownEvent = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Y�{�^��
        if (Gamepad.current.buttonNorth.wasPressedThisFrame)
        {
            Debug.Log("Button Y�������ꂽ�I");
        }

        //X�{�^��
        if (Gamepad.current.buttonWest.wasPressedThisFrame)
        {
            Debug.Log("Button X�������ꂽ�I");
        }

        //A�{�^��
        if (Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            Debug.Log("Button A�������ꂽ�I");
            Press_EnterEvent.Invoke();
        }

        //B�{�^��
        if (Gamepad.current.buttonEast.wasPressedThisFrame)
        {
            Debug.Log("Button B�������ꂽ�I");
            Press_BackEvent.Invoke();
        }

        //���{�^��
        if (Gamepad.current.dpad.left.wasPressedThisFrame)
        {
            Debug.Log("Button ���������ꂽ�I");
            Press_LeftEvent.Invoke();
        }

        //�E�{�^��
        if (Gamepad.current.dpad.right.wasPressedThisFrame)
        {
            Debug.Log("Button �E�������ꂽ�I");
            Press_RightEvent.Invoke();
        }

        //��{�^��
        if (Gamepad.current.dpad.up.wasPressedThisFrame)
        {
            Debug.Log("Button �オ�����ꂽ�I");
            Press_UpEvent.Invoke();

        }

        //���{�^��
        if (Gamepad.current.dpad.down.wasPressedThisFrame)
        {
            Debug.Log("Button ���������ꂽ�I");
            Press_DownEvent.Invoke();
        }

        if (Gamepad.current.leftStick.ReadValue().x > 0.5)
        {
            Debug.Log("���X�e�B�b�N���E�ɓ|����");
        }

        if (Gamepad.current.leftStick.ReadValue().x < -0.5)
        {
            Debug.Log("���X�e�B�b�N�����ɓ|����");
        }

        if (Gamepad.current.leftStick.ReadValue().y > 0.5)
        {
            Debug.Log("���X�e�B�b�N����ɓ|����");
        }

        if (Gamepad.current.leftStick.ReadValue().y < -0.5)
        {
            Debug.Log("���X�e�B�b�N�����ɓ|����");
        }
    }
}
