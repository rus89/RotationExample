using System;
using UnityEngine;

public class PlayerControllerRotation : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform playerCamera;

    private float middleOfScreen;
    private float direction;
    private float speed;

    private bool inside;
    private int levelNumber;

#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_OSX
    private void OnValidate()
    {
        Debug.Assert(target != null, "target==null", gameObject);
        Debug.Assert(playerCamera != null, "playerCamera==null", gameObject);
    }
#endif

    // Start is called before the first frame update
    private void Start()
    {
        ++levelNumber;
        inside = levelNumber % 2 == 1;
        speed = 80f;
        middleOfScreen = Screen.width / 2;
    }

    // Update is called once per frame
    private void Update()
    {
        RotationDirection();
        playerCamera.RotateAround(target.position, direction * target.forward, speed * Time.deltaTime);
    }

    private void RotationDirection()
    {
#if UNITY_EDITOR
        if (!Input.GetMouseButton(0))
        {
            direction = 0;
            return;
        }

        if (Input.mousePosition.x > middleOfScreen)
        {
            direction = inside ? 1 : -1;
        }
        else
        {
            direction = inside ? -1 : 1;
        }

#elif UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount == 0)
        {
            direction = 0;
            return;
        }

        if (Input.touches[0].position.x > middleOfScreen)
        {
            direction = inside ? 1 : -1;
        }
        else
        {
            direction = inside ? -1 : 1;
        }
#endif
    }
}