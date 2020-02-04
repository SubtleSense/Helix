using UnityEngine;

public class Rotate : MonoBehaviour // в билде работает не корректно.
{
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * -1, 0) * 500 * Time.deltaTime);
        }
    }
}
