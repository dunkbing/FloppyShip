// Created by Binh Bui on 06, 27, 2021

using System;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Events;

namespace Utilities
{
    public class AdsManager : MonoBehaviour, IUnityAdsListener
    {
        private const string GameId = "4270041";
        private const string SurfacingId = "Rewarded_Android";
        private const bool TestMode = false;

        public UnityAction OnEarnedReward;
        public static AdsManager Instance { get; private set; }

        private void Awake()
        {
            Instance ??= this;
        }

        private void OnEnable()
        {
            try
            {
                Advertisement.Initialize(GameId, TestMode);
                Advertisement.AddListener(this);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }

        public void ShowRewardedVideo() {
            // Check if UnityAds ready before calling Show method:
            if (Advertisement.IsReady(SurfacingId)) {
                Advertisement.Show(SurfacingId);
            }
            else {
                Debug.Log("Rewarded video is not ready at the moment! Please try again later!");
            }
        }

        public bool ReadyToShow()
        {
            return Advertisement.IsReady(SurfacingId);
        }

        // Implement IUnityAdsListener interface methods:
        public void OnUnityAdsDidFinish(string surfacingId, ShowResult showResult)
        {
            switch (showResult)
            {
                // Define conditional logic for each ad completion status:
                case ShowResult.Finished:
                    // Reward the user for watching the ad to completion.
                    OnEarnedReward?.Invoke();
                    break;
                case ShowResult.Skipped:
                    // Do not reward the user for skipping the ad.
                    break;
                case ShowResult.Failed:
                    Debug.LogWarning ("The ad did not finish due to an error.");
                    break;
            }
        }

        public void OnUnityAdsReady (string surfacingId) {
            if (surfacingId == SurfacingId) {
                // Optional actions to take when theAd Unit or legacy Placement becomes ready (for example, enable the rewarded ads button)
                // gameObject.GetComponent<Button>().interactable = true;
            }
        }

        public void OnUnityAdsDidError (string message) {
            // Log the error.
        }

        public void OnUnityAdsDidStart (string surfacingId) {
            // Optional actions to take when the end-users triggers an ad.
        }

        // When the object that subscribes to ad events is destroyed, remove the listener:
        public void OnDisable() {
            Advertisement.RemoveListener(this);
        }
    }
}