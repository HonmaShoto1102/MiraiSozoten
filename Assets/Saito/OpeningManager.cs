using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningManager : MonoBehaviour
{
    [SerializeField] TextController text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // �e�L�X�g�I���t���O����������V�[���I��
        if (text.GetTextEnd() == true)
        {
            OpeningEnd();
        }
    }

    // �V�[���ύX�p�̊֐�
    void OpeningEnd()
    {

    }
}
