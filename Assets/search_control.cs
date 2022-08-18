using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class search_control : MonoBehaviour
{
    public GameObject contents;
    public GameObject selection;
    public GameObject[] selections;
    public GameObject cocktail;
    public Text input;
    public GameObject myPanel, panel_holder;
    public Sprite icoO, icoX, icoN;
    char[] oxDatas = new char[39];
    List<int> found = new List<int>();

    int nextUpdate = 1;
    // Start is called before the first frame update

    private void Start()
    {
        SearchUpdate();
    }

    private void Update()
    {
        if(Time.time>=nextUpdate){
             // Change the next update (current second+1)
             nextUpdate=Mathf.FloorToInt(Time.time)+1;
             // Call your fonction
             SearchUpdate();
         }
    }

    public void Initiate(int count)
    {
        //reset
        for (int i = 0; i < selections.Length; i++)
        {
            if (i == 0)
            {
                selections[0].GetComponent<Button>().onClick.RemoveAllListeners();
            }
            else
            {
                Destroy(selections[i]);
            }
        }
        selections = new GameObject[count];

        //initiate
        if (count == 0)
        {
            selection.SetActive(false);
            return;
        }
        else
        {
            selection.SetActive(true);
            contents.GetComponent<RectTransform>().sizeDelta = new Vector2(contents.GetComponent<RectTransform>().sizeDelta.x, -400 + 90f * count);
            
            selections[0] = selection;
            for (int i = 1; i < count; i++)
            {
                selections[i] = GameObject.Instantiate(selection, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity, contents.transform);
                selections[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(selections[i - 1].GetComponent<RectTransform>().anchoredPosition.x, selections[i - 1].GetComponent<RectTransform>().anchoredPosition.y - 90f);
            }
        }
    }

    public void SearchUpdate()
    {
        string inputStr = PlayerPrefs.GetString("oxData");
        for (int j = 0; j<inputStr.Length; j++)
        {
            oxDatas[j] = inputStr[j];
        }
        
        string key = input.text;
        found = new List<int>();
        for (int i = 0; i< cocktail.GetComponent<CocktailList>().cocktails.Count; i++)
        {
            if (cocktail.GetComponent<CocktailList>().cocktails[i].C_name.Contains(key))
            {
                if(!found.Contains(i)) found.Add(i);
            }
        }
        for (int i = 0; i < cocktail.GetComponent<CocktailList>().cocktails.Count; i++)
        {
            if (cocktail.GetComponent<CocktailList>().cocktails[i].C_glass.Contains(key))
            {
                if (!found.Contains(i)) found.Add(i);
            }
        }
        for (int i = 0; i < cocktail.GetComponent<CocktailList>().cocktails.Count; i++)
        {
            for (int j = 0; j < cocktail.GetComponent<CocktailList>().cocktails[i].C_recipe.Count; j++)
            {
                if (cocktail.GetComponent<CocktailList>().cocktails[i].C_recipe[j].Contains(key))
                {
                    if (!found.Contains(i)) found.Add(i);
                }
            }
        }
        for (int i = 0; i < cocktail.GetComponent<CocktailList>().cocktails.Count; i++)
        {
            if (cocktail.GetComponent<CocktailList>().cocktails[i].C_methode.Contains(key))
            {
                if (!found.Contains(i)) found.Add(i);
            }
        }
        for (int i = 0; i < cocktail.GetComponent<CocktailList>().cocktails.Count; i++)
        {
            if (cocktail.GetComponent<CocktailList>().cocktails[i].C_garnish.Contains(key))
            {
                if (!found.Contains(i)) found.Add(i);
            }
        }

        Initiate(found.Count);

        for(int i = 0; i<found.Count; i++)
        {
            selections[i].GetComponent<Selection>().indexTxt.text = "" + (i + 1);
            selections[i].GetComponent<Selection>().nameTxt.text = cocktail.GetComponent<CocktailList>().ReturnName(found[i]);
            int idx = found[i];
            selections[i].GetComponent<Button>().onClick.AddListener(() => OpenCard(idx));

            switch (oxDatas[found[i]])
            {
                case 'O':
                    selections[i].GetComponent<Selection>().smallIco.GetComponent<Image>().sprite = icoO;
                    break;
                case 'X':
                    selections[i].GetComponent<Selection>().smallIco.GetComponent<Image>().sprite = icoX;
                    break;
                case '-':
                    selections[i].GetComponent<Selection>().smallIco.GetComponent<Image>().sprite = icoN;
                    break;
            }

        }
    }

    public void hideMenu()
    {
        gameObject.SetActive(false);
    }

    void OpenCard(int i)
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
        newPanel.GetComponent<MouseDrag>().searchMode = true;
        newPanel.GetComponent<MouseDrag>().SetCloseBtn();
        newPanel.transform.localScale = new Vector3(1f, 1f, 1f);
        newPanel.GetComponent<PanelAnimControl>().PlayAnim("new");

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

    /*
    void UpdateSelection()
    {
        GameObject newPanel = GameObject.Instantiate(myPanel, new Vector3(myPanel.transform.position.x, myPanel.transform.position.y, myPanel.transform.position.z), Quaternion.identity, panel_holder.transform);

        string inputStr = PlayerPrefs.GetString("oxData");
        for (int i = 0; i<inputStr.Length; i++)
        {
            oxDatas[i] = inputStr[i];
        }

        for (int i = 0; i < selections.Length; i++)
        {            
            switch (oxDatas[currentStageIdx[i]])
            {
                case 'O':
                    selections[i].GetComponent<Selection>().smallIco.GetComponent<Image>().sprite = icoO;
                    break;
                case 'X':
                    selections[i].GetComponent<Selection>().smallIco.GetComponent<Image>().sprite = icoX;
                    break;
                case '-':
                    selections[i].GetComponent<Selection>().smallIco.GetComponent<Image>().sprite = icoN;
                    break;
            }

            if(currentIdx == i) selections[i].GetComponent<Image>().sprite = selectedImg;
            else selections[i].GetComponent<Image>().sprite = selectionImg;
            myScrollRect.verticalNormalizedPosition = 1f - (float)currentIdx /(float)maxIdx;
        }
    }


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


    foreach(GameObject obj in SelectionPanel.GetComponent<SetSelection>().selections)
        {
            obj.GetComponent<Button>().onClick.AddListener(() => Goto(System.Array.IndexOf(SelectionPanel.GetComponent<SetSelection>().selections, obj)));
        }


    */
}
