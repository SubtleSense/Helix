using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeRotateTest : MonoBehaviour
{

    private Touch touch;
    private Vector2 touchPosition;
    private Quaternion rotationY;
    private float rotateSpeedModifier = 0.1f;


    void Update()
    {

        //if (Input.GetMouseButton(0))
        //{
        //    transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * -1, 0) * 10 * Time.deltaTime);
        //}
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                rotationY = Quaternion.Euler(
                    0f,
                    -touch.deltaPosition.x * rotateSpeedModifier,
                    0f);
                transform.rotation = rotationY * transform.rotation;
            }
        }
    }
}