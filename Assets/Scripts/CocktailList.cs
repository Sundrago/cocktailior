using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CocktailList : MonoBehaviour
{
    public List<Cocktail> cocktails = new List<Cocktail>();
    public Text T_title, T_description, T_glass, T_methode, T_garnish, T_receipe_0, T_receipe_1, T_receipe_2, T_amount_0, T_amount_1, T_amount_2, T_index;
    public int current_receipe, max_receipe;
    public Sprite[] icons;
    public GameObject myIcon;

    public GameObject recipeS, recipeM, recipeL;

    public bool O_show, X_show;
    public GameObject O_img, X_img, mainCamera;

    public GameObject[] box;
    public GameObject[] boxH;
    public bool[] showing;
    public bool destroyOnClose = false;
    bool started = false;
    public GameObject closeBtn_ui;

    // Start is called before the first frame update
    public void Start()
    {
        if(started) return;
        started = true;
        mainCamera = GameObject.Find("Main Camera");
        // 01
        Cocktail myCocktail = new Cocktail();
        myCocktail.C_name = "푸스카페";
        myCocktail.C_description = "식후 커피 다음에 나오는 작은 잔의 칵테일";
        myCocktail.C_glass = "스템드 리큐르 글라스";
        myCocktail.C_methode = "플로트";
        myCocktail.C_garnish = "없음";
        myCocktail.C_recipe = new List<string>() { "그레나딘 시럽", "크림 드 민트(그린)", "브랜디" };
        myCocktail.C_amount = new List<string>() { "1/3 part", "1/3 part", "1/3 part" };
        cocktails.Add(myCocktail);

        // 02
        myCocktail = new Cocktail();
        myCocktail.C_name = "맨해튼";
        myCocktail.C_description = "칵테일의 여왕이라는 별칭을 가지고 있는 칵테일";
        myCocktail.C_glass = "칵테일 글라스";
        myCocktail.C_methode = "스터링";
        myCocktail.C_garnish = "체리";
        myCocktail.C_recipe = new List<string>() { "버번 위스키", "스위트 베르무스", "앙고스투라 비터" };
        myCocktail.C_amount = new List<string>() { "1 1/2 oz", "3/4 oz", "1 dash" };
        cocktails.Add(myCocktail);

        // 03
        myCocktail = new Cocktail();
        myCocktail.C_name = "드라이 마티니";
        myCocktail.C_description = "칵테일의 왕, 헤밍웨이가 사랑한 칵테일";
        myCocktail.C_glass = "칵테일 글라스";
        myCocktail.C_methode = "스터링";
        myCocktail.C_garnish = "그린 올리브";
        myCocktail.C_recipe = new List<string>() { "드라이 진", "드라이 베르무스" };
        myCocktail.C_amount = new List<string>() { "2 oz", "1/3 oz"};
        cocktails.Add(myCocktail);

        // 04
        myCocktail = new Cocktail();
        myCocktail.C_name = "올드 패션드";
        myCocktail.C_description = "클래식 칵테일의 정석";
        myCocktail.C_glass = "올드 패션드 글라스";
        myCocktail.C_methode = "빌드";
        myCocktail.C_garnish = "오렌지 슬라이스와 체리";
        myCocktail.C_recipe = new List<string>() { "버번 위스키", "파우더 설탕", "앙고스투라 비터", "소다 워터" };
        myCocktail.C_amount = new List<string>() { "1 1/2 oz", "1 tsp", "1 dash", "1/2 oz" };
        cocktails.Add(myCocktail);

        // 05
        myCocktail = new Cocktail();
        myCocktail.C_name = "브랜디 알렉산더";
        myCocktail.C_description = "영국의 국왕과 덴마크 왕국의 장녀 알렉산드라와의 결혼을 기념해 바친 칵테일";
        myCocktail.C_glass = "칵테일 글라스";
        myCocktail.C_methode = "셰이크";
        myCocktail.C_garnish = "넛맥 파우더";
        myCocktail.C_recipe = new List<string>() { "브랜디", "크림 드 카카오(브라운)", "우유" };
        myCocktail.C_amount = new List<string>() { "3/4 oz", "3/4 oz", "3/4 oz"};
        cocktails.Add(myCocktail);

        /* 06
        myCocktail = new Cocktail();
        myCocktail.C_name = "블러디 메리";
        myCocktail.C_description = "해장 술로 유명한 칵테일";
        myCocktail.C_glass = "하이볼 글라스";
        myCocktail.C_methode = "빌드";
        myCocktail.C_garnish = "레몬 슬라이스 또는 샐러리";
        myCocktail.C_recipe = new List<string>() { "보드카", "우스터셔 소스", "타바스코 소스", "소금과 후추", "토마토쥬스" };
        myCocktail.C_amount = new List<string>() { "1 1/2 oz", "1 tsp", "1 dash", "1 pinch", "Fill Up" };
        cocktails.Add(myCocktail);
        */

        // 06
        myCocktail = new Cocktail();
        myCocktail.C_name = "싱가폴 슬링";
        myCocktail.C_description = "싱가폴 래플스 호텔에서 고안한 저녁 노을을 표현한 칵테일";
        myCocktail.C_glass = "푸티드 필스너 글라스";
        myCocktail.C_methode = "셰이크/ 빌드";
        myCocktail.C_garnish = "오렌지 슬라이스와 체리";
        myCocktail.C_recipe = new List<string>() { "드라이 진", "레몬 쥬스", "파우더 설탕", "클럽 소다", "체리 브랜디" };
        myCocktail.C_amount = new List<string>() { "1 1/2 oz", "1/2 oz", "1 tsp", "Fill Up", "1/2 oz On Top" };
        cocktails.Add(myCocktail);

        // 07
        myCocktail = new Cocktail();
        myCocktail.C_name = "블랙 러시안";
        myCocktail.C_description = "러시아를 대표하는 검정색 칵테일";
        myCocktail.C_glass = "올드 패션드 글라스";
        myCocktail.C_methode = "빌드";
        myCocktail.C_garnish = "없음";
        myCocktail.C_recipe = new List<string>() { "보드카", "커피 리큐어" };
        myCocktail.C_amount = new List<string>() { "1 oz", "1/2 oz" };
        cocktails.Add(myCocktail);

        // 08
        myCocktail = new Cocktail();
        myCocktail.C_name = "마가리타";
        myCocktail.C_description = "데킬라를 가장 우아하게 마시는 방법";
        myCocktail.C_glass = "칵테일 글라스";
        myCocktail.C_methode = "셰이크";
        myCocktail.C_garnish = "소금 리밍";
        myCocktail.C_recipe = new List<string>() { "데킬라", "트리플섹", "라임 쥬스" };
        myCocktail.C_amount = new List<string>() { "1 1/2 oz", "1/2 oz", "1/2 oz" };
        cocktails.Add(myCocktail);

        // 9
        myCocktail = new Cocktail();
        myCocktail.C_name = "러스티 네일";
        myCocktail.C_description = "'녹슨 못'이라는 이름의 영국 신사들이 즐겨 마시는 칵테일";
        myCocktail.C_glass = "올드 패션드 글라스";
        myCocktail.C_methode = "빌드";
        myCocktail.C_garnish = "없음";
        myCocktail.C_recipe = new List<string>() { "스카치 위스키", "드람부이" };
        myCocktail.C_amount = new List<string>() { "1 oz", "1/2 oz" };
        cocktails.Add(myCocktail);

        // 10
        myCocktail = new Cocktail();
        myCocktail.C_name = "위스키 사워";
        myCocktail.C_description = "새콤한 위스키 칵테일";
        myCocktail.C_glass = "사워 글라스";
        myCocktail.C_methode = "셰이크/ 빌드";
        myCocktail.C_garnish = "레몬 슬라이스와 체리";
        myCocktail.C_recipe = new List<string>() { "버번 위스키", "레몬 쥬스", "파우더 설탕", "소다 워터" };
        myCocktail.C_amount = new List<string>() { "1 1/2 oz", "1/2 oz", "1 tsp", "1 oz On Top" };
        cocktails.Add(myCocktail);
        

        // 11
        myCocktail = new Cocktail();
        myCocktail.C_name = "뉴욕";
        myCocktail.C_description = "뉴욕에 해가 떠오르는 모습을 한 칵테일";
        myCocktail.C_glass = "칵테일 글라스";
        myCocktail.C_methode = "셰이크";
        myCocktail.C_garnish = "레몬 필 트위스트";
        myCocktail.C_recipe = new List<string>() { "버번 위스키", "라임 쥬스", "파우더 설탕", "그레나딘 시럽" };
        myCocktail.C_amount = new List<string>() { "1 1/2 oz", "1/2 oz", "1 tsp", "1/2 tsp" };
        cocktails.Add(myCocktail);

        /* 13
        myCocktail = new Cocktail();
        myCocktail.C_name = "하비 월뱅어";
        myCocktail.C_description = "캘리포니아의 서퍼였던 하비가 경기에서 지고 마음을 달래기 위해 마신 칵테일";
        myCocktail.C_glass = "콜린스 글라스";
        myCocktail.C_methode = "빌드/ 플로트";
        myCocktail.C_garnish = "없음";
        myCocktail.C_recipe = new List<string>() { "보드카", "오렌지 쥬스", "길리아노" };
        myCocktail.C_amount = new List<string>() { "1 1/2 oz", "Fill Up", "1/2 oz"};
        cocktails.Add(myCocktail);
        */

        // 12
        myCocktail = new Cocktail();
        myCocktail.C_name = "다이키리";
        myCocktail.C_description = "쿠바의 다이키리 광산에서 이름을 딴 칵테일";
        myCocktail.C_glass = "칵테일 글라스";
        myCocktail.C_methode = "셰이크";
        myCocktail.C_garnish = "없음";
        myCocktail.C_recipe = new List<string>() { "라이트 럼", "라임 쥬스", "파우더 설탕" };
        myCocktail.C_amount = new List<string>() { "1 3/4 oz", "3/4 oz", "1 tsp" };
        cocktails.Add(myCocktail);

        /* 15
        myCocktail = new Cocktail();
        myCocktail.C_name = "키스 오브 파이어";
        myCocktail.C_description = "불타는 키스의 맛과 색을 표현한 칵테일";
        myCocktail.C_glass = "칵테일 글라스";
        myCocktail.C_methode = "셰이크";
        myCocktail.C_garnish = "설탕 리밍";
        myCocktail.C_recipe = new List<string>() { "보드카", "슬로 진", "드라이 버무스", "레몬 쥬스" };
        myCocktail.C_amount = new List<string>() { "1 oz", "1/2 oz", "1/2 oz", "1 tsp" };
        cocktails.Add(myCocktail);
        */

        // 13
        myCocktail = new Cocktail();
        myCocktail.C_name = "B-52";
        myCocktail.C_description = "미군의 폭격기의 이름을 딴 칵테일";
        myCocktail.C_glass = "셰리 글라스";
        myCocktail.C_methode = "플로트";
        myCocktail.C_garnish = "없음";
        myCocktail.C_recipe = new List<string>() { "커피 리큐어", "베일리스 아이리쉬 크림", "그랑 마니에르" };
        myCocktail.C_amount = new List<string>() { "1/3 part", "1/3 part", "1/3 part" };
        cocktails.Add(myCocktail);

        // 14
        myCocktail = new Cocktail();
        myCocktail.C_name = "준벅";
        myCocktail.C_description = "'6월의 벌레'라는 이름의 싱그러운 색감의 칵테일";
        myCocktail.C_glass = "콜린스 글라스";
        myCocktail.C_methode = "셰이크";
        myCocktail.C_garnish = "파인애플 웨지와 체리";
        myCocktail.C_recipe = new List<string>() { "미도리(메론 리큐르)", "코코넛 럼", "바나나 리큐르", "파인애플 쥬스", "스윗 엔 사워 믹스" };
        myCocktail.C_amount = new List<string>() { "1 oz", "1/2 oz", "1/2 oz", "2 oz", "2 oz" };
        cocktails.Add(myCocktail);

        // 15
        myCocktail = new Cocktail();
        myCocktail.C_name = "바카디 칵테일";
        myCocktail.C_description = "바카디만을 위해 만들어진 칵테일";
        myCocktail.C_glass = "칵테일 글라스";
        myCocktail.C_methode = "셰이크";
        myCocktail.C_garnish = "없음";
        myCocktail.C_recipe = new List<string>() { "바카디 화이트 럼", "라임 쥬스", "그레나딘 시럽"};
        myCocktail.C_amount = new List<string>() { "1 3/4 oz", "3/4 oz", "1 tsp"};
        cocktails.Add(myCocktail);

        /*
        myCocktail = new Cocktail();
        myCocktail.C_name = "슬로 진 피즈";
        myCocktail.C_description = "'진 피즈'에 슬로진의 새콤한 맛을 더한 칵테일";
        myCocktail.C_glass = "하이볼 글라스";
        myCocktail.C_methode = "셰이크/ 빌드";
        myCocktail.C_garnish = "레몬 슬라이스";
        myCocktail.C_recipe = new List<string>() { "슬로 진", "레몬 쥬스", "파우더 설탕", "클럽 소다" };
        myCocktail.C_amount = new List<string>() { "1 1/2 oz", "1/2 oz", "1 tsp", "Fill Up" };
        cocktails.Add(myCocktail);
        */

        // 16
        myCocktail = new Cocktail();
        myCocktail.C_name = "쿠바 리브레";
        myCocktail.C_description = "쿠바의 독립을 축하하기 위한 칵테일";
        myCocktail.C_glass = "하이볼 글라스";
        myCocktail.C_methode = "빌드";
        myCocktail.C_garnish = "레몬 웨지";
        myCocktail.C_recipe = new List<string>() { "라이트 럼", "라임 쥬스", "콜라"};
        myCocktail.C_amount = new List<string>() { "1 1/2 oz", "1/2 oz", "Fill Up" };
        cocktails.Add(myCocktail);

        // 17
        myCocktail = new Cocktail();
        myCocktail.C_name = "그래스호퍼";
        myCocktail.C_description = "여름의 초원 이미지를 한 칵테일";
        myCocktail.C_glass = "샴페인 글라스(소서형)";
        myCocktail.C_methode = "셰이크";
        myCocktail.C_garnish = "없음";
        myCocktail.C_recipe = new List<string>() { "크림 드 민트(그린)", "크림 드 카카오(화이트)", "우유" };
        myCocktail.C_amount = new List<string>() { "1 oz", "1 oz", "1 oz" };
        cocktails.Add(myCocktail);

        // 18
        myCocktail = new Cocktail();
        myCocktail.C_name = "시 브리즈";
        myCocktail.C_description = "'바닷바람'이라는 이름을 가진 싱그러운 칵테일";
        myCocktail.C_glass = "하이볼 글라스";
        myCocktail.C_methode = "빌드";
        myCocktail.C_garnish = "라임 또는 레몬 웨지";
        myCocktail.C_recipe = new List<string>() { "보드카", "크렌베리 쥬스", "자몽 쥬스" };
        myCocktail.C_amount = new List<string>() { "1 1/2 oz", "3 oz", "1/2 oz" };
        cocktails.Add(myCocktail);

        // 19
        myCocktail = new Cocktail();
        myCocktail.C_name = "애플 마티니";
        myCocktail.C_description = "마티니의 사과 버젼 칵테일";
        myCocktail.C_glass = "칵테일 글라스";
        myCocktail.C_methode = "셰이크";
        myCocktail.C_garnish = "사과 슬라이스";
        myCocktail.C_recipe = new List<string>() { "보드카", "애플퍼커", "라임 쥬스" };
        myCocktail.C_amount = new List<string>() { "1 oz", "1 oz", "1/2 oz" };
        cocktails.Add(myCocktail);

        // 20
        myCocktail = new Cocktail();
        myCocktail.C_name = "네그로니";
        myCocktail.C_description = "이탈리아의 대표 식전 칵테일";
        myCocktail.C_glass = "올드 패션드 글라스";
        myCocktail.C_methode = "빌드";
        myCocktail.C_garnish = "레몬 필 트위스트";
        myCocktail.C_recipe = new List<string>() { "드라이 진", "스위트 베르무스", "캄파리" };
        myCocktail.C_amount = new List<string>() { "3/4 oz", "3/4 oz", "3/4 oz" };
        cocktails.Add(myCocktail);

        // 21
        myCocktail = new Cocktail();
        myCocktail.C_name = "롱 아일랜드 아이스 티";
        myCocktail.C_description = "재료도 많이 들어가고 도수도 높아 '칵테일의 폭탄주'라는 별명이 붙은 칵테일";
        myCocktail.C_glass = "콜린스 글라스";
        myCocktail.C_methode = "빌드";
        myCocktail.C_garnish = "라임 또는 레몬 웨지";
        myCocktail.C_recipe = new List<string>() { "드라이 진", "보드카", "라이트 럼", "데킬라", "트리플섹", "스윗 엔 사워 믹스", "콜라" };
        myCocktail.C_amount = new List<string>() { "1/2 oz", "1/2 oz", "1/2 oz", "1/2 oz", "1/2 oz", "1 1/2 oz", "Fill Up" };
        cocktails.Add(myCocktail);

        // 22
        myCocktail = new Cocktail();
        myCocktail.C_name = "사이드 카";
        myCocktail.C_description = "브랜디 베이스의 대표적인 칵테일";
        myCocktail.C_glass = "칵테일 글라스";
        myCocktail.C_methode = "셰이크";
        myCocktail.C_garnish = "없음";
        myCocktail.C_recipe = new List<string>() { "브랜디", "쿠엥트로 또는 트리플섹", "레몬 쥬스" };
        myCocktail.C_amount = new List<string>() { "1 oz", "1 oz", "1/4 oz" };
        cocktails.Add(myCocktail);

        // 23
        myCocktail = new Cocktail();
        myCocktail.C_name = "마이타이";
        myCocktail.C_description = "여름을 대표하는 트로피컬 칵테일";
        myCocktail.C_glass = "푸티드 필스너 글라스";
        myCocktail.C_methode = "블렌드";
        myCocktail.C_garnish = "파인애플 또는 오렌지 웨지와 체리";
        myCocktail.C_recipe = new List<string>() { "라이트 럼", "트리플섹", "라임 쥬스", "파인애플 쥬스", "오렌지 쥬스", "그레나딘 시럽" };
        myCocktail.C_amount = new List<string>() { "1 1/4 oz", "3/4 oz", "1 oz", "1 oz", "1 oz", "1/4 oz"};
        cocktails.Add(myCocktail);

        // 24
        myCocktail = new Cocktail();
        myCocktail.C_name = "피나콜라다";
        myCocktail.C_description = "'파인애플이 무성한 언덕'이라는 의미의 여름 칵테일";
        myCocktail.C_glass = "푸티드 필스너 글라스";
        myCocktail.C_methode = "블렌드";
        myCocktail.C_garnish = "파인애플 웨지와 체리";
        myCocktail.C_recipe = new List<string>() { "라이트 럼", "피나콜라다 믹스", "파인애플 쥬스" };
        myCocktail.C_amount = new List<string>() { "1 1/4 oz", "2 oz", "2 oz" };
        cocktails.Add(myCocktail);

        // 25
        myCocktail = new Cocktail();
        myCocktail.C_name = "코스모폴리탄";
        myCocktail.C_description = "'세계인'이라는 의미의 도시적인 칵테일";
        myCocktail.C_glass = "칵테일 글라스";
        myCocktail.C_methode = "셰이크";
        myCocktail.C_garnish = "레몬 또는 라임 필 트위스트";
        myCocktail.C_recipe = new List<string>() { "보드카", "트리플섹", "라임 쥬스", "크렌베리 쥬스" };
        myCocktail.C_amount = new List<string>() { "1 oz", "1/2 oz", "1/2 oz", "1/2 oz" };
        cocktails.Add(myCocktail);

        // 26
        myCocktail = new Cocktail();
        myCocktail.C_name = "모스코 뮬";
        myCocktail.C_description = "구리잔에 마셔야 제맛인 칵테일";
        myCocktail.C_glass = "하이볼 글라스";
        myCocktail.C_methode = "빌드";
        myCocktail.C_garnish = "레몬 또는 라임 슬라이스";
        myCocktail.C_recipe = new List<string>() { "보드카", "라임 쥬스", "진저에일" };
        myCocktail.C_amount = new List<string>() { "1 1/2 oz", "1/2 oz", "Fill Up" };
        cocktails.Add(myCocktail);

        // 27
        myCocktail = new Cocktail();
        myCocktail.C_name = "애프리콧 칵테일";
        myCocktail.C_description = "상큼한 살구향을 느낄 수 있는 칵테일";
        myCocktail.C_glass = "칵테일 글라스";
        myCocktail.C_methode = "셰이크";
        myCocktail.C_garnish = "없음";
        myCocktail.C_recipe = new List<string>() { "애프리콧 브랜디", "드라이 진", "레몬 쥬스", "오렌지 쥬스" };
        myCocktail.C_amount = new List<string>() { "1 1/2 oz", "1 tsp", "1/2 oz", "1/2 oz" };
        cocktails.Add(myCocktail);

        // 28
        myCocktail = new Cocktail();
        myCocktail.C_name = "허니문 칵테일";
        myCocktail.C_description = "'신혼여행'이라는 이름의 달콤한 칵테일";
        myCocktail.C_glass = "칵테일 글라스";
        myCocktail.C_methode = "셰이크";
        myCocktail.C_garnish = "없음";
        myCocktail.C_recipe = new List<string>() { "애플 브랜디", "베네딕틴 DOM", "트리플섹", "레몬 쥬스" };
        myCocktail.C_amount = new List<string>() { "3/4 oz", "3/4 oz", "1/4 oz", "1/2 oz" };
        cocktails.Add(myCocktail);

        // 29
        myCocktail = new Cocktail();
        myCocktail.C_name = "블루 하와이안";
        myCocktail.C_description = "시원한 푸른빛 바다를 떠올리게 하는 여름 칵테일";
        myCocktail.C_glass = "푸티드 필스너 글라스";
        myCocktail.C_methode = "블렌드";
        myCocktail.C_garnish = "파인애플 웨지와 체리";
        myCocktail.C_recipe = new List<string>() { "라이트 럼", "블루 큐라소", "코코넛 럼", "파인애플 쥬스" };
        myCocktail.C_amount = new List<string>() { "1 oz", "1 oz", "1 oz", "2 1/2 oz" };
        cocktails.Add(myCocktail);

        // 30
        myCocktail = new Cocktail();
        myCocktail.C_name = "키르";
        myCocktail.C_description = "우아하고 고급스러운 이미지의 식전 칵테일";
        myCocktail.C_glass = "화이트 와인 글라스";
        myCocktail.C_methode = "빌드";
        myCocktail.C_garnish = "레몬 필 트위스트";
        myCocktail.C_recipe = new List<string>() { "화이트 와인", "크림 드 카시스"};
        myCocktail.C_amount = new List<string>() { "3 oz", "1/2 oz" };
        cocktails.Add(myCocktail);

        // 31
        myCocktail = new Cocktail();
        myCocktail.C_name = "데킬라 선라이즈";
        myCocktail.C_description = "해 뜰 때의 붉은 하늘을 연상시키는 칵테일";
        myCocktail.C_glass = "푸티드 필스너 글라스";
        myCocktail.C_methode = "빌드/ 플로팅";
        myCocktail.C_garnish = "없음";
        myCocktail.C_recipe = new List<string>() { "데킬라", "오렌지 쥬스", "그레나딘 시럽"};
        myCocktail.C_amount = new List<string>() { "1 1/2 oz", "Fill Up", "1/2 oz"};
        cocktails.Add(myCocktail);

        // 32
        myCocktail = new Cocktail();
        myCocktail.C_name = "힐링";
        myCocktail.C_description = "몸에 좋은 8가지 한약재가 담긴 칵테일";
        myCocktail.C_glass = "칵테일 글라스";
        myCocktail.C_methode = "셰이크";
        myCocktail.C_garnish = "레몬 필 트위스트";
        myCocktail.C_recipe = new List<string>() { "감흥로", "베네딕틴 DOM", "크림 드 카시스", "스윗 엔 사워 믹스"};
        myCocktail.C_amount = new List<string>() { "1 1/2 oz", "1/3 oz", "1/3 oz", "1 oz"};
        cocktails.Add(myCocktail);

        // 33
        myCocktail = new Cocktail();
        myCocktail.C_name = "진도";
        myCocktail.C_description = "홍옥색의 빛깔의 칵테일";
        myCocktail.C_glass = "칵테일 글라스";
        myCocktail.C_methode = "셰이크";
        myCocktail.C_garnish = "없음";
        myCocktail.C_recipe = new List<string>() { "진도 홍주", "크림 드 민트(화이트)", "청포도 쥬스", "라즈베리 시럽"};
        myCocktail.C_amount = new List<string>() { "1 oz", "1/2 oz", "3/4 oz", "1/2 oz"};
        cocktails.Add(myCocktail);

        // 34
        myCocktail = new Cocktail();
        myCocktail.C_name = "풋사랑";
        myCocktail.C_description = "능금아가씨의 풋풋하고 아련한 첫사랑";
        myCocktail.C_glass = "칵테일 글라스";
        myCocktail.C_methode = "셰이크";
        myCocktail.C_garnish = "사과 슬라이스";
        myCocktail.C_recipe = new List<string>() { "안동 소주", "트리플섹", "애플퍼커", "라임 쥬스"};
        myCocktail.C_amount = new List<string>() { "1 oz", "1/3 oz", "1 oz", "1/3 oz"};
        cocktails.Add(myCocktail);

        // 35
        myCocktail = new Cocktail();
        myCocktail.C_name = "금산";
        myCocktail.C_description = "현대인의 건강을 위한 칵테일";
        myCocktail.C_glass = "칵테일 글라스";
        myCocktail.C_methode = "셰이크";
        myCocktail.C_garnish = "없음";
        myCocktail.C_recipe = new List<string>() { "금산 인삼주", "커피 리큐어", "애플퍼커", "라임 쥬스"};
        myCocktail.C_amount = new List<string>() { "1 1/2 oz", "1/2 oz", "1/2 oz", "1 tsp"};
        cocktails.Add(myCocktail);

        // 36
        myCocktail = new Cocktail();
        myCocktail.C_name = "고창";
        myCocktail.C_description = "산뜻한 복분자 칵테일";
        myCocktail.C_glass = "플루트 샴페인 글라스";
        myCocktail.C_methode = "스터링";
        myCocktail.C_garnish = "없음";
        myCocktail.C_recipe = new List<string>() { "선운산 복분자주", "트리플섹", "스프라이트"};
        myCocktail.C_amount = new List<string>() { "2 oz", "1/2 oz", "2 oz"};
        cocktails.Add(myCocktail);

        // 37
        myCocktail = new Cocktail();
        myCocktail.C_name = "진피즈";
        myCocktail.C_description = "여름철 인기가 많은 칵테일";
        myCocktail.C_glass = "하이볼 글라스";
        myCocktail.C_methode = "셰이크/ 빌드";
        myCocktail.C_garnish = "레몬 슬라이스";
        myCocktail.C_recipe = new List<string>() { "드라이 진", "레몬 쥬스", "파우더 설탕", "클럽 소다"};
        myCocktail.C_amount = new List<string>() { "1 1/2 oz", "1/2 oz", "1 tsp", "Fill Up"};
        cocktails.Add(myCocktail);

        // 38
        myCocktail = new Cocktail();
        myCocktail.C_name = "레몬 스쿼시";
        myCocktail.C_description = "신선한 레몬즙이 들어가는 칵테일";
        myCocktail.C_glass = "하이볼 글라스";
        myCocktail.C_methode = "빌드";
        myCocktail.C_garnish = "레몬 슬라이스";
        myCocktail.C_recipe = new List<string>() { "레몬 즙", "파우더 설탕", "소다 워터"};
        myCocktail.C_amount = new List<string>() { "1/2 ea", "2 tsp", "Fill Up"};
        cocktails.Add(myCocktail);

        // 39
        myCocktail = new Cocktail();
        myCocktail.C_name = "버진 프루츠 펀치";
        myCocktail.C_description = "논알콜 트로피컬 칵테일";
        myCocktail.C_glass = "푸티드 필스너 글라스";
        myCocktail.C_methode = "블렌드";
        myCocktail.C_garnish = "파인애플 웨지와 체리";
        myCocktail.C_recipe = new List<string>() { "오렌지 쥬스", "파인애플 쥬스", "크렌베리 쥬스", "자몽 쥬스", "레몬 쥬스", "그레나딘 시럽"};
        myCocktail.C_amount = new List<string>() { "1 oz", "1 oz", "1 oz", "1 oz", "1/2 oz", "1/2 oz"};
        cocktails.Add(myCocktail);

        /*
        List<string> myGlass = new List<string>();
        for(int i =0; i<cocktails.Count; i++){
            for(int j=0; j<cocktails[i].C_amount.Count; j++){
                if(!myGlass.Contains(cocktails[i].C_amount[j])){
                    myGlass.Add(cocktails[i].C_amount[j]);
                }
            }
        }
        Debug.Log(myGlass);
        for(int i =0; i<myGlass.Count; i++){
            Debug.Log(myGlass[i]);
        }
        */
    }


    public void UpdateCocktailInfo(int i)
    {
        //Debug.Log("open cocktial ID : " + i);
        T_title.text = cocktails[i].C_name;
        T_description.text = cocktails[i].C_description;
        T_glass.text = cocktails[i].C_glass;
        T_methode.text = cocktails[i].C_methode;
        T_garnish.text = cocktails[i].C_garnish;

        string receipe = "";
        for (int j = 0; j < cocktails[i].C_recipe.Count; j++)
        {
            receipe += cocktails[i].C_recipe[j];
            if (j < cocktails[i].C_recipe.Count - 1) receipe += "\n";
        }

        T_receipe_0.text = receipe;
        T_receipe_1.text = receipe;
        T_receipe_2.text = receipe;

        if (cocktails[i].C_recipe.Count <= 2)
        {
            recipeS.SetActive(true);
            recipeM.SetActive(false);
            recipeL.SetActive(false);
        } else if (cocktails[i].C_recipe.Count <= 3)
        {
            recipeS.SetActive(false);
            recipeM.SetActive(true);
            recipeL.SetActive(false);

        } else
        {
            recipeS.SetActive(false);
            recipeM.SetActive(false);
            recipeL.SetActive(true);
        }

        string amount = "";
        for (int j = 0; j < cocktails[i].C_amount.Count; j++)
        {
            amount += cocktails[i].C_amount[j];
            if (j < cocktails[i].C_amount.Count - 1) amount += "\n";
        }

        T_amount_0.text = amount;
        T_amount_1.text = amount;
        T_amount_2.text = amount;

        T_index.text = "" + (current_receipe + 1) + "/" + (max_receipe);
        myIcon.GetComponent<Image>().sprite = icons[i];

    }

    public void IconControl(string action)
    {
        switch(action)
        {
            case "O_show":
                O_img.GetComponent<Animator>().SetTrigger("show");
                O_show = true;
                if (X_show) IconControl("X_hide");
                break;
            case "O_hide":
                O_img.GetComponent<Animator>().SetTrigger("hide");
                O_show = false;
                break;
            case "O_showed":
                O_img.GetComponent<Animator>().SetTrigger("showed");
                O_show = true;
                break;
            case "O_hidden":
                O_img.GetComponent<Animator>().SetTrigger("hidden");
                O_show = false;
                break;
            case "X_show":
                X_img.GetComponent<Animator>().SetTrigger("show");
                X_show = true;
                if (O_show) IconControl("O_hide");
                break;
            case "X_hide":
                X_img.GetComponent<Animator>().SetTrigger("hide");
                X_show = false;
                break;
            case "X_showed":
                X_img.GetComponent<Animator>().SetTrigger("showed");
                X_show = true;
                break;
            case "X_hidden":
                X_img.GetComponent<Animator>().SetTrigger("hidden");
                X_show = false;
                break;
        }
    }

    public void ShowBox(int i)
    {
        box[i].GetComponent<Animator>().SetTrigger("show");
        boxH[i].GetComponent<Animator>().SetTrigger("hide");
    }

    public void HideBox(int i)
    {
        box[i].GetComponent<Animator>().SetTrigger("hide");
        boxH[i].GetComponent<Animator>().SetTrigger("show");
    }

    public void showAll()
    {
        for(int i = 0; i<5; i++)
        {
            box[i].GetComponent<Animator>().SetTrigger("idle");
            boxH[i].GetComponent<Animator>().SetTrigger("hidden");
        }
    }

    public string ReturnName(int idx)
    {
        return cocktails[idx].C_name;
    }

    public string ReturnDescription(int idx)
    {
        return cocktails[idx].C_description;
    }
    public string ReturnGlass(int idx)
    {
        return cocktails[idx].C_glass;
    }
    public string ReturnGarnish(int idx)
    {
        return cocktails[idx].C_garnish;
    }
    public string ReturnMethode(int idx)
    {
        return cocktails[idx].C_methode;
    }

    public List<string> ReturnRcp(int idx) {
        return cocktails[idx].C_recipe;
    }

    public List<string> ReturnAmt(int idx) {
        return cocktails[idx].C_amount;
    }

    void Update(){
        if(destroyOnClose) {
            if(mainCamera.GetComponent<MainControl>().showMenu == false){
                destroyOnClose = false;
                gameObject.GetComponent<PanelAnimControl>().PlayAnim("scroll_left");
            }
        }
    }
}

public class Cocktail
{
    public string C_name { get; set; }
    public string C_description { get; set; }
    public string C_glass { get; set; }
    public string C_methode { get; set; }
    public string C_garnish { get; set; }
    public List<string> C_recipe { get; set; }
    public List<string> C_amount { get; set; }
}