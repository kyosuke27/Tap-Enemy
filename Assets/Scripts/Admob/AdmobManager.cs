using UnityEngine;
using GoogleMobileAds.Api;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.RemoteConfig;

public class AdmobManager : MonoBehaviour
{
    public static AdmobManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AdmobManager>();
            }
            return instance;
        }
    }
    public static AdmobManager instance;

    private bool isReady = false;
    public bool IsReady
    {
        get
        {
            return isReady;
        }
    }

    [SerializeField] private bool useRemoteConfig = false;

    // Create attributes classes
    private class UserAttributes { }
    private class AppAttributes { }

    async void Start()
    {
        if (this != Instance)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        Debug.Log("AdmobManager Start");
        Debug.Log("useRemoteConfig: " + useRemoteConfig);   
        if (useRemoteConfig)
        {
            Debug.Log("Remote Config is enabled");
            if (Utilities.CheckForInternetConnection())
            {
                await UnityServices.InitializeAsync();

                if (!AuthenticationService.Instance.IsSignedIn)
                {
                    await AuthenticationService.Instance.SignInAnonymouslyAsync();
                }

                // Create instances of user and app attributes
                var userAttributes = new UserAttributes();
                var appAttributes = new AppAttributes();

                // Fetch Remote Config data
                await RemoteConfigService.Instance.FetchConfigsAsync(userAttributes, appAttributes);
            }
        }

        MobileAds.RaiseAdEventsOnUnityMainThread = true;
        MobileAds.Initialize(initStatus =>
        {
            isReady = true;
            Debug.Log(initStatus);
        });
    }
}
