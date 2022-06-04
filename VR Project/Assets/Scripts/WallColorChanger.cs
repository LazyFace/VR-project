using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallColorChanger : MonoBehaviour
{
    [SerializeField] Color releasedColor;
    [SerializeField] Color pushedColor;
    [SerializeField] float lerpValue;
    private MeshRenderer meshRend;
    private bool isPushed = false;

    void Awake() => meshRend = GetComponent<MeshRenderer>();

    void Start() => meshRend.material.color = releasedColor;

    void Update()
    {
        if(isPushed)
        {
            meshRend.material.color = Color.Lerp(meshRend.material.color, pushedColor, lerpValue * Time.deltaTime);
        }
        else
        {
            meshRend.material.color = Color.Lerp(meshRend.material.color, releasedColor, lerpValue * Time.deltaTime);
        }
    }

    public void InteractionActivated()
    {
        isPushed = true;
    }

    public void InteractionReleased()
    {
        isPushed = false;
    }

}
