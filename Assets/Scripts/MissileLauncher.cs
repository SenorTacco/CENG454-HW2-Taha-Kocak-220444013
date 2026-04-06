// MissileLauncher.cs
// CENG 454 - HW2: Sky-High Prototype II
// Author: Taha Kocak | Student ID: 220444013

using UnityEngine;

public class MissileLauncher : MonoBehaviour
{
    [SerializeField] private GameObject missilePrefab;
    [SerializeField] private Transform launchPoint;
    [SerializeField] private AudioSource launchAudioSource;

    // ayni anda tek fuze olsun
    private GameObject activeMissile;

    // fuzeyi olustur ve hedefe yonlendir
    public GameObject Launch(Transform target)
    {
        if (activeMissile != null) return activeMissile;

        if (missilePrefab == null || launchPoint == null)
        {
            Debug.LogError("missilePrefab veya launchPoint bos!");
            return null;
        }

        activeMissile = Instantiate(missilePrefab, launchPoint.position, launchPoint.rotation);
        activeMissile.tag = "Missile";

        // homing scriptine hedefi ver
        MissileHoming homing = activeMissile.GetComponent<MissileHoming>();
        if (homing != null)
        {
            homing.SetTarget(target);
        }
        else
        {
            Debug.LogWarning("prefab'da MissileHoming yok!");
        }

        if (launchAudioSource != null)
        {
            launchAudioSource.Play();
        }

        Debug.Log("fuze firlatildi");
        return activeMissile;
    }

    // aktif fuzeyi sil - zone'dan cikinca cagirilir
    public void DestroyActiveMissile()
    {
        if (activeMissile != null)
        {
            Destroy(activeMissile);
            activeMissile = null;
        }
    }
}
