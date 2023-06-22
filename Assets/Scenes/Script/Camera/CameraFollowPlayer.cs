using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform target;
    public Vector3 posOffset;
    public float smooth;

    // Update is called once per frame
    void LateUpdate() //late update dipanggil setelah semua UPDATE sudah dipanggil 
    {
        transform.position = Vector3.Lerp(transform.position, target.position + posOffset, smooth * Time.deltaTime);
    }
}
