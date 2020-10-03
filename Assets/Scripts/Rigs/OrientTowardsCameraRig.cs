using GameplayIngredients;
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class OrientTowardsCameraRig : MonoBehaviour
{
    public bool InvertFacing = false;

    public enum UpVector
    {
        WorldUp,
        CameraUp,
        Custom
    }

    public UpVector upVector = UpVector.WorldUp;

    [ShowIf("isCustomUpVector")]
    public Vector3 CustomUpVector;

    VirtualCameraManager m_VCM;

    bool isCustomUpVector() => upVector == UpVector.Custom;

    private void Start()
    {
        m_VCM = Manager.Get<VirtualCameraManager>();
    }

    void LateUpdate()
    {
        transform.LookAt(m_VCM.transform.position, GetUpVector());

        if(InvertFacing)
            transform.forward = -transform.forward;
    }

    Vector3 GetUpVector()
    {
        switch (upVector)
        {
            default:
            case UpVector.WorldUp:
                return Vector3.up;
            case UpVector.CameraUp:
                return m_VCM.transform.up;
            case UpVector.Custom:
                return CustomUpVector;

        }
    }
}
