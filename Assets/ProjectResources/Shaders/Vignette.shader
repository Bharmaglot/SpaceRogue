Shader "Custom/Vignette"
{
    Properties
    {
        [HideInInspector] _MainTex("Main Texture", 2D) = "white" {}
        _Color("Color", Color) = (1,1,1,1)
        _CircleSize("Size", Range(0,1)) = 1
        _CircleTransition("Transition", Range(0,1)) = 1
    }

    SubShader
    {
        Tags
        {
            "Queue" = "Transparent"
            "RenderType" = "Transparent"
            "IgnoreProjector" = "True"
            "ForceNoShadowCasting" = "True"
            "PreviewType" = "Plane"
        }
    
        LOD 200
        Cull Off
        Blend SrcAlpha OneMinusSrcAlpha
    
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
    
            #include "UnityCG.cginc" 
    
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
    
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
    
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _CircleSize;
            float _CircleTransition;
            float4 _Color;

            float RadiusCorrection(float radius, float transition)
            {
                return (1 - (radius));
            }

            float Circle(float2 xy, float radius, float transition)
            {
                float2 dist = xy - float2(0.5, 0.5);
                return smoothstep(radius - transition , radius + transition, dot(dist, dist));
            }
    
            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }
    
            fixed4 frag(v2f i) : SV_Target
            {
                float circle = Circle(i.uv, RadiusCorrection(_CircleSize, _CircleTransition), _CircleTransition);
                fixed4 col = tex2D(_MainTex, i.uv) * circle * _Color;
                return col;
            }
            ENDCG
        }
    }
}
