Shader "Unlit/StencilMask"
{
    Properties
    {
        [IntRange] _StencilID("Stencil ID",Range(0,255))=0
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" "Queue" = "Geometry-1" "RenderPipeline" = "UniversalPipline" }
        LOD 100

        Pass
        {
            Blend Zero One
            ZWrite Off

            Stencil
            {
                ref [_StencilID]
                Comp Always
                Pass Replace
            }
        }
    }
}
