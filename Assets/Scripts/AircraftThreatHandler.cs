// AircraftThreatHandler.cs
// CENG 454 - HW2: Sky-High Prototype II
// Author: Taha Kocak | Student ID: 220444013

using UnityEngine;

public class AircraftThreatHandler : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private AudioSource hitAudioSource;
    [SerializeField] private FlightExamManager examManager;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        // sadece Missile tag
        if (!collision.CompareTag("Missile")) return;

        Debug.Log("fuze carpti!");

        if (hitAudioSource != null)
        {
            hitAudioSource.Play();
        }

        // carpan fuzeyi sil
        Destroy(collision.gameObject);

        if (examManager != null)
        {
            examManager.OnMissileHit();
        }

        // ucagi geri gonder
        ResetAircraft();
    }

    // ucagi respawn noktasina geri al
    private void ResetAircraft()
    {
        if (respawnPoint == null)
        {
            Debug.LogWarning("respawnPoint atanmamis!");
            return;
        }

        // hizi sifirla
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        transform.position = respawnPoint.position;
        transform.rotation = respawnPoint.rotation;

        // 1 frame bekle ki carpma bitmis olsun
        if (examManager != null)
        {
            Invoke(nameof(DelayedReset), 0.1f);
        }

        Debug.Log("ucak resetlendi");
    }

    private void DelayedReset()
    {
        if (examManager != null)
        {
            examManager.ResetMission();
        }
    }
}
