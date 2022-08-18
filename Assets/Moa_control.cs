using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moa_control : MonoBehaviour
{
    public GameObject myPanel, panel_holder;
    char[] oxDatas = new char[39];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OpenCard(int i)
    {
        string inputStr = PlayerPrefs.GetString("oxData");
        for (int j = 0; j<inputStr.Length; j++)
        {
            oxDatas[j] = inputStr[j];
        }
        int currentIdx = i;
        GameObject newPanel = GameObject.Instantiate(myPanel, new Vector3(myPanel.transform.position.x, myPanel.transform.position.y, myPanel.transform.position.z), Quaternion.identity, panel_holder.transform);
        //newPanel.transform.SetParent(myCanvas.transform);
        newPanel.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 30, 0);
        newPanel.GetComponent<CocktailList>().Start();
        newPanel.GetComponent<CocktailList>().current_receipe = currentIdx;
        newPanel.GetComponent<CocktailList>().max_receipe = 0;
        newPanel.GetComponent<CocktailList>().UpdateCocktailInfo(i);
        newPanel.GetComponent<CocktailList>().showAll();
        newPanel.GetComponent<CocktailList>().destroyOnClose = true;
        newPanel.transform.localScale = new Vector3(1f, 1f, 1f);
        newPanel.GetComponent<PanelAnimControl>().PlayAnim("new");
        newPanel.GetComponent<MouseDrag>().searchMode = true;
        newPanel.GetComponent<MouseDrag>().SetCloseBtn();

        if (oxDatas[i] == 'O')
        {
            newPanel.GetComponent<CocktailList>().IconControl("X_hidden");
            newPanel.GetComponent<CocktailList>().IconControl("O_showed");
        }
        else if (oxDatas[i] == 'X')
        {
            newPanel.GetComponent<CocktailList>().IconControl("O_hidden");
            newPanel.GetComponent<CocktailList>().IconControl("X_showed");
        }
        else
        {
            newPanel.GetComponent<CocktailList>().IconControl("X_hidden");
            newPanel.GetComponent<CocktailList>().IconControl("O_hidden");
        }
        //UpdateSelection();
    }
}
