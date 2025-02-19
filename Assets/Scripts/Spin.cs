using UnityEngine;

public class Spin : MonoBehaviour
{
    float rotationSpeed;
    void Start()
    {
        rotationSpeed = 25;
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            transform.RotateAround(OpenFIle.bound.center, Vector3.down, Input.GetAxis("Mouse X") * rotationSpeed);
            transform.RotateAround(OpenFIle.bound.center, Vector3.left, Input.GetAxis("Mouse Y") * rotationSpeed); 
        }
    } 
}
