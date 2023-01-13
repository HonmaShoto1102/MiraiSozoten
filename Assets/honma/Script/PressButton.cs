using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PressButton : MonoBehaviour
{
    [SerializeField][Header("ClickLoadScene��script�������Ă���")]
    private NextSceneLoad _nextSceneLoadScript;

    [SerializeField]
    private GameObject TitleUi;

    Keyboard _keyboard;

    private float _uiTimer;

    // Start is called before the first frame update
    void Start()
    {
        // ���݂̃L�[�{�[�h���
        _keyboard = Keyboard.current;

        // �L�[�{�[�h�ڑ��`�F�b�N
        if (_keyboard == null)
        {
            // �L�[�{�[�h���ڑ�����Ă��Ȃ���
            // Keyboard.current��null�ɂȂ�
            return;
        }

        _uiTimer = 0.0f;
        TitleUi.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        // B�L�[�̓��͏�Ԏ擾
        var Key_putB = _keyboard.bKey;

        if (/*Gamepad.current.bButton.wasPressedThisFrame||*/Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Go");
            _nextSceneLoadScript.LoadSceneStart("Scenes/OpeningCreate");//�V�[��������
        }

        UiSpawn();
    }

    private void UiSpawn()
    {
        _uiTimer += Time.deltaTime;

        if (_uiTimer >= 120.0f)
        {
            TitleUi.SetActive(!TitleUi.activeSelf);
        }
        if(_uiTimer >= 180.0f)
        {
            TitleUi.SetActive(!TitleUi.activeSelf);
            _uiTimer = 0.0f;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            TitleUi.SetActive(!TitleUi.activeSelf);
            _uiTimer = 0.0f;
        }
    }
}
