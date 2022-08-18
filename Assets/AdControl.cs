using System;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;

public class AdControl : MonoBehaviour
{
    private InterstitialAd interstitial;
    private RewardedAd rewardedAd;
    string nextAction;
    private float lastAdShown;
    private RewardedInterstitialAd rewardedInterstitialAd;

    // Start is called before the first frame update
    void Start()
    {
        MobileAds.Initialize(initStatus => { });
        RequestInterstitial();
        LoadRewardAds();

        //ShowShortAds();
        lastAdShown = Time.time;
    }

    public void ShowShortAds(string doNext)
    {
        nextAction = doNext;

        if (Time.time - lastAdShown < 60f)
        {
            DoNext();
            return;
        }

        lastAdShown = Time.time;
        
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
        else
        {
            DoNext();
            RequestInterstitial();
        }

    }
    public void ShowRewardAds(string doNext)
    {
        nextAction = doNext;
        lastAdShown = Time.time;

        if (this.rewardedAd.IsLoaded()) {
            this.rewardedAd.Show();
        } else
        {
            DoNext();
            LoadRewardAds();
        }

        
    }

    private void DoNext()
    {
        print(nextAction);
        switch(nextAction)
        {
            case "menu_btn":
                gameObject.GetComponent<MainControl>().menu_btn_clicked_ad();
                break;
            case "CheckQuize1":
                gameObject.GetComponent<MainControl>().CheckQuize(1);
                break;
            case "CheckQuize2":
                gameObject.GetComponent<MainControl>().CheckQuize(2);
                break;
            case "CheckQuize0":
                gameObject.GetComponent<MainControl>().CheckQuize(0);
                break;
            case "none":
                return;
        }
        nextAction = "none";
    }

    private void LoadRewardAds(){

        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-2796864814295819/9883547117";
        #elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-2796864814295819/8007067081";
        #else
            string adUnitId = "unexpected_platform";
        #endif
        
        this.rewardedAd = new RewardedAd(adUnitId);

        // Called when an ad request has successfully loaded.
        //this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        //this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        //this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        // this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        DoNext();
        LoadRewardAds();
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: "
                             + args.Message);
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        DoNext();
        LoadRewardAds();
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardedAdRewarded event received for "
                        + amount.ToString() + " " + type);
    }

    private void RequestInterstitial()
    {
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-2796864814295819/9450456363";
        #elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-2796864814295819/9658958471";
        #else
            string adUnitId = "unexpected_platform";
        #endif

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);

        // Called when an ad request has successfully loaded.
        //this.interstitial.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        //this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        //this.interstitial.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        //this.interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        DoNext();
        RequestInterstitial();
        MonoBehaviour.print("HandleAdClosed event received");
    }
}
