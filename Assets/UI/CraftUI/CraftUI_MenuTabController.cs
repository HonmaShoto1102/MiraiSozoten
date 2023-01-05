using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum MenuTab
{
    CRAFT_TAB=0,
    ITEM_TAB,
    MAX_TAB
}

public class CraftUI_MenuTabController : MonoBehaviour
{
    GameObject TabCursol;
    RectTransform TabTransform;
    
    GameObject CraftPanel;
    GameObject ItemPanel;
    GameObject CraftSubjectPanel;


    MenuTab NowTab;

    // Start is called before the first frame update
    void Start()
    {
        GameObject CenterUIPanel = this.gameObject.transform.parent.gameObject;

        CraftPanel = CenterUIPanel.transform.Find("CraftUIPanel").gameObject;
        CraftSubjectPanel = CraftPanel.transform.Find("SubjectPanel").gameObject;
        ItemPanel = CenterUIPanel.transform.Find("ItemMenuPanel").gameObject;

        TabCursol = this.gameObject.transform.Find("MenuTabCursol").gameObject;
        TabTransform = TabCursol.GetComponent<RectTransform>();

        CraftPanel.SetActive(true);
        ItemPanel.SetActive(false);

        NowTab = MenuTab.CRAFT_TAB;
    }

    // Update is called once per frame
    void Update()
    {
        // キー入力でカーソルを移動させる
        if (Input.GetKeyDown(KeyCode.W) && NowTab == MenuTab.CRAFT_TAB) //　アイテムタブに切り替え
        {
            TabTransform.anchoredPosition = new Vector3(110.0f, 0.0f, 0.0f);

            // タブを切り替える
            CraftPanel.SetActive(false);
            ItemPanel.SetActive(true);

            CraftSubjectPanel.SetActive(false);

            NowTab = MenuTab.ITEM_TAB;
        }
        else if (Input.GetKeyDown(KeyCode.Q) && NowTab == MenuTab.ITEM_TAB) //　クラフトタブに切り替え
        {
            TabTransform.anchoredPosition = new Vector3(-110.0f, 0.0f, 0.0f);

            // タブを切り替える
            CraftPanel.SetActive(true);
            ItemPanel.SetActive(false);

            CraftSubjectPanel.SetActive(true);

            NowTab = MenuTab.CRAFT_TAB;
        }
    }

    // 初期化(クラフトモードに入る時に呼ぶ)
    public void Init()
    {
        CraftPanel.SetActive(true);
        ItemPanel.SetActive(false);

        NowTab = MenuTab.CRAFT_TAB;
    }
}
