using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftUI_ItemIconController : MonoBehaviour
{
    GameObject Detail;
    Image DetailColor;
    float alpha;
    float aimAlpha;
    float ChangeTime;
    bool fade;

    // Start is called before the first frame update
    void Start()
    {
        Detail = this.gameObject.transform.Find("ItemDetail").gameObject;
        Detail.SetActive(false);
        DetailColor = Detail.GetComponent<Image>();
        alpha = 0.0f;
        fade = true;
    }

    // Update is called once per frame
    void Update()
    {
        //if (fade)
        //{
        //    // �摜���t�F�[�h
        //    // ���Ԍv��
        //    ChangeTime += Time.deltaTime;

        //    // ���l���v�Z
        //    alpha = aimAlpha * (ChangeTime / 0.2f);

        //    if (ChangeTime > 0.2f)
        //    {
        //        alpha = aimAlpha;
        //    }
        //    DetailColor.a = alpha;
        //}
        

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "ItemMenuCursol")
        {
            this.gameObject.transform.SetSiblingIndex(14);
            Detail.SetActive(true);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "ItemMenuCursol")
        {
            Detail.SetActive(false);
        }
    }
}
