using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class TitleCloudBone : MonoBehaviour
{
    [SerializeField]
    [Header("�J������Transform������")]
    private Transform _cameraTransform;

    [SerializeField] [Header("prefab�̉_��GameObject������")]
    private GameObject _cloudObject;

    [SerializeField] [Header("��������Ȃ�")]
    private GameObject[] _cloudArray;

    [SerializeField][Header("�_�̐�����")]
    private int _cloudAmount;

    [Header("----------------------")]
    [SerializeField]
    [Range(-1200.0f, -600.0f)] private float MinPosition_X = -850.0f;
    [SerializeField]
    [Range(-1100.0f, -500.0f)] private float MaxPosition_X = -750.0f;

    [SerializeField]
    [Range(-1500.0f, 0.0f)] private float MinPosition_W = -1500.0f;
    [SerializeField]
    [Range(0.0f, 1500.0f)] private float MaxPosition_W = 1500.0f;

    [SerializeField]     private float MinPosition_Y = 150.0f;
    [SerializeField]     private float MaxPosition_Y = 300.0f;

    //ki-
    Keyboard keyboard;
    int putA = 0;

    // Start is called before the first frame update
    void Start()
    {
        _cloudArray = new GameObject[_cloudAmount];

        Spawn();

        // ���݂̃L�[�{�[�h���
        keyboard = Keyboard.current;
        // �L�[�{�[�h�ڑ��`�F�b�N
        if (keyboard == null)
        {
            // �L�[�{�[�h���ڑ�����Ă��Ȃ���
            // Keyboard.current��null�ɂȂ�
            return;
        }
    }

    private void Spawn()
    {
        for (int i = 0; i < _cloudAmount; i++)
        {
            _cloudArray[i] = Instantiate(_cloudObject, RandomVector3(), Quaternion.identity, this.gameObject.transform);
        }

    }

    // Update is called once per frame
    void Update()
    {
        //// 1�L�[�̓��͏�Ԏ擾
        //var Key_a = keyboard.aKey;
        
        //if (Key_a.wasPressedThisFrame)
        //{
        //    _cloudArray[putA].SetActive(false);
        //    putA += 1;

        //    if (putA >= _cloudAmount)
        //        putA = 0;
            
        //}

       
    }

    private void FixedUpdate()
    {
        int index = CloudArraySerch();

        if (index >= 0)//_cloudArray��Null���o������
        {
            Destroy(_cloudArray[index]);
            _cloudArray[index] = Instantiate(_cloudObject, RandomVector3(), Quaternion.identity, this.gameObject.transform);
        }
    }


    //SetActive(False)�̗v�f��T���Ainstantiate����ׂ̗v�f�̔ԍ���Ԃ�
    private int CloudArraySerch()
    {
        int[] copyArray = new int[_cloudAmount];

        for (int i = 0; i < _cloudAmount; i++)
        {
            copyArray[i] = Convert.ToInt32(_cloudArray[i].activeSelf);
        }

        int gameint = Array.IndexOf(copyArray, 0);
       
        return gameint;
    }


    private Vector3 RandomVector3()
    {

        float x = UnityEngine.Random.Range(MinPosition_X, MaxPosition_X);
        float w = UnityEngine.Random.Range(MinPosition_W, MaxPosition_W);
        float y = UnityEngine.Random.Range(MinPosition_Y, MaxPosition_Y);

        Vector3 position = new Vector3(x, y, w);

        position = position + _cameraTransform.position;
        return position;
    }
}
