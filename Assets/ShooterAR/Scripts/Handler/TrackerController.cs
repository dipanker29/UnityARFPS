using UnityEngine;
using System.Collections;
using Wikitude;

namespace ShooterAR
{
    public class TrackerController : MonoBehaviour
    {
        const string TAG = "EXAMPLEAR";

        // Use this for initialization
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }

        // URLResource events
        public void OnFinishLoading()
        {
            Debug.Log(TAG + " URL Resource loaded successfully.");
        }

        public void OnErrorLoading(int errorCode, string errorMessage)
        {
            Debug.LogError(TAG + " Error loading URL Resource!\nErrorCode: " + errorCode + "\nErrorMessage: " + errorMessage);
        }

        // Tracker events
        public void OnTargetsLoaded()
        {
            Debug.Log(TAG + " Targets loaded successfully.");
        }

        public void OnErrorLoadingTargets(int errorCode, string errorMessage)
        {
            Debug.LogError(TAG + " Error loading targets!\nErrorCode: " + errorCode + "\nErrorMessage: " + errorMessage);
        }

        public void OnExtendedTrackingQualityChanged()
        {
            Debug.LogError(TAG + " OnExtended Tracking Quality Changed");
        }

        public void OnStopExtendedTrackingButtonPressed()
        {
            Debug.LogError(TAG + " OnStop Extended Tracking ButtonPressed");
        }

        public void OnExtendedTrackingQualityChanged(string targetName, ExtendedTrackingQuality newQuality)
        {
            Debug.LogError(TAG + " OnStop Extended Tracking ButtonPressed");
        }

        public void OnEnterFieldOfVision(string target)
        {
            Debug.LogError(TAG + " OnEnter Field Of Vision " + target);
            GameManager.Instance.OnFieldOfVision(true);
        }

        public void OnExitFieldOfVision(string target)
        {
            Debug.LogError(TAG + " OnExit Field Of Vision " + target);
            GameManager.Instance.OnFieldOfVision(false);
        }

        public void OnStateChanged(InstantTrackingState state)
        {
            Debug.Log(TAG + " OnState Changed. " + state);
        }
    }
}
