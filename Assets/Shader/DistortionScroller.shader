Shader "Custom/DistortionScroller"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _Color2 ("Color 2", Color) = (1,1,1,1)
        _MainTex ("Texture (RGB)", 2D) = "white" {}
        _SecondTex("Secondary Texture (RGB)", 2D) = "white" {}
        _DistortionNormal("Distortion Normal", 2D) = "white" {}
        _DistortionScroll("Distortion Scroll Speed", Float) = 1
        _DistortionStrength("Distortion Strength", Float) = 1
        _DistortionScale("Distortion Scale", Float) = 1
        _TextureScroll("Texture Scroll Speed", Float) = -1
        _PixelizationCoord("Pixelization Size", Int) = 256
    }
    SubShader
    {
        Tags { "Queue" = "Background" "RenderType" = "Background" "PreviewType" = "Skybox" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _SecondTex;
        sampler2D _DistortionNormal;

        struct Input
        {
            float2 uv_MainTex;
        };

        fixed4 _Color, _Color2;

        float _DistortionScroll;
        float _TextureScroll;
        float _DistortionScale;
        float _DistortionStrength;
        int _PixelizationCoord;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float2 pixelUVs = floor(IN.uv_MainTex * _PixelizationCoord) / _PixelizationCoord;
            float4 dist = tex2D(_DistortionNormal, float2(pixelUVs.x + _Time.y * _DistortionScroll, pixelUVs.y + _Time.x * _DistortionScroll) * _DistortionScale)  * _DistortionStrength;
            float2 texScroll = float2(pixelUVs.x + dist.r + _Time.x * _TextureScroll, pixelUVs.y + dist.g + _Time.y * _TextureScroll);
            float2 texScrollMinus = float2(pixelUVs.x + dist.g - _Time.y * _TextureScroll, pixelUVs.y - dist.b + _Time.y * _TextureScroll);
            // Albedo comes from a texture tinted by color
            fixed4 c = (tex2D(_MainTex, texScroll).r * _Color + tex2D(_SecondTex, texScrollMinus * -1).r * _Color2);

            o.Albedo = c.rgb;
            o.Emission = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
