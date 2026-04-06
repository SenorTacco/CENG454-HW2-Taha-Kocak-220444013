// MissileHoming.cs
// CENG 454 - HW2: Sky-High Prototype II
// Author: Taha Kocak | Student ID: 220444013

using UnityEngine;

public class MissileHoming : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 22f;  // ucaktan biraz yavas ayarladim
    [SerializeField] private float turnSpeed = 3f;   // cok yukseltince kacilamaz oluyor

    private Transform target;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void Update()
    {
        if (target == null)
        {
            // hedef yoksa duz git
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            return;
        }

        // hedefe olan yon
        Vector3 dir = target.position - transform.position;

        if (dir.sqrMagnitude < 0.01f) return;

        // hedefe dogru yavasca don
        Quaternion hedefRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            hedefRot,
            turnSpeed * Time.deltaTime
        );

        // ileri git
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
}
