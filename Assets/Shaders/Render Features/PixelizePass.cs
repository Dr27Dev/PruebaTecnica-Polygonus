using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PixelizePass : ScriptableRenderPass
{
    private PixelizeFeature.CustomPassSettings settings;

    private RenderTargetIdentifier colorBuffer, pixelBuffer;
    private int pixelBufferID = Shader.PropertyToID("_PixelBuffer");

    private RenderTargetIdentifier pointBuffer;
    private int pointBufferID = Shader.PropertyToID("_PointBuffer");

    private Material material;
    private int pixelScreenHeight, pixelScreenWidth;

    public PixelizePass(PixelizeFeature.CustomPassSettings settings)
    {
        this.settings = settings;
        renderPassEvent = settings.renderPassEvent;
        if (material == null) material = CoreUtils.CreateEngineMaterial("Custom/Pixelize");
    }

    public override void OnCameraSetup(CommandBuffer cmdb, ref RenderingData renderingData)
    {
        colorBuffer = renderingData.cameraData.renderer.cameraColorTarget;
        RenderTextureDescriptor descriptor = renderingData.cameraData.cameraTargetDescriptor;

        cmdb.GetTemporaryRT(pointBufferID, descriptor.width, descriptor.height, 0, FilterMode.Point);
        pointBuffer = new RenderTargetIdentifier(pointBufferID);

        pixelScreenHeight = settings.screenHeight;
        pixelScreenWidth = (int)(pixelScreenHeight * renderingData.cameraData.camera.aspect + 0.5f);

        material.SetVector("_BlockCount", new Vector2(pixelScreenWidth, pixelScreenHeight));
        material.SetVector("_BlockSize", new Vector2(1.0f / pixelScreenWidth, 1.0f / pixelScreenHeight));
        material.SetVector("_HalfBlockSize", new Vector2(0.5f / pixelScreenWidth, 0.5f / pixelScreenHeight));

        descriptor.height = pixelScreenHeight;
        descriptor.width = pixelScreenWidth;

        cmdb.GetTemporaryRT(pixelBufferID, descriptor, FilterMode.Point);
        pixelBuffer = new RenderTargetIdentifier(pixelBufferID);
    }

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        CommandBuffer cmdb = CommandBufferPool.Get();
        using (new ProfilingScope(cmdb, new ProfilingSampler("Pixelize Pass")))
        {
            // No-shader variant
            Blit(cmdb, colorBuffer, pointBuffer);
            Blit(cmdb, pointBuffer, pixelBuffer);
            Blit(cmdb, pixelBuffer, colorBuffer);

            Blit(cmdb, colorBuffer, pixelBuffer, material);
            Blit(cmdb, pixelBuffer, colorBuffer);
        }

        context.ExecuteCommandBuffer(cmdb);
        CommandBufferPool.Release(cmdb);
    }

    public override void OnCameraCleanup(CommandBuffer cmdb)
    {
        if (cmdb == null) throw new System.ArgumentNullException("cmdb");
        cmdb.ReleaseTemporaryRT(pixelBufferID);
        cmdb.ReleaseTemporaryRT(pointBufferID);
    }

}