using UnityEngine;

public class TitleTextAnimation : MonoBehaviour
{
    public float tiltSpeed;
    public float tiltAngle;


    void Update()
    {
        transform.rotation = Quaternion.Euler(0,0,Mathf.Sin(Time.time*tiltSpeed)*tiltAngle);
    }
}
