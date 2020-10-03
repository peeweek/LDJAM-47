using GameplayIngredients;

[ManagerDefaultPrefab("UIManager")]
public class UIManager : Manager
{
    public Retroizer.Retroizer Retroizer;

    private void Start()
    {
        Retroizer.targetCamera = Manager.Get<VirtualCameraManager>().Camera;
    }
}
