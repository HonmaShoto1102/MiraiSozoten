using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using SoftGear.Strix.Unity.Runtime;
using SoftGear.Strix.Client.Core;

public class ResultData : StrixBehaviour
{
    [SerializeField][Header("Player��script")]
    private Player _playerScript;

    [SerializeField][Header("1�`4�܂ł̃t�F�[�Y������")]
    private List<GameObject> _phaseList;

    [SerializeField][Header("�{�^�����������ƕ\��")]
    private GameObject _phase04Ui;


    Keyboard _keyboard;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
