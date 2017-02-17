using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TrackerEventHandler : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
	
	}

    public virtual void OnBackButtonClicked()
    {
        SceneManager.LoadScene("Main Menu");
    }

    // URLResource events
    public virtual void OnFinishLoading()
    {
        Debug.Log("URL Resource loaded successfully.");
    }

    public virtual void OnErrorLoading(int errorCode, string errorMessage)
    {
        Debug.LogError("Error loading URL Resource!\nErrorCode: " + errorCode + "\nErrorMessage: " + errorMessage);
    }

    // Tracker events
    public virtual void OnTargetsLoaded()
    {
        Debug.Log("Targets loaded successfully.");
    }

    public virtual void OnErrorLoadingTargets(int errorCode, string errorMessage)
    {
        Debug.LogError("Error loading targets!\nErrorCode: " + errorCode + "\nErrorMessage: " + errorMessage);
    }

    public virtual void OnExtendedTrackingQualityChanged()
    {
        Debug.LogError("OnExtended Tracking Quality Changed");
    }

    protected virtual void Update()
    {
        // Also handles the back button on Android
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnBackButtonClicked();
        }
    }
}
