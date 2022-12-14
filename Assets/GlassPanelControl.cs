using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlassPanelControl : MonoBehaviour
{
    public GameObject ParentPanel, Rcp2Panel;
    public List<Button> glassButtonIdx = new List<Button>(11);
    public List<Button> glassButtonIdx_2 = new List<Button>(7);
    public int selected = -1;
    public int selected_2 = -1;
    public string setup;
    public bool updateInfo = false;
    public bool started = false;
    Color defaultColor, selectedColor;

    private bool fillUp, onTop;

    public void Start()
    {
        fillUp = false;
        onTop = false;

        if(started) return;
        started = true;
        if(setup == "garnish") {
            glassButtonIdx[0].onClick.AddListener(() => BtnClicked(0));
            glassButtonIdx[1].onClick.AddListener(() => BtnClicked(1));
            glassButtonIdx[2].onClick.AddListener(() => BtnClicked(2));
            glassButtonIdx[3].onClick.AddListener(() => BtnClicked(3));
            glassButtonIdx[4].onClick.AddListener(() => BtnClicked(4));
            glassButtonIdx[5].onClick.AddListener(() => BtnClicked(5));
            glassButtonIdx[6].onClick.AddListener(() => BtnClicked(6));
            glassButtonIdx[7].onClick.AddListener(() => BtnClicked(7));
            glassButtonIdx[8].onClick.AddListener(() => BtnClicked(8));
            glassButtonIdx[9].onClick.AddListener(() => BtnClicked(9));
            glassButtonIdx[10].onClick.AddListener(() => BtnClicked(10));
            glassButtonIdx[11].onClick.AddListener(() => BtnClicked(11));
        } else if(setup == "glass" ) {
            glassButtonIdx[0].onClick.AddListener(() => BtnClicked(0));
            glassButtonIdx[1].onClick.AddListener(() => BtnClicked(1));
            glassButtonIdx[2].onClick.AddListener(() => BtnClicked(2));
            glassButtonIdx[3].onClick.AddListener(() => BtnClicked(3));
            glassButtonIdx[4].onClick.AddListener(() => BtnClicked(4));
            glassButtonIdx[5].onClick.AddListener(() => BtnClicked(5));
            glassButtonIdx[6].onClick.AddListener(() => BtnClicked(6));
            glassButtonIdx[7].onClick.AddListener(() => BtnClicked(7));
            glassButtonIdx[8].onClick.AddListener(() => BtnClicked(8));
            glassButtonIdx[9].onClick.AddListener(() => BtnClicked(9));
            glassButtonIdx[10].onClick.AddListener(() => BtnClicked(10));
        }else if(setup == "methode") {
            glassButtonIdx[0].onClick.AddListener(() => BtnClicked(0));
            glassButtonIdx[1].onClick.AddListener(() => BtnClicked(1));
            glassButtonIdx[2].onClick.AddListener(() => BtnClicked(2));
            glassButtonIdx[3].onClick.AddListener(() => BtnClicked(3));
            glassButtonIdx[4].onClick.AddListener(() => BtnClicked(4));
            glassButtonIdx[5].onClick.AddListener(() => BtnClicked(5));
            glassButtonIdx[6].onClick.AddListener(() => BtnClicked(6));
        } else if(setup == "amt") {
            glassButtonIdx[0].onClick.AddListener(() => AmtClicked(0));
            glassButtonIdx[1].onClick.AddListener(() => AmtClicked(1));
            glassButtonIdx[2].onClick.AddListener(() => AmtClicked(2));
            glassButtonIdx[3].onClick.AddListener(() => AmtClicked(3));
            glassButtonIdx[4].onClick.AddListener(() => AmtClicked(4));
            glassButtonIdx[5].onClick.AddListener(() => AmtClicked(5));
            glassButtonIdx[6].onClick.AddListener(() => AmtClicked(6));
            glassButtonIdx[7].onClick.AddListener(() => AmtClicked(7));
            glassButtonIdx[8].onClick.AddListener(() => AmtClicked(8));
            glassButtonIdx[9].onClick.AddListener(() => AmtClicked(9));
            glassButtonIdx[10].onClick.AddListener(() => AmtClicked(10));
            glassButtonIdx[11].onClick.AddListener(() => AmtClicked(11));
            glassButtonIdx[12].onClick.AddListener(() => AmtClicked(12));
            glassButtonIdx[13].onClick.AddListener(() => AmtClicked(13));
            glassButtonIdx[14].onClick.AddListener(() => AmtClicked(14));

            glassButtonIdx_2[0].onClick.AddListener(() => MtClicked(0));
            glassButtonIdx_2[1].onClick.AddListener(() => MtClicked(1));
            glassButtonIdx_2[2].onClick.AddListener(() => MtClicked(2));
            glassButtonIdx_2[3].onClick.AddListener(() => MtClicked(3));
            glassButtonIdx_2[4].onClick.AddListener(() => MtClicked(4));
            glassButtonIdx_2[5].onClick.AddListener(() => FillUpBtn(0));
            glassButtonIdx_2[6].onClick.AddListener(() => FillUpBtn(1));
        } else if(setup == "rcp") {
            print("???");
            Rcp2Panel.SetActive(false);
            glassButtonIdx[0].onClick.AddListener(() => RcpClicked(0));
            glassButtonIdx[1].onClick.AddListener(() => RcpClicked(1));
            glassButtonIdx[2].onClick.AddListener(() => RcpClicked(2));
            glassButtonIdx[3].onClick.AddListener(() => RcpClicked(3));
            glassButtonIdx[4].onClick.AddListener(() => RcpClicked(4));
            glassButtonIdx[5].onClick.AddListener(() => RcpClicked(5));
            glassButtonIdx[6].onClick.AddListener(() => RcpClicked(6));
            glassButtonIdx[7].onClick.AddListener(() => RcpClicked(7));

            glassButtonIdx_2[0].onClick.AddListener(() => Rcp2Clicked(0));
            glassButtonIdx_2[1].onClick.AddListener(() => Rcp2Clicked(1));
            glassButtonIdx_2[2].onClick.AddListener(() => Rcp2Clicked(2));
            glassButtonIdx_2[3].onClick.AddListener(() => Rcp2Clicked(3));
            glassButtonIdx_2[4].onClick.AddListener(() => Rcp2Clicked(4));
            glassButtonIdx_2[5].onClick.AddListener(() => Rcp2Clicked(5));
            glassButtonIdx_2[6].onClick.AddListener(() => Rcp2Clicked(6));
            glassButtonIdx_2[7].onClick.AddListener(() => Rcp2Clicked(7));
            glassButtonIdx_2[8].onClick.AddListener(() => Rcp2Clicked(8));
            glassButtonIdx_2[9].onClick.AddListener(() => Rcp2Clicked(9));
            glassButtonIdx_2[10].onClick.AddListener(() => Rcp2Clicked(10));
            glassButtonIdx_2[11].onClick.AddListener(() => Rcp2Clicked(11));
            glassButtonIdx_2[12].onClick.AddListener(() => Rcp2Clicked(12));
            glassButtonIdx_2[13].onClick.AddListener(() => Rcp2Clicked(13));
            glassButtonIdx_2[14].onClick.AddListener(() => Rcp2Clicked(14));
            glassButtonIdx_2[15].onClick.AddListener(() => Rcp2Clicked(15));
            glassButtonIdx_2[16].onClick.AddListener(() => Rcp2Clicked(16));
            glassButtonIdx_2[17].onClick.AddListener(() => Rcp2Clicked(17));
            glassButtonIdx_2[18].onClick.AddListener(() => Rcp2Clicked(18));
            glassButtonIdx_2[19].onClick.AddListener(() => Rcp2Clicked(19));
            glassButtonIdx_2[20].onClick.AddListener(() => Rcp2Clicked(20));
            glassButtonIdx_2[21].onClick.AddListener(() => Rcp2Clicked(21));
        }
        

        defaultColor = glassButtonIdx[0].GetComponent<Image>().color;
        selectedColor = defaultColor;
        defaultColor.a = 0.35f;
    }

    private void FillUpBtn(int i)
    {
        if(i==0)
        {
            fillUp = !fillUp;
            if (fillUp) onTop = false;
        } else if (i==1)
        {
            onTop = !onTop;
            if (onTop) fillUp = false;
        }

        if(fillUp) glassButtonIdx_2[5].GetComponent<Image>().color = selectedColor;
        else glassButtonIdx_2[5].GetComponent<Image>().color = defaultColor;

        if (onTop) glassButtonIdx_2[6].GetComponent<Image>().color = selectedColor;
        else glassButtonIdx_2[6].GetComponent<Image>().color = defaultColor;
    }

    public void BtnClicked(int idx){
        selected = idx;
        if(selected != -1) {
            for(int i = 0; i < glassButtonIdx.Count; i++){
                glassButtonIdx[i].GetComponent<Image>().color = defaultColor;
            }
            glassButtonIdx[idx].GetComponent<Image>().color = selectedColor;
        }
        if(!updateInfo) ExitBtnClicked();
    }

    public void AmtClicked(int idx){
        selected = idx;
        for(int i = 0; i < glassButtonIdx.Count; i++){
            glassButtonIdx[i].GetComponent<Image>().color = defaultColor;
        }
        if(idx!=-1) glassButtonIdx[idx].GetComponent<Image>().color = selectedColor;
        if(!updateInfo) if(selected != -1 & selected_2 != -1 & setup != "amt") ExitBtnClicked();
    }

    public void MtClicked(int idx){
        if(idx == selected_2)   idx = -1;

        selected_2 = idx;
        for(int i = 0; i < glassButtonIdx_2.Count; i++){
            glassButtonIdx_2[i].GetComponent<Image>().color = defaultColor;
        }
        FillUpBtn(-1);
        if (idx!=-1) glassButtonIdx_2[idx].GetComponent<Image>().color = selectedColor;
        if(selected != -1 & selected_2 != -1 & !updateInfo) {
            //ExitBtnClicked();
            return;
        }
        if(!updateInfo & selected_2==5) {
            selected = -1;
            //ExitBtnClicked();
        }
    }

    public void RcpClicked(int idx){
        if(selected != idx) selected_2 = -1;
        selected = idx;
        for(int i = 0; i < glassButtonIdx.Count; i++){
            glassButtonIdx[i].GetComponent<Image>().color = defaultColor;
        }
        if(idx!=-1) glassButtonIdx[idx].GetComponent<Image>().color = selectedColor;

        List<string> rcp2 = new List<string>();
        switch(idx){
            //??????
            case 0: 
                rcp2.Add("?????????");
                rcp2.Add("????????? ???");
                rcp2.Add("????????? ???");
                rcp2.Add("????????? ????????? ???");
                rcp2.Add("?????? ?????????");
                rcp2.Add("?????????");
                rcp2.Add("?????????");
                rcp2.Add("????????? ?????????");
                rcp2.Add("?????? ?????????");
                break;
            //?????????
            case 1:
                rcp2.Add("?????? ????????????");
                rcp2.Add("????????? ????????????");
                rcp2.Add("????????????");
                rcp2.Add("?????????(?????? ?????????)");
                rcp2.Add("????????? ?????????");
                rcp2.Add("???????????? DOM");
                rcp2.Add("???????????? ???????????? ??????");
                rcp2.Add("?????? ?????????");
                rcp2.Add("????????? ????????????");
                rcp2.Add("???????????? ?????????");
                rcp2.Add("????????????");
                rcp2.Add("?????? ?????????");
                rcp2.Add("?????????");
                rcp2.Add("?????? ?????????");
                rcp2.Add("????????? ???");
                rcp2.Add("???????????? ?????? ????????????");
                rcp2.Add("?????? ??? ??????(??????)");
                rcp2.Add("?????? ??? ??????(?????????)");
                rcp2.Add("?????? ??? ?????????");
                rcp2.Add("?????? ??? ?????????(?????????)");
                rcp2.Add("?????? ??? ?????????(?????????)");
                rcp2.Add("????????????");
                break;
            case 2:
                rcp2.Add("???????????? ??????");
                rcp2.Add("???????????? ??????");
                rcp2.Add("?????? ??? ?????? ??????");
                rcp2.Add("??????????????? ??????");
                rcp2.Add("????????? ??????");
                rcp2.Add("??????????????? ??????");
                break;
            case 3:
                rcp2.Add("?????? ??????");
                rcp2.Add("?????? ??????");
                rcp2.Add("?????? ???");
                rcp2.Add("?????? ??????");
                rcp2.Add("????????? ??????");
                rcp2.Add("?????? ??????");
                rcp2.Add("????????? ??????");
                rcp2.Add("???????????? ??????");
                rcp2.Add("???????????? ??????");
                break;
            case 4:
                rcp2.Add("?????? ??????");
                rcp2.Add("???????????????");
                rcp2.Add("??????");
                rcp2.Add("????????????");
                rcp2.Add("??????");
                rcp2.Add("?????? ??????");
                break;
            case 5:
                rcp2.Add("????????? ????????????");
                rcp2.Add("????????? ????????????");
                rcp2.Add("????????? ??????");
                break;
            case 6:
                rcp2.Add("?????????");
                rcp2.Add("?????? ?????????");
                rcp2.Add("????????? ????????????");
                rcp2.Add("?????? ??????");
                rcp2.Add("?????? ??????");
                break;
            case 7:
                ParentPanel.GetComponent<QuizePanelControl>().UpdateRcp(-1,"");
                ExitBtnClicked();
                return;
        }
        //ParentPanel.GetComponent<QuizePanelControl>().UpdateRcp(idx);

        for(int i = 0; i<rcp2.Count; i++){
            glassButtonIdx_2[i].GetComponent<RcpBtnControl>().ChangeText(rcp2[i]);
        }
        for(int i = rcp2.Count; i<glassButtonIdx_2.Count; i++) {
            glassButtonIdx_2[i].GetComponent<RcpBtnControl>().ChangeText("hide");
        }

        Rcp2Panel.SetActive(true);
        if(selected != -1 & selected_2 != -1 & !updateInfo) ExitBtnClicked();
    }
    
    public void Rcp2Clicked(int idx){
        ParentPanel.GetComponent<QuizePanelControl>().UpdateRcp(idx,glassButtonIdx_2[idx].GetComponent<RcpBtnControl>().ReturnText());
        if(setup != "amt") ExitBtnClicked();
    }

    public void ExitBtnClicked(){
        if(setup == "glass") {
            ParentPanel.GetComponent<QuizePanelControl>().UpdateGlass(selected);
        } else if(setup == "garnish"){
            ParentPanel.GetComponent<QuizePanelControl>().UpdateGarnish(selected);
        } else if(setup == "methode"){
            ParentPanel.GetComponent<QuizePanelControl>().UpdateMethode(selected);
        } else if(setup == "amt"){
            if (fillUp)
            {
                if(selected_2 == -1) ParentPanel.GetComponent<QuizePanelControl>().UpdateAmt(selected, selected_2, "Fill Up");
                else ParentPanel.GetComponent<QuizePanelControl>().UpdateAmt(selected, selected_2, " Fill Up");
            }
            else if (onTop) ParentPanel.GetComponent<QuizePanelControl>().UpdateAmt(selected, selected_2, " On Top");
            else ParentPanel.GetComponent<QuizePanelControl>().UpdateAmt(selected, selected_2, "");
        } else if(setup == "rcp"){
            Rcp2Panel.SetActive(false);
        }
        
        GetComponent<Animator>().SetTrigger("hide");
    }

    public void RcpBackClicked(){
        selected_2 = -1;
        Rcp2Panel.SetActive(false);
    }

}
