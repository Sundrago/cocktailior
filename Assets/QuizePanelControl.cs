using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizePanelControl : MonoBehaviour
{
    public GameObject glassPanel, garnishPanel, methodePanel, receipePanel, amountPanel, myPanel;
    public int GlassIdx = -1;
    public int GarnishIdx = -1;
    public int MethodeIdx = -1;
    public List<int> AmtIdx = new List<int>();
    public List<int> AmtIdx_2 = new List<int>();
    public List<int> RcpIdx = new List<int>();

    public List<GameObject> rtxamt = new List<GameObject>(7);
    public int rcpCount = 1;
    public int rcpCurrentIdx = -1;
    public int amtCurrentIdx = -1;

    string glass_t, methode_t, garnish_t;
    public List<string> rcp_t = new List<string>();
    public List<string> amt_t = new List<string>();


    public Text glass_ui, methode_ui, garnish_ui, title_ui, description_ui, qIndex_ui;
    public GameObject score_ui, socre_img;

    public int cocktail_i = 0;
    public int quize_i;

    Color a, b, c;

    public Sprite q_s_0, q_s_1, q_m_0, q_m_1, q_l_0, q_l_10, q_l_11, q_l_01;

    public GameObject glassBtn, methodeBtn, garnishBtn;
    public int score;

    public int myScore = -1;


    public void HidePanel()
    {
        GetComponent<MouseDrag>().Start();
        gameObject.SetActive(false);
    }

    public void ShowPanel()
    {
        gameObject.SetActive(true);
        GetComponent<MouseDrag>().Centerize();
    }

    public void TestBtnUpClicked()
    {
        print(cocktail_i);
        cocktail_i += 1;
        LoadCocktail(cocktail_i);
    }
    public void TestBtnDownClicked()
    {
        cocktail_i -= 1;
        LoadCocktail(cocktail_i);
    }

    public void TestBtnCheckClicked()
    {
        QuizeDone();
    }

    public void QuizeDone()
    {
        score = 23;
        gameObject.GetComponent<MouseDrag>().Righted();

        glassPanel.SetActive(false);
        garnishPanel.SetActive(false);
        methodePanel.SetActive(false);
        receipePanel.SetActive(false);
        amountPanel.SetActive(false);

        //glass
        if (glass_ui.text == myPanel.GetComponent<CocktailList>().ReturnGlass(cocktail_i))
        {
            SetBtnColor(0, 0);
        }
        else
        {
            SetBtnColor(0, 1);
            score -= 3;
            print(glass_ui.text + " / " + myPanel.GetComponent<CocktailList>().ReturnGlass(cocktail_i) + " : " + (glass_ui.text == myPanel.GetComponent<CocktailList>().ReturnGlass(cocktail_i)));
        }

        //methode
        if (methode_ui.text == myPanel.GetComponent<CocktailList>().ReturnMethode(cocktail_i))
        {
            SetBtnColor(1, 0);
        }
        else
        {
            SetBtnColor(1, 1);
            score -= 3;
            print(methode_ui.text + " / " + myPanel.GetComponent<CocktailList>().ReturnMethode(cocktail_i) + " : " + (methode_ui.text == myPanel.GetComponent<CocktailList>().ReturnMethode(cocktail_i)));
        }

        //garnish
        if (garnish_ui.text == myPanel.GetComponent<CocktailList>().ReturnGarnish(cocktail_i))
        {
            SetBtnColor(2, 0);
        }
        else if (garnish_ui.text == "?? ??" && myPanel.GetComponent<CocktailList>().ReturnGarnish(cocktail_i) == "?? ?? ?? ??")
        {
            SetBtnColor(2, 0);
        }
        else if (garnish_ui.text == "???? ??? ??" && myPanel.GetComponent<CocktailList>().ReturnGarnish(cocktail_i) == "???? ?? ??? ??? ??")
        {
            SetBtnColor(2, 0);
        }
        else if (garnish_ui.text == "?? ? ????" && myPanel.GetComponent<CocktailList>().ReturnGarnish(cocktail_i) == "?? ?? ?? ? ????")
        {
            SetBtnColor(2, 0);
        }
        else if (garnish_ui.text == "?? ????" && myPanel.GetComponent<CocktailList>().ReturnGarnish(cocktail_i) == "?? ?? ?? ????")
        {
            SetBtnColor(2, 0);
        }
        else
        {
            SetBtnColor(2, 1);
            score -= 3;
            print(garnish_ui.text + " / " + myPanel.GetComponent<CocktailList>().ReturnGarnish(cocktail_i) + " : " + (garnish_ui.text == myPanel.GetComponent<CocktailList>().ReturnGarnish(cocktail_i)));
        }

        List<string> answerRcp = myPanel.GetComponent<CocktailList>().ReturnRcp(cocktail_i);
        List<string> answerAmt = myPanel.GetComponent<CocktailList>().ReturnAmt(cocktail_i);
        List<string> rcps = new List<string>();
        for (int i = 0; i < rcpCount; i++)
        {
            string rcp = rtxamt[i].GetComponent<SetQuizeMenuBar>().ReturnRcp();
            string amt = rtxamt[i].GetComponent<SetQuizeMenuBar>().ReturnAmt();
            rcps.Add(rcp);
            if (answerRcp.Contains(rcp))
            {
                if (amt == answerAmt[answerRcp.IndexOf(rcp)])
                {
                    //correct
                    SetBtnColor(i + 3, 0);
                }
                else
                {
                    //incorrect amt
                    score -= 1;
                    SetBtnColor(i + 3, 1);
                }
            }
            else if (rcp == "????" & answerRcp.Contains("???? ?? ????"))
            {
                if (amt == answerAmt[answerRcp.IndexOf("???? ?? ????")])
                {
                    //correct
                    SetBtnColor(i + 3, 0);
                }
                else
                {
                    //incorrect amt
                    score -= 1;
                    SetBtnColor(i + 3, 1);
                }
            }
            else
            {
                //wrong receipe
                score -= 2;
                SetBtnColor(i + 3, 11);
            }
        }
        int needMore = 0;
        for (int i = 0; i < answerRcp.Count; i++)
        {
            if (!rcps.Contains(answerRcp[i]))
            {
                //Need more
                print("NEED MORE : " + answerRcp[i]);
                score -= 2;
                needMore += 1;
            }
        }

        if (needMore != 0)
        {
            rtxamt[rcpCount].GetComponent<SetQuizeMenuBar>().Setup("wrong", needMore.ToString());
            SetBtnColor(rcpCount + 3, 11);
        }
        else
        {
            rtxamt[rcpCount].GetComponent<SetQuizeMenuBar>().Setup("add", needMore.ToString());
            SetBtnColor(rcpCount + 3, 0);
        }

        myScore = Mathf.RoundToInt(score / 23f * 100);
        print("SCORE : " + myScore);
        score_ui.GetComponent<Text>().text = "" + Mathf.Round(score / 23f * 100);
        score_ui.SetActive(true);
        socre_img.SetActive(true);
    }


    public void SetBtnColor(int i, int j)
    {
        if (i == 0)
        {
            switch (j)
            {
                case 0: glassBtn.GetComponent<Image>().sprite = q_s_0; break;
                case 1: glassBtn.GetComponent<Image>().sprite = q_s_1; break;
                case 2: glassBtn.GetComponent<Image>().color = Color.red; break;
            }
        }
        else if (i == 1)
        {
            switch (j)
            {
                case 0: methodeBtn.GetComponent<Image>().sprite = q_s_0; break;
                case 1: methodeBtn.GetComponent<Image>().sprite = q_s_1; break;
                case 2: methodeBtn.GetComponent<Image>().color = Color.red; break;
            }
        }
        else if (i == 2)
        {
            switch (j)
            {
                case 0: garnishBtn.GetComponent<Image>().sprite = q_m_0; break;
                case 1: garnishBtn.GetComponent<Image>().sprite = q_m_1; break;
                case 2: garnishBtn.GetComponent<Image>().color = Color.red; break;
            }
        }
        else if (i >= 3 && i <= 9)
        {
            int index = i - 3;
            switch (j)
            {
                case 0: rtxamt[index].GetComponent<Image>().sprite = q_l_0; break;
                case 1: rtxamt[index].GetComponent<Image>().sprite = q_l_01; break;
                case 10: rtxamt[index].GetComponent<Image>().sprite = q_l_10; break;
                case 11: rtxamt[index].GetComponent<Image>().sprite = q_l_11; break;
            }
        }


    }
    void Start()
    {
        score_ui.SetActive(false);
        socre_img.SetActive(false);
        AmtIdx.Add(-1);
        AmtIdx_2.Add(-1);
        RcpIdx.Add(-1);
        rcp_t.Add("");
        amt_t.Add("");
        glassPanel.SetActive(false);
        garnishPanel.SetActive(false);
        methodePanel.SetActive(false);
        receipePanel.SetActive(false);
        amountPanel.SetActive(false);
        UpateRcp();
        glass_ui.text = "??? ??";
        garnish_ui.text = "??? ??";
        methode_ui.text = "?? ??";
        qIndex_ui.text = "Q " + quize_i + "/3";
    }

    void UpateRcp()
    {
        for (int i = 0; i < rcpCount; i++)
        {
            rtxamt[i].GetComponent<SetQuizeMenuBar>().Setup("show", "");
            rtxamt[i].GetComponent<SetQuizeMenuBar>().Setup("null", "");
        }
        for (int i = rcpCount; i < 7; i++)
        {
            rtxamt[i].GetComponent<SetQuizeMenuBar>().Setup("hide", "");
        }
        if (rcpCount < 7)
        {
            rtxamt[rcpCount].SetActive(true);
            rtxamt[rcpCount].GetComponent<SetQuizeMenuBar>().Setup("add", "");
        }
    }



    public void UpdateGlass(int idx)
    {
        GlassIdx = idx;
        Debug.Log("GLASS IDX : " + idx);
        switch (idx)
        {
            case -1: glass_t = "??? ??"; break;
            case 0: glass_t = "??? ???"; break;
            case 1: glass_t = "?? ??? ???"; break;
            case 2: glass_t = "??? ??? ???"; break;
            case 3: glass_t = "??? ???"; break;
            case 4: glass_t = "??? ???"; break;
            case 5: glass_t = "?? ???"; break;
            case 6: glass_t = "??? ??? ???"; break;
            case 7: glass_t = "?? ???"; break;
            case 8: glass_t = "??? ???(???)"; break;
            case 9: glass_t = "??? ?? ???"; break;
            case 10: glass_t = "??? ??? ???"; break;
        }
        glass_ui.text = glass_t;
    }

    public void UpdateGarnish(int idx)
    {
        GarnishIdx = idx;
        Debug.Log("GARNISH IDX : " + idx);
        switch (idx)
        {
            case -1: garnish_t = "??? ??"; break;
            case 0: garnish_t = "?? ????"; break;
            case 1: garnish_t = "?? ??"; break;
            case 2: garnish_t = "?? ? ????"; break;
            case 3: garnish_t = "?? ????? ??"; break;
            case 4: garnish_t = "??? ????? ??"; break;
            case 5: garnish_t = "???? ??? ??"; break;
            case 6: garnish_t = "??"; break;
            case 7: garnish_t = "?? ???"; break;
            case 8: garnish_t = "?? ????"; break;
            case 9: garnish_t = "?? ???"; break;
            case 10: garnish_t = "?? ??"; break;
            case 11: garnish_t = "??"; break;
        }
        garnish_ui.text = garnish_t;
    }

    public void UpdateMethode(int idx)
    {
        MethodeIdx = idx;
        Debug.Log("METHODE IDX : " + idx);
        switch (idx)
        {
            case -1: methode_t = "?? ??"; break;
            case 0: methode_t = "???"; break;
            case 1: methode_t = "???"; break;
            case 2: methode_t = "??"; break;
            case 3: methode_t = "???"; break;
            case 4: methode_t = "???"; break;
            case 5: methode_t = "???/ ??"; break;
            case 6: methode_t = "??/ ???"; break;
        }
        methode_ui.text = methode_t;
    }

    public void UpdateRcp(int idx, string name)
    {
        RcpIdx[rcpCurrentIdx] = idx;
        Debug.Log("RCP IDX : " + idx);

        if (idx == -1)
        {
            RcpIdx.RemoveAt(rcpCurrentIdx);
            AmtIdx.RemoveAt(rcpCurrentIdx);
            AmtIdx_2.RemoveAt(rcpCurrentIdx);
            rcp_t.RemoveAt(rcpCurrentIdx);
            amt_t.RemoveAt(rcpCurrentIdx);
            rtxamt[rcpCount].GetComponent<SetQuizeMenuBar>().Setup("hide", "");
            rtxamt[rcpCount - 1].GetComponent<SetQuizeMenuBar>().Setup("add", "");
            rcpCount -= 1;
            for (int i = 0; i < rcpCount; i++)
            {
                if (rcp_t[i] == "")
                {
                    rtxamt[i].GetComponent<SetQuizeMenuBar>().Setup("null", "");
                }
                else
                {
                    rtxamt[i].GetComponent<SetQuizeMenuBar>().Setup(rcp_t[i], amt_t[i]);
                }
            }
        }
        else
        {
            rcp_t[rcpCurrentIdx] = name;
            if (amt_t[rcpCurrentIdx] == "")
            {
                rtxamt[rcpCurrentIdx].GetComponent<SetQuizeMenuBar>().Setup(name, "oz");
            }
            else
            {
                rtxamt[rcpCurrentIdx].GetComponent<SetQuizeMenuBar>().Setup(name, amt_t[rcpCurrentIdx]);
            }
        }
    }

    public void UpdateAmt(int idx, int idx2, string add)
    {
        AmtIdx[amtCurrentIdx] = idx;
        AmtIdx_2[amtCurrentIdx] = idx2;
        Debug.Log("RCP IDX : " + idx + " , " + idx2);

        switch (idx)
        {
            case -1: amt_t[amtCurrentIdx] = ""; break;
            case 0: amt_t[amtCurrentIdx] = "1/4"; break;
            case 1: amt_t[amtCurrentIdx] = "1/3"; break;
            case 2: amt_t[amtCurrentIdx] = "1/2"; break;
            case 3: amt_t[amtCurrentIdx] = "3/4"; break;
            case 4: amt_t[amtCurrentIdx] = "1"; break;
            case 5: amt_t[amtCurrentIdx] = "1 1/4"; break;
            case 6: amt_t[amtCurrentIdx] = "1 1/3"; break;
            case 7: amt_t[amtCurrentIdx] = "1 1/2"; break;
            case 8: amt_t[amtCurrentIdx] = "1 3/4"; break;
            case 9: amt_t[amtCurrentIdx] = "2"; break;
            case 10: amt_t[amtCurrentIdx] = "2 1/4"; break;
            case 11: amt_t[amtCurrentIdx] = "2 1/3"; break;
            case 12: amt_t[amtCurrentIdx] = "2 1/2"; break;
            case 13: amt_t[amtCurrentIdx] = "2 3/4"; break;
            case 14: amt_t[amtCurrentIdx] = "3"; break;
        }
        switch (idx2)
        {
            case 0: amt_t[amtCurrentIdx] += " oz"; break;
            case 1: amt_t[amtCurrentIdx] += " tsp"; break;
            case 2: amt_t[amtCurrentIdx] += " dash"; break;
            case 3: amt_t[amtCurrentIdx] += " part"; break;
            case 4: amt_t[amtCurrentIdx] += " ea"; break;
            case 5: amt_t[amtCurrentIdx] += "Fill Up"; break;
            case 6: amt_t[amtCurrentIdx] += " oz On Top"; break;
        }

        if(add != "")
            amt_t[amtCurrentIdx] += add;

        rtxamt[amtCurrentIdx].GetComponent<SetQuizeMenuBar>().Setup(rcp_t[amtCurrentIdx], amt_t[amtCurrentIdx]);

    }

    public void GlassBtnClicked()
    {
        glassPanel.SetActive(true);
        glassPanel.GetComponent<Animator>().SetTrigger("show");
    }

    public void GarnishBtnClicked()
    {
        garnishPanel.SetActive(true);
        garnishPanel.GetComponent<Animator>().SetTrigger("show");
    }

    public void MethodeBtnClicked()
    {
        methodePanel.SetActive(true);
        methodePanel.GetComponent<Animator>().SetTrigger("show");
    }

    public void receipeBtnClicked(int i)
    {
        if (i == rcpCount)
        {
            if (rcpCount < 6)
            {
                rtxamt[rcpCount].GetComponent<SetQuizeMenuBar>().Setup("null", "");
                rtxamt[rcpCount + 1].SetActive(true);
                rtxamt[rcpCount + 1].GetComponent<SetQuizeMenuBar>().Setup("add", "");
                AmtIdx.Add(-1);
                AmtIdx_2.Add(-1);
                RcpIdx.Add(-1);
                rcpCount += 1;
                rcp_t.Add("");
                amt_t.Add("");
            }
            else if (rcpCount == 6)
            {
                rtxamt[rcpCount].GetComponent<SetQuizeMenuBar>().Setup("null", "");
                AmtIdx.Add(-1);
                AmtIdx_2.Add(-1);
                RcpIdx.Add(-1);
                rcp_t.Add("");
                amt_t.Add("");
                rcpCount += 1;
            }
        }
        else
        {
            rcpCurrentIdx = i;
            receipePanel.GetComponent<GlassPanelControl>().Start();
            receipePanel.SetActive(true);
            receipePanel.GetComponent<Animator>().SetTrigger("show");
        }
    }

    public void amountBtnClicked(int i)
    {
        print("AMT : " + i);
        if (i == rcpCount)
        {
            if (rcpCount < 6)
            {
                rtxamt[rcpCount].GetComponent<SetQuizeMenuBar>().Setup("null", "");
                rtxamt[rcpCount + 1].SetActive(true);
                rtxamt[rcpCount + 1].GetComponent<SetQuizeMenuBar>().Setup("add", "");
                AmtIdx.Add(-1);
                AmtIdx_2.Add(-1);
                RcpIdx.Add(-1);
                rcp_t.Add("");
                amt_t.Add("");
                rcpCount += 1;
            }
            else if (rcpCount == 6)
            {
                rtxamt[rcpCount].GetComponent<SetQuizeMenuBar>().Setup("null", "");
                AmtIdx.Add(-1);
                AmtIdx_2.Add(-1);
                RcpIdx.Add(-1);
                rcp_t.Add("");
                amt_t.Add("");
                rcpCount += 1;
            }
        }
        else
        {
            amtCurrentIdx = i;
            amountPanel.GetComponent<GlassPanelControl>().Start();
            amountPanel.GetComponent<GlassPanelControl>().updateInfo = true;
            amountPanel.GetComponent<GlassPanelControl>().AmtClicked(AmtIdx[amtCurrentIdx]);
            amountPanel.GetComponent<GlassPanelControl>().MtClicked(AmtIdx_2[amtCurrentIdx]);
            amountPanel.GetComponent<GlassPanelControl>().updateInfo = false;
            amountPanel.SetActive(true);
            amountPanel.GetComponent<Animator>().SetTrigger("show");
        }
    }

    public void LoadCocktail(int index)
    {
        print("LoadCocktail Info : " + index);
        myPanel.GetComponent<CocktailList>().Start();
        cocktail_i = index;
        title_ui.text = myPanel.GetComponent<CocktailList>().ReturnName(index);
        description_ui.text = myPanel.GetComponent<CocktailList>().ReturnDescription(index);
    }
}
