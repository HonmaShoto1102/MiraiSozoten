using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CraftUI_SubjectIconController : MonoBehaviour
{
    CraftUI_MainSubjectController MSC;

    bool CursolNotice;// �J�[�\�����������Ă��邩�̃t���O

    // Start is called before the first frame update
    void Start()
    {        
        CursolNotice = false;

        // Player��CraftUI_MainSubjectController����擾
        GameObject SubjectPanel = this.gameObject.transform.parent.gameObject;
        MSC = SubjectPanel.GetComponent<CraftUI_MainSubjectController>();
    }

    // Update is called once per frame
    void Update()
    {
        // �J�[�\�����������Ă��鎞��Enter�ŋ���
        if (CursolNotice == true)
        {
            
        }
    }
    
    // �J�[�\��������������CursolNotice��true�ɂ���
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "SubjectCursol")
        {
            // MeinSubjectController�̃e�L�X�g���X�V
            MSC.TextUpdate();

            CursolNotice = true;
        }
    }

    // �J�[�\�������ꂽ����CursolNotice��false�ɂ���
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "SubjectCursol")
        {
            CursolNotice = false;
        }
    }
}
