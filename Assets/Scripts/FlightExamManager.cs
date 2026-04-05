// FlightExamManager.cs
// CENG 454 - HW2: Sky-High Prototype II
// Author: Taha Kocak | Student ID: 220444013

using UnityEngine;
using TMPro;

public class FlightExamManager : MonoBehaviour
{
    [SerializeField] private TMP_Text statusText;   // uyari mesaji icin
    [SerializeField] private TMP_Text missionText;  // gorev durumu icin

    [SerializeField] private AudioSource warningAudio;
    [SerializeField] private AudioSource successAudio;

    // gorev bayraklari
    private bool hasTakenOff = false;
    private bool inDangerZone = false;
    private bool threatCleared = false;
    private bool missionComplete = false;

    void Start()
    {
        if (statusText != null)
            statusText.text = "";

        if (missionText != null)
            missionText.text = "Mission: Take off from the runway";

        UpdateMissionHUD();
    }

    // kalkis fonksiyonu - TakeoffDetector cagiriyor
    public void OnTakeoff()
    {
        if (hasTakenOff) return;

        hasTakenOff = true;
        Debug.Log("kalkis ok");
        UpdateMissionHUD();
    }

    // DangerZoneController'dan cagirilir - bolgeye giris
    public void EnterDangerZone()
    {
        inDangerZone = true;

        // HUD uyarisi hemen ciksin
        if (statusText != null)
            statusText.text = "Entered a Dangerous Zone!";

        if (warningAudio != null && !warningAudio.isPlaying)
            warningAudio.Play();

        Debug.Log("tehlike bolgesine girildi");
        UpdateMissionHUD();
    }

    // DangerZoneController'dan cagirilir - bolgeden cikis
    public void ExitDangerZone()
    {
        inDangerZone = false;
        threatCleared = true;

        if (statusText != null)
            statusText.text = "Zone cleared - find the landing strip!";

        if (warningAudio != null && warningAudio.isPlaying)
            warningAudio.Stop();

        Debug.Log("tehlike atlatildi");
        UpdateMissionHUD();
    }

    // fuze carptiysa - AircraftThreatHandler cagiriyor
    public void OnMissileHit()
    {
        if (statusText != null)
            statusText.text = "MISSILE HIT! Mission Failed!";

        if (missionText != null)
            missionText.text = "Mission: FAILED - Try again";

        missionComplete = false;
        threatCleared = false;
        inDangerZone = false;

        Debug.Log("fuze carpti, gorev basarisiz");
    }

    // reset - carpma sonrasi her seyi sifirla
    public void ResetMission()
    {
        hasTakenOff = false;
        inDangerZone = false;
        threatCleared = false;
        missionComplete = false;

        if (statusText != null)
            statusText.text = "";

        UpdateMissionHUD();
        Debug.Log("gorev sifirlandi");
    }

    // inis denemesi - LandingDetector cagiriyor
    // kalkis + tehlike atlatma sart
    public bool TryLanding()
    {
        if (!hasTakenOff)
        {
            Debug.Log("henuz kalkmadin, inis gecersiz");
            if (missionText != null)
                missionText.text = "You must take off first!";
            return false;
        }

        if (!threatCleared)
        {
            Debug.Log("tehlike atlatilmadi, inis gecersiz");
            if (missionText != null)
                missionText.text = "Survive the danger zone first!";
            return false;
        }

        // her sey tamam
        missionComplete = true;

        if (statusText != null)
            statusText.text = "MISSION COMPLETE!";

        if (missionText != null)
            missionText.text = "Mission: SUCCESS - Well done!";

        if (successAudio != null)
            successAudio.Play();

        Debug.Log("gorev tamamlandi!");
        return true;
    }

    // missionText'i duruma gore guncelle
    private void UpdateMissionHUD()
    {
        if (missionText == null) return;

        if (missionComplete)
        {
            missionText.text = "Mission: SUCCESS!";
        }
        else if (threatCleared)
        {
            missionText.text = "Mission: Return to landing strip";
        }
        else if (inDangerZone)
        {
            missionText.text = "Mission: Escape the danger zone!";
        }
        else if (hasTakenOff)
        {
            missionText.text = "Mission: Fly towards the corridor";
        }
        else
        {
            missionText.text = "Mission: Take off from the runway";
        }
    }

    // diger scriptlerin durumu sorgulamasi icin
    public bool HasTakenOff { get { return hasTakenOff; } }
    public bool IsThreatCleared { get { return threatCleared; } }
    public bool IsMissionComplete { get { return missionComplete; } }
    public bool IsInDangerZone { get { return inDangerZone; } }
}
