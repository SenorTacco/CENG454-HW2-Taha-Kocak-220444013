// LandingDetector.cs
// CENG 454 - HW2: Sky-High Prototype II
// Author: Taha Kocak | Student ID: 220444013

using UnityEngine;

public class LandingDetector : MonoBehaviour
{
    [SerializeField] private FlightExamManager examManager;

    // ucak inis alanina girince TryLanding dene
    private void OnTriggerEnter(Collider collision)
    {
        if (!collision.CompareTag("Player")) return;

        if (examManager != null)
        {
            bool ok = examManager.TryLanding();

            if (ok)
                Debug.Log("inis basarili!");
            else
                Debug.Log("inis sartlari saglanmadi");
        }
    }
}
