using System.Collections;
using UnityEngine;
using GoogleMobileAds.Common;
using Unity.Services.RemoteConfig;

public abstract class AdmobUnitBase : MonoBehaviour
{
    [SerializeField] private string unitIDAndroid;
    [SerializeField] private string unitIDIOS;

    protected string UnitID
    {
        get
        {
#if UNITY_ANDROID
            return unitIDAndroid;
#elif UNITY_IOS
            return unitIDIOS;
#else
            return "";
#endif
        }
    }

    [Header("Remote Config")]
    [SerializeField] private bool useRemoteConfig = false;
    public bool UseRemoteConfig => useRemoteConfig;
    [SerializeField] private string keyUnitIDAndroid = "unit_id_android";
    [SerializeField] private string keyUnitIDIOS = "unit_id_ios";

    private void OnAppStateChangedBase(AppState state)
    {
        Debug.Log("App State changed to : " + state);
        OnAppStateChanged(state);
    }

    private IEnumerator Start()
    {
        while (AdmobManager.Instance.IsReady == false)
        {
            yield return 0;
        }
        if (useRemoteConfig)
        {
            unitIDAndroid = RemoteConfigService.Instance.appConfig.GetString(keyUnitIDAndroid);
            unitIDIOS = RemoteConfigService.Instance.appConfig.GetString(keyUnitIDIOS);
            Debug.Log("keyUnitIDIOS: " + keyUnitIDIOS);
            Debug.Log("unitIDIOS: " + unitIDIOS);
        }
        Initialize();
    }
    protected virtual void Initialize()
    {
        // Call after initialization of AdsManager
    }
    protected virtual void OnAppStateChanged(AppState state)
    {
    }
}
