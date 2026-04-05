// DangerZoneController.cs
// CENG 454 - HW2: Sky-High Prototype II
// Author: Taha Kocak | Student ID: 220444013

using UnityEngine;

public class DangerZoneController : MonoBehaviour
{
    [SerializeField] private FlightExamManager examManager;
    [SerializeField] private MissileLauncher missileLauncher;
    [SerializeField] private float missileDelay = 5f;

    // coroutine referansi - cikarken iptal etmek icin tutuyorum
    private Coroutine activeCountdown;
    private bool playerInside = false;

    private void OnTriggerEnter(Collider collision)
    {
        if (!collision.CompareTag("Player")) return;

        playerInside = true;

        // HUD uyarisini hemen goster, fuze gecikmesi ayri
        if (examManager != null)
        {
            examManager.EnterDangerZone();
        }

        // fuze countdown basla
        activeCountdown = StartCoroutine(MissileCountdown());
    }

    private void OnTriggerExit(Collider collision)
    {
        if (!collision.CompareTag("Player")) return;

        playerInside = false;

        // countdown hala devam ediyorsa durdur - yoksa ghost missile olur
        if (activeCountdown != null)
        {
            StopCoroutine(activeCountdown);
            activeCountdown = null;
        }

        // sahnedeki fuzeyi temizle
        if (missileLauncher != null)
        {
            missileLauncher.DestroyActiveMissile();
        }

        // manager'a bildir
        if (examManager != null)
        {
            examManager.ExitDangerZone();
        }
    }

    // missileDelay kadar bekle, sonra fuze firlat
    private System.Collections.IEnumerator MissileCountdown()
    {
        yield return new WaitForSeconds(missileDelay);

        // sure doldu, oyuncu hala icerdeyse firlat
        if (playerInside && missileLauncher != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                missileLauncher.Launch(player.transform);
            }
        }

        activeCountdown = null;
    }
}
