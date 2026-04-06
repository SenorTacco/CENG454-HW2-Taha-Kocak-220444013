// TakeoffDetector.cs
// CENG 454 - HW2: Sky-High Prototype II
// Author: Taha Kocak | Student ID: 220444013

using UnityEngine;

public class TakeoffDetector : MonoBehaviour
{
    [SerializeField] private FlightExamManager examManager;

    // ucak pistten cikinca kalkis sayilir
    private void OnTriggerExit(Collider collision)
    {
        if (!collision.CompareTag("Player")) return;

        if (examManager != null)
        {
            examManager.OnTakeoff();
        }
    }
}
