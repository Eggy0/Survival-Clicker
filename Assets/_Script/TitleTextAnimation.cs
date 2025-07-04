using UnityEngine;

public class TitleTextAnimation : MonoBehaviour
{
    public float tiltSpeed;
    public float tiltAngle;
    public float floatSpeed;
    public float floatAmplitude;

    void Update()
    {
        transform.rotation = Quaternion.Euler(0,0,Mathf.Sin(Time.time*tiltSpeed)*tiltAngle);
        transform.position = new Vector3(transform.position.x, transform.position.y + Mathf.Sin(Time.time*floatSpeed)*floatAmplitude * Time.deltaTime, transform.position.z);
    }
}
