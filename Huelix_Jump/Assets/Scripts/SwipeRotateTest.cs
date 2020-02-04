using UnityEngine;

public class SwipeRotateTest : MonoBehaviour //работает только в билде.
{

    private Touch touch;
    private Quaternion rotationY;
    private float rotateSpeedModifier = 0.1f;


    void Update()
    {
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