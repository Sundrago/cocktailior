using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainControl_2 : MonoBehaviour
{
    public List<int> currentStageIdx = new List<int>(39);
    public int currentIdx;
    public int maxIdx;
    public GameObject myPanel;
    public GameObject quizePanel;
    public GameObject myCanvas;
    public Button Xbtn, Obtn;
    public GameObject panel_holder;

    GameObject newPanel;

    char[] oxDatas = new char[39];
    int XCount;
    int OCount;
    public Text XText, OText;
    public GameObject SelectionPanel;
    public Sprite icoO, icoX, icoN, selectionImg, selectedImg;
    public GameObject menuBtn;
    public bool showMenu;
    public ScrollRect myScrollRect;
    public bool questionMode;
    public GameObject menu_selctor;
    public GameObject SearchPanel;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        SearchPanel.SetActive(false);
        ReadData();

        for (int i = 0; i<39; i++)
        {
            currentStageIdx.Add(i);
        }

        currentIdx = 0;
        maxIdx = currentStageIdx.Count;
        InitiateSelection();

        OpenCard(0, "new");

        menuBtn.GetComponent<Animator>().SetTrigger("menu");
        SelectionPanel.GetComponent<Animator>().SetTrigger("hide");
        showMenu = false;
    }


    void InitiateSelection()
    {
        SelectionPanel.GetComponent<SetSelection>().Initiate(maxIdx);
        foreach(GameObject obj in SelectionPanel.GetComponent<SetSelection>().selections)
        {
            obj.GetComponent<Button>().onClick.AddListener(() => Goto(System.Array.IndexOf(SelectionPanel.GetComponent<SetSelection>().selections, obj)));
        }
    }

    void UpdateSelection()
    {
        for (int i = 0; i < maxIdx; i++)
        {
            SelectionPanel.GetComponent<SetSelection>().selections[i].GetComponent<Selection>().indexTxt.text = "" + (i+1);
            SelectionPanel.GetComponent<SetSelection>().selections[i].GetComponent<Selection>().nameTxt.text = newPanel.GetComponent<CocktailList>().ReturnName(currentStageIdx[i]);
            switch (oxDatas[currentStageIdx[i]])
            {
                case 'O':
                    SelectionPanel.GetComponent<SetSelection>().selections[i].GetComponent<Selection>().smallIco.GetComponent<Image>().sprite = icoO;
                    break;
                case 'X':
                    SelectionPanel.GetComponent<SetSelection>().selections[i].GetComponent<Selection>().smallIco.GetComponent<Image>().sprite = icoX;
                    break;
                case '-':
                    SelectionPanel.GetComponent<SetSelection>().selections[i].GetComponent<Selection>().smallIco.GetComponent<Image>().sprite = icoN;
                    break;
            }

            if(currentIdx == i) SelectionPanel.GetComponent<SetSelection>().selections[i].GetComponent<Image>().sprite = selectedImg;
            else SelectionPanel.GetComponent<SetSelection>().selections[i].GetComponent<Image>().sprite = selectionImg;
            myScrollRect.verticalNormalizedPosition = 1f - (float)currentIdx /(float)maxIdx;
        }
    }

    public void Goto(int index)
    {
        Debug.Log(index);
        if (currentIdx < currentStageIdx[index])
        {
            newPanel.GetComponent<PanelAnimControl>().PlayAnim("scroll_right");
            OpenCard(currentStageIdx[index], "new");
        }
        else if (currentIdx > currentStageIdx[index])
        {
            newPanel.GetComponent<PanelAnimControl>().PlayAnim("scroll_left");
            OpenCard(currentStageIdx[index], "old");
        }
        menuBtn.GetComponent<Animator>().SetTrigger("menu");
        SelectionPanel.GetComponent<Animator>().SetTrigger("hide");
        showMenu = false;
    }

    public void GoBefore()
    {
        if(currentIdx < maxIdx - 1)
        {
            currentIdx++;
            OpenCard(currentIdx, "new");
        } else
        {
            currentIdx = 0;
            OpenCard(currentIdx, "new");
        }
    }

    public void GoNext()
    {
        if (currentIdx > 0)
        {
            currentIdx--;
            OpenCard(currentIdx, "old");
        }
        else
        {
            currentIdx = maxIdx - 1;
            OpenCard(currentIdx, "old");
        }
    }

    void OpenCard(int i, string anim)
    {
        currentIdx = i;
        newPanel = GameObject.Instantiate(quizePanel, new Vector3(quizePanel.transform.position.x, quizePanel.transform.position.y, quizePanel.transform.position.z), Quaternion.identity, panel_holder.transform);
        //newPanel.transform.SetParent(myCanvas.transform);
        newPanel.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 30, 0);
        newPanel.transform.localScale = new Vector3(1f, 1f, 1f);
        newPanel.GetComponent<PanelAnimControl>().PlayAnim(anim);
        UpdateSelection();
    }

    public void ReadData()
    {
        string inputStr;
        if(!PlayerPrefs.HasKey("oxData"))
        {
            PlayerPrefs.SetString("oxData", "---------------------------------------");
            Debug.Log("Data Set Default");
        } else if(PlayerPrefs.GetString("oxData").Length != 39) {
            PlayerPrefs.SetString("oxData", "---------------------------------------");
            Debug.Log("Data Set Default");
        }
        inputStr = PlayerPrefs.GetString("oxData");

        for (int i = 0; i<inputStr.Length; i++)
        {
            oxDatas[i] = inputStr[i];
        }

        Debug.Log("Data Loaded : " + inputStr);
        UpdateCountData();
    }

    public void SaveData()
    {
        string outputStr = "";

        for(int i = 0; i<oxDatas.Length; i++)
        {
            outputStr += oxDatas[i];
        }

        PlayerPrefs.SetString("oxData", outputStr);
        PlayerPrefs.Save();

        Debug.Log("Data Saved : " + outputStr);
        UpdateCountData();
        UpdateSelection();
    }

    void UpdateCountData()
    {
        XCount = 0;
        OCount = 0;

        for(int i = 0; i<oxDatas.Length; i++)
        {
            if (oxDatas[i] == 'O') OCount++;
            else if (oxDatas[i] == 'X') XCount++;
        }

        XText.text = "" + XCount;
        OText.text = "" + OCount;

    }

    public void OClicked()
    {
        if(!newPanel.GetComponent<CocktailList>().O_show)
        {
            newPanel.GetComponent<PanelAnimControl>().PlayAnim("left");
            newPanel.GetComponent<CocktailList>().IconControl("O_show");
            oxDatas[currentIdx] = 'O';
            SaveData();
        } else
        {
            newPanel.GetComponent<PanelAnimControl>().PlayAnim("left");
            newPanel.GetComponent<CocktailList>().IconControl("O_hide");
            oxDatas[currentIdx] = '-';
            SaveData();
        }
        //newPanel.GetComponent<MouseDrag>().Righted();
    }

    public void XClicked()
    {
        if (!newPanel.GetComponent<CocktailList>().X_show)
        {
            newPanel.GetComponent<PanelAnimControl>().PlayAnim("right");
            oxDatas[currentIdx] = 'X';
            SaveData();
            newPanel.GetComponent<CocktailList>().IconControl("X_show");
        } else
        {
            newPanel.GetComponent<PanelAnimControl>().PlayAnim("right");
            oxDatas[currentIdx] = '-';
            SaveData();
            newPanel.GetComponent<CocktailList>().IconControl("X_hide");
        }
        //newPanel.GetComponent<MouseDrag>().Lefted();

    }

    public void Right()
    {
        //oxDatas[currentIdx] = 'O';
        //SaveData();
        GoBefore();
    }

    public void Left()
    {
        //oxDatas[currentIdx] = 'X';
        //SaveData();
        GoNext();
    }

    public void menu_btn_clicked()
    {
        menu_selctor.GetComponent<menu_selector>().SetTarget(1);
        if(!showMenu)
        {
            SelectionPanel.SetActive(true);
            menuBtn.GetComponent<Animator>().SetTrigger("back");
            SelectionPanel.GetComponent<Animator>().SetTrigger("show");
            showMenu = true;
        }
        else
        {
            menuBtn.GetComponent<Animator>().SetTrigger("menu");
            SelectionPanel.GetComponent<Animator>().SetTrigger("hide");
            showMenu = false;
        }
    }

    public void TestBtnClicked()
    {
        menu_selctor.GetComponent<menu_selector>().SetTarget(1);
        menu_btn_clicked();
    }

    public void AnswerBtnClicked()
    {
        menu_selctor.GetComponent<menu_selector>().SetTarget(2);
        SearchPanel.SetActive(true);

    }

    public void ExitBtnClicked()
    {
        menu_selctor.GetComponent<menu_selector>().SetTarget(3);
        SearchPanel.SetActive(false);
    }
}
