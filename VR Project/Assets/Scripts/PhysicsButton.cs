using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicsButton : MonoBehaviour
{
    private bool isPressed;
    private Vector3 startPos;
    private ConfigurableJoint joint;

    public UnityEvent onPressed, onReleased;

    void Awake()
    {
        joint = GetComponent<ConfigurableJoint>();
        startPos = transform.localPosition;
    }

    void Update()
    {
        if(!isPressed && GetValue() >= 0.1f)
        {
            Pressed();
        }
        if(isPressed && GetValue() <= 0.03)
        {
            Released();
        }
    }

    void Pressed()
    {
        isPressed = true;
        onPressed?.Invoke();
    }

    void Released()
    {
        isPressed = false;
        onReleased?.Invoke();
    }

    float GetValue()
    {
        float value = Vector3.Distance(startPos, transform.localPosition) / joint.linearLimit.limit;

        return Mathf.Clamp(value, -1f, 1f);
    }


}
