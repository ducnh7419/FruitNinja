using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class FrostEffectFeature : ScriptableRendererFeature
{
    [System.Serializable]
    public class FrostSettings
    {
        public Material material;
        public Texture2D Frost; //RGBA
        public Texture2D FrostNormals; //normalmap
        public Shader Shader; //ImageBlendEffect.shader
        [Range(0, 1)] public float frostAmount = 0.5f;
        [Range(1, 10)] public float edgeSharpness = 1;
        [Range(0, 1)] public float minFrost = 0;
        [Range(0, 1)] public float maxFrost = 1;
        [Range(0, 1)] public float seeThroughness = 0.2f;
        [Range(0, 1)] public float distortion = 0.1f;
    }

    public FrostSettings settings = new FrostSettings();
    Material frostMaterial;
    private FrostEffectPass frostEffectPass;
    [NonSerialized]public bool enabledFeature = false;

    public override void Create()
    {
        frostMaterial = new Material(settings.Shader);
        frostMaterial.SetTexture("_BlendTex", settings.Frost);
        frostMaterial.SetTexture("_BumpMap", settings.FrostNormals);
        frostMaterial.SetFloat("_BlendAmount", Mathf.Clamp01(settings.frostAmount));
        frostMaterial.SetFloat("_EdgeSharpness", settings.edgeSharpness);
        frostMaterial.SetFloat("_SeeThroughness", settings.seeThroughness);
        frostMaterial.SetFloat("_Distortion", settings.distortion);
        frostEffectPass = new FrostEffectPass(frostMaterial);
        frostEffectPass.renderPassEvent = RenderPassEvent.AfterRenderingPostProcessing; // Adjust as needed
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (!enabledFeature) return;
        if (frostMaterial != null && renderingData.cameraData.renderType == CameraRenderType.Base)
        {
            renderer.EnqueuePass(frostEffectPass);
        }
    }

    public void SetEnableFeature(bool enable){
            enabledFeature=enable;
    }

    public class FrostEffectPass : ScriptableRenderPass
    {
        private Material frostMaterial;
        private RTHandle tempTexture;
        RTHandle cameraColorTarget;

        public FrostEffectPass(Material material)
        {
            frostMaterial = material;
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            if (frostMaterial == null)
                return;
            CommandBuffer cmd = CommandBufferPool.Get("FrostEffectPass");
            if (cameraColorTarget == null)
                // Get the camera color target
                cameraColorTarget = renderingData.cameraData.renderer.cameraColorTargetHandle;

            // Create a temporary texture for processing
            if (tempTexture == null || !tempTexture.rt)
            {
                tempTexture = RTHandles.Alloc(cameraColorTarget.rt.width, cameraColorTarget.rt.height,
                    filterMode: FilterMode.Bilinear, name: "TempFrostTexture");
            }
            if (tempTexture == null || cameraColorTarget == null) return;
            // Blit the camera color target to the temporary texture
            cmd.Blit(cameraColorTarget, tempTexture, frostMaterial);

            // Blit the temporary texture back to the camera target
            cmd.Blit(tempTexture, cameraColorTarget);

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

        public override void OnCameraCleanup(CommandBuffer cmd)
        {
            // Giải phóng RTHandle khi kết thúc xử lý cho camera
            if (tempTexture != null)
            {
                tempTexture.Release();
                tempTexture = null;
            }
        }

        

    }
}
