using UnityEngine;

[CreateAssetMenu]
public class GameSceneSharedObjects : ScriptableObject
{
    public Camera gameCamera;
    public float TileWidthUnits = 10f;
    public float ScreenHeightUnits => 6 * TileWidthUnits;
    public float ScreenWidthUnits => 9 * TileWidthUnits;
    public AudioSource catAudioSource;    
}
