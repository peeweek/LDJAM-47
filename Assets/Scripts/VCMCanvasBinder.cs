using GameplayIngredients;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class VCMCanvasBinder : MonoBehaviour
{
    void Start()
    {
        GetComponent<Canvas>().worldCamera = Manager.Get<VirtualCameraManager>().Camera;
    }


}
