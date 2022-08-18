using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainControl : MonoBehaviour
{
    public List<int> currentStageIdx = new List<int>(39);
    public int currentIdx;
    public int maxIdx;
    public GameObject myPanel, quizePanel;
    public GameObject myCanvas;
    public Button Xbtn, Obtn;
    public GameObject panel_holder; 
    public GameObject panel_holder_front;

    GameObject newPanel;
    public List<GameObject> newQuize;

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
    public GameObject SearchPanel, TestPanel, MoaPanel, timer_ui, popupPanel, popupText, Test_before, Test_ing, Test_done, underPanelD, underPanelQ, underPanelQ2, showAnsBtnText;
    int currentQ;
    bool quizeMode = false;
    bool ansMode = false;
    float startTime, timer;
    string ask;
    public Text QLeft_ui, QRight_ui, testShowBtn1, testShowBtn2, testShowBtn3;
    public bool answerShowing = false;

    public Text ansFinalScore;
    public List<Text> ansName = new List<Text>(3);
    public List<Text> ansScore = new List<Text>(3);
    public bool failed = false;
    // Start is called before the first frame update
    GameObject ansPanel;
    public bool rewardAdShown;

    void Start()
    {
        timer_ui.SetActive(false);
        popupPanel.SetActive(false);
        Test_before.SetActive(true);
        Test_ing.SetActive(false);
        Test_done.SetActive(false);
        underPanelD.SetActive(true);
        underPanelQ.SetActive(false);
        underPanelQ2.SetActive(false);

        SearchPanel.SetActive(false);
        MoaPanel.SetActive(false);
        TestPanel.SetActive(false);

        Application.targetFrameRate = 60;
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

    void Update(){
        if(failed) return;
        if(quizeMode) {
            timer = 420 + startTime - Time.time;
            if(timer > 60) {
                timer_ui.GetComponent<Text>().text = "" + Mathf.FloorToInt(timer/60) + "분 " + Mathf.FloorToInt(timer%60) + "초";
            } else if(timer > 0) timer_ui.GetComponent<Text>().text = "" + Mathf.FloorToInt(timer%60) + "초";
            else {
                timer_ui.GetComponent<Text>().text = "시간 종료";
                failed = true;
                FinishTest();
            }
        }
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

    public void OpenQuize(){
        UpdateQuizeUnderUI();
        underPanelD.SetActive(false);
        underPanelQ.SetActive(true);
        underPanelQ2.SetActive(false);
        Test_before.SetActive(false);
        Test_ing.SetActive(true);
        Test_done.SetActive(false);
        quizeMode = true;
        startTime = Time.time;
        timer_ui.SetActive(true);
        menu_btn_clicked_ad();
        rewardAdShown = false;

        newPanel.SetActive(false);
        newQuize = new List<GameObject>();
        currentQ = 0;

        newQuize.Add(GameObject.Instantiate(quizePanel, new Vector3(quizePanel.transform.position.x, quizePanel.transform.position.y, quizePanel.transform.position.z), Quaternion.identity, panel_holder.transform));
        newQuize[0].GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 30, 0);
        newQuize[0].transform.localScale = new Vector3(1f, 1f, 1f);
        newQuize[0].GetComponent<QuizePanelControl>().quize_i = 1;

        newQuize.Add(GameObject.Instantiate(quizePanel, new Vector3(quizePanel.transform.position.x, quizePanel.transform.position.y, quizePanel.transform.position.z), Quaternion.identity, panel_holder.transform));
        newQuize[1].GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 30, 0);
        newQuize[1].transform.localScale = new Vector3(1f, 1f, 1f);
        newQuize[1].GetComponent<QuizePanelControl>().quize_i = 2;
        newQuize[1].GetComponent<QuizePanelControl>().HidePanel();

        newQuize.Add(GameObject.Instantiate(quizePanel, new Vector3(quizePanel.transform.position.x, quizePanel.transform.position.y, quizePanel.transform.position.z), Quaternion.identity, panel_holder.transform));
        newQuize[2].GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 30, 0);
        newQuize[2].transform.localScale = new Vector3(1f, 1f, 1f);
        newQuize[2].GetComponent<QuizePanelControl>().quize_i = 3;
        newQuize[2].GetComponent<QuizePanelControl>().HidePanel();
        
        int rnd1, rnd2, rnd3;
        rnd1 = Random.Range(0,38);
        for(;;){
            rnd2 = Random.Range(0,38);
            if(rnd1 != rnd2) break;
        }
        for(;;){
            rnd3 = Random.Range(0,38);
            if(rnd3 != rnd1 & rnd3 != rnd2) break;
        }
        newQuize[0].GetComponent<QuizePanelControl>().LoadCocktail(rnd1);
        newQuize[1].GetComponent<QuizePanelControl>().LoadCocktail(rnd2);
        newQuize[2].GetComponent<QuizePanelControl>().LoadCocktail(rnd3);
        failed = false;
    }

    void UpdateQuizeUnderUI(){
        if(currentQ == 0) {
            QLeft_ui.text = "종료 하기";
            QRight_ui.text = "다음 문제";
        } else if(currentQ == 1) {
            QLeft_ui.text = "이전 문제";
            QRight_ui.text = "다음 문제";
        } else if(currentQ == 2) {
            QLeft_ui.text = "이전 문제";
            QRight_ui.text = "답안 제출";
        }
    }

    void OpenCard(int i, string anim)
    {
        gameObject.GetComponent<TutorialControl>().UpdateStatus(1);

        currentIdx = i;
        newPanel = GameObject.Instantiate(myPanel, new Vector3(myPanel.transform.position.x, myPanel.transform.position.y, myPanel.transform.position.z), Quaternion.identity, panel_holder.transform);
        //newPanel.transform.SetParent(myCanvas.transform);
        newPanel.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 30, 0);
        newPanel.GetComponent<CocktailList>().Start();
        newPanel.GetComponent<CocktailList>().current_receipe = currentIdx;
        newPanel.GetComponent<CocktailList>().max_receipe = maxIdx;
        newPanel.GetComponent<CocktailList>().UpdateCocktailInfo(currentStageIdx[i]);
        if (!questionMode) newPanel.GetComponent<CocktailList>().showAll();
        newPanel.transform.localScale = new Vector3(1f, 1f, 1f);
        newPanel.GetComponent<PanelAnimControl>().PlayAnim(anim);

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
        UpdateSelection();
        newPanel.GetComponent<MouseDrag>().SetCloseBtn();
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
        gameObject.GetComponent<TutorialControl>().UpdateStatus(3);
        if (!newPanel.GetComponent<CocktailList>().O_show)
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
        gameObject.GetComponent<TutorialControl>().UpdateStatus(2);
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
        GoBefore();
    }

    public void Left()
    {
        GoNext();
    }

    public void QLeft(){
        if(ansMode) {
            menu_btn_clicked_ad();
            return;
        }
        if(currentQ == 0 || currentQ == 1) {
            newQuize[currentQ+1].GetComponent<QuizePanelControl>().ShowPanel();
            newQuize[currentQ+1].GetComponent<PanelAnimControl>().PlayAnim("old");
            currentQ += 1;
        } else if(currentQ == 2){
            Ask("done");
            newQuize[2].GetComponent<QuizePanelControl>().ShowPanel();
            newQuize[2].GetComponent<PanelAnimControl>().PlayAnim("new");
        }
        UpdateQuizeUnderUI();
    }

    public void QRight(){
        if(ansMode) {
            menu_btn_clicked_ad();
            return;
        }
        if(currentQ == 0) {
            Ask("quit");
            newQuize[0].GetComponent<QuizePanelControl>().ShowPanel();
            newQuize[0].GetComponent<PanelAnimControl>().PlayAnim("old");
        } else if(currentQ == 1 || currentQ == 2) {
            newQuize[currentQ-1].GetComponent<QuizePanelControl>().ShowPanel();
            newQuize[currentQ-1].GetComponent<PanelAnimControl>().PlayAnim("new");
            currentQ -= 1;
        }
        UpdateQuizeUnderUI();
    }

    public void QLeftBtnClicked(){
        newQuize[currentQ].GetComponent<MouseDrag>().Righted();
    }

    public void QRightBtnClicked(){
        newQuize[currentQ].GetComponent<MouseDrag>().Lefted();
    }

    public void menu_btn_clicked()
    {
        gameObject.GetComponent<TutorialControl>().UpdateStatus(4);
        gameObject.GetComponent<AdControl>().ShowShortAds("menu_btn");
    }

    public void menu_btn_clicked_ad()
    {
        if (quizeMode) TestBtnClicked();
        if (!showMenu)
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

    public void toggleQmode(bool show)
    {
        if(quizeMode) {
            Ask("quit");
            return;
        }
        if(SearchPanel.activeSelf)
        {
            SearchPanel.SetActive(false);
        }
        if(TestPanel.activeSelf)
        {
            TestPanel.SetActive(false);
        }
        if(MoaPanel.activeSelf)
        {
            MoaPanel.SetActive(false);
        }

        if (show)
        {
            menu_selctor.GetComponent<menu_selector>().SetTarget(0);
            questionMode = false;
            for(int i =0; i<5; i++)
            {
                myPanel.GetComponent<CocktailList>().ShowBox(i);
                newPanel.GetComponent<CocktailList>().ShowBox(i);
            }
            
        } else
        {
            menu_selctor.GetComponent<menu_selector>().SetTarget(1);
            questionMode = true;
            for (int i = 0; i < 5; i++)
            {
                myPanel.GetComponent<CocktailList>().HideBox(i);
                newPanel.GetComponent<CocktailList>().HideBox(i);
            }
        }
        //menu_btn_clicked();
    }


    public void TestBtnClicked()
    {
        menu_selctor.GetComponent<menu_selector>().SetTarget(2);
        SearchPanel.SetActive(false);
        MoaPanel.SetActive(false);
        TestPanel.SetActive(true);
    }

    public void SearchBtnClicked()
    {
        if(quizeMode) {
            Ask("quit");
            return;
        }
        menu_selctor.GetComponent<menu_selector>().SetTarget(3);
        SearchPanel.SetActive(true);
        MoaPanel.SetActive(false);
        TestPanel.SetActive(false);

    }

    public void ShowAllBtnClicked()
    {
        if(quizeMode) {
            Ask("quit");
            return;
        }
        menu_selctor.GetComponent<menu_selector>().SetTarget(4);
        SearchPanel.SetActive(false);
        MoaPanel.SetActive(true);
        TestPanel.SetActive(false);
    }

    public void ShowPopup(string input) {
        Time.timeScale = 0;
        popupText.GetComponent<Text>().text = input;
        popupPanel.SetActive(true);
    }

    public void HidePopup(){
        Time.timeScale = 1;
        popupPanel.SetActive(false);
    }

    public void popupYesClicked(){
        if(ask == "quit"){
            QuitTest();
        } else if(ask == "done"){
            FinishTest();
        } 
        HidePopup();
    }

    public void popupNoClicked(){
        HidePopup();
    }

    public void Ask(string what){
        if(what == "quit") {
            ShowPopup("진행중인 시험이 있습니다.\n종료하시겠습니까?");
            ask = "quit";
        } else if(what == "done") {
            ShowPopup("답안을 제출하시겠습니까?");
            ask = "done";
        }
    }

    public void QuitTestBtnClicked(){
        Ask("quit");
    }

    public void FinishTestBtnClicked(){
        Ask("done");
    }

    public void ContinueTestBtnClicked(){
        menu_btn_clicked_ad();
    }
    public void QuitTest(){
        underPanelD.SetActive(true);
        underPanelQ.SetActive(false);
        underPanelQ2.SetActive(false);
        quizeMode = false;
        ansMode = false;
        timer_ui.SetActive(false);
        Destroy(newQuize[2]);
        Destroy(newQuize[1]);
        Destroy(newQuize[0]);
        newPanel.SetActive(true);

        Test_before.SetActive(true);
        Test_ing.SetActive(false);
        Test_done.SetActive(false);
    }

    public void FinishTest(){
        quizeMode = true;
        ansMode = true;
        timer_ui.SetActive(false);
        underPanelD.SetActive(false);
        underPanelQ.SetActive(false);
        underPanelQ2.SetActive(true);

        newQuize[0].GetComponent<QuizePanelControl>().QuizeDone();
        newQuize[1].GetComponent<QuizePanelControl>().QuizeDone();
        newQuize[2].GetComponent<QuizePanelControl>().QuizeDone();

        Test_before.SetActive(false);
        Test_ing.SetActive(false);
        Test_done.SetActive(true);
        Test_done.GetComponent<Animator>().SetTrigger("show");

        int totalScore = newQuize[0].GetComponent<QuizePanelControl>().myScore;
        totalScore += newQuize[1].GetComponent<QuizePanelControl>().myScore;
        totalScore += newQuize[2].GetComponent<QuizePanelControl>().myScore;


        TestShowBtnDeactive();

        if (totalScore >= 210) ansFinalScore.text = "테스트 결과 - 합격!";
        else ansFinalScore.text = "테스트 결과 - 불합격";
        ansName[0].text = "1. " + newQuize[0].GetComponent<QuizePanelControl>().title_ui.text;
        ansName[1].text = "2. " + newQuize[1].GetComponent<QuizePanelControl>().title_ui.text;
        ansName[2].text = "3. " + newQuize[2].GetComponent<QuizePanelControl>().title_ui.text;

        ansScore[0].text = "" + newQuize[0].GetComponent<QuizePanelControl>().myScore +"점";
        ansScore[1].text = "" + newQuize[1].GetComponent<QuizePanelControl>().myScore +"점";
        ansScore[2].text = "" + newQuize[2].GetComponent<QuizePanelControl>().myScore +"점";
    }

    public void CheckQuize(int i){
        if(!rewardAdShown)
        {
            rewardAdShown = true;
            TestShowBtnActive();
            gameObject.GetComponent<AdControl>().ShowRewardAds("CheckQuize" + i);
        } else
        {
            currentQ = i;
            newQuize[i].GetComponent<QuizePanelControl>().ShowPanel();
            newQuize[i].GetComponent<PanelAnimControl>().PlayAnim("old");
            if (showMenu) menu_btn_clicked_ad();
        } 
    }

    public void TestShowBtnActive()
    {
        testShowBtn1.text = "답안 확인!";
        testShowBtn2.text = "답안 확인!";
        testShowBtn3.text = "답안 확인!";
    }

    public void TestShowBtnDeactive()
    {
        testShowBtn1.text = "광고보고 답안확인";
        testShowBtn2.text = "광고보고 답안확인";
        testShowBtn3.text = "광고보고 답안확인";
    }

    public void CheckAnswer(){
        if (answerShowing)
        {
            ansPanel.GetComponent<MouseDrag>().Lefted();
            newQuize[currentQ].GetComponent<PanelAnimControl>().PlayAnim("left");
            showAnsBtnText.GetComponent<Text>().text = "정답 확인";
            return;
        }
        showAnsBtnText.GetComponent<Text>().text = "답안 확인";
        answerShowing = true;
        currentIdx = newQuize[currentQ].GetComponent<QuizePanelControl>().cocktail_i;

        ansPanel = GameObject.Instantiate(myPanel, new Vector3(myPanel.transform.position.x, myPanel.transform.position.y, myPanel.transform.position.z), Quaternion.identity, panel_holder.transform);
        //newPanel.transform.SetParent(myCanvas.transform);
        ansPanel.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 30, 0);
        ansPanel.GetComponent<CocktailList>().Start();
        ansPanel.GetComponent<CocktailList>().current_receipe = currentQ+1;
        ansPanel.GetComponent<CocktailList>().max_receipe = 3;
        ansPanel.GetComponent<CocktailList>().UpdateCocktailInfo(currentIdx);
        ansPanel.GetComponent<CocktailList>().showAll();
        ansPanel.transform.localScale = new Vector3(1f, 1f, 1f);
        ansPanel.GetComponent<PanelAnimControl>().PlayAnim("new");
        ansPanel.GetComponent<MouseDrag>().ansMode = true;
        ansPanel.GetComponent<MouseDrag>().searchMode = true;
        ansPanel.GetComponent<MouseDrag>().SetCloseBtn();
    }

    public void AnswerClosed(){
        print("CLOSD");
        answerShowing = false;
    }

    public void ResetQuize(){
        QuitTest();
    }
}
