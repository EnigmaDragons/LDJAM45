using UnityEngine;

public class SetAsGameCameraOnAwake : MonoBehaviour
{
    [SerializeField] private GameSceneSharedObjects shared;
    [SerializeField] private Camera gameCamera;

    private void Awake()
    {
        shared.gameCamera = gameCamera;
    }
}
