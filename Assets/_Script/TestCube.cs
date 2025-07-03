using UnityEngine;

public class TestCube : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(-40*Time.deltaTime,40 * Time.deltaTime, 0);
    }
}
