Shader "Custom/GoldCoinShader" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _Metallic ("Metallic", Range(0,1)) = 1
        _Glossiness ("Smoothness", Range(0,1)) = 1
    }

    SubShader {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        struct Input {
            float2 uv_MainTex;
        };

        sampler2D _MainTex;
        float _Metallic;
        float _Glossiness;

        void surf (Input IN, inout SurfaceOutputStandard o) {
            o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Normal = float3(0, 0, 1);
        }
        ENDCG
    }

    FallBack "Diffuse"
}