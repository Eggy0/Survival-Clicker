using UnityEngine;

public class HandRotation : MonoBehaviour
{

    void Update()
    {
        transform.Rotate(0,0,-36*Time.deltaTime);
    }
}
