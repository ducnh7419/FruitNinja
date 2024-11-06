using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[System.Serializable, VolumeComponentMenu("Custom/Frost Effect")]
public class FrostEffectVolume : VolumeComponent, IPostProcessComponent
{
    [Range(0f, 1f)]
    public FloatParameter frostAmount = new FloatParameter(0.5f);
    
    [Range(1f, 10f)]
    public FloatParameter edgeSharpness = new FloatParameter(1f);
    
    [Range(0f, 1f)]
    public FloatParameter minFrost = new FloatParameter(0f);
    
    [Range(0f, 1f)]
    public FloatParameter maxFrost = new FloatParameter(1f);
    
    [Range(0f, 1f)]
    public FloatParameter seethroughness = new FloatParameter(0.2f);
    
    [Range(0f, 1f)]
    public FloatParameter distortion = new FloatParameter(0.1f);

    public TextureParameter frostTexture = new TextureParameter(null);
    public TextureParameter frostNormalMap = new TextureParameter(null);

    public bool IsActive() => frostAmount.value > 0f;
    public bool IsTileCompatible() => false;
}
