using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class StencilRenderFeature : ScriptableRendererFeature
{
    class StencilRenderPass : ScriptableRenderPass
    {
        public RenderTargetIdentifier source;
        private Material stencilMaterial;

        public StencilRenderPass(Material material)
        {
            this.stencilMaterial = material;
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            CommandBuffer cmd = CommandBufferPool.Get("Stencil Render Pass");

            cmd.SetRenderTarget(source);
            cmd.DrawMesh(RenderingUtils.fullscreenMesh, Matrix4x4.identity, stencilMaterial);
            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }
    }

    public Material stencilMaterial;
    StencilRenderPass stencilRenderPass;

    public override void Create()
    {
        stencilRenderPass = new StencilRenderPass(stencilMaterial);
        stencilRenderPass.renderPassEvent = RenderPassEvent.AfterRenderingTransparents;
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        stencilRenderPass.source = renderer.cameraColorTarget;
        renderer.EnqueuePass(stencilRenderPass);
    }
}
