Shader "Custom/Stencil"
{
	Properties
	{		
		_MaskTint("Mask Tint", Color) = (1,1,1,1)
		
		_Stencil ("Stencil ID [0;255]", Float) = 1
		[Enum(UnityEngine.Rendering.CompareFunction)] _StencilComp ("Stencil Comparison", Int) = 8
		[Enum(UnityEngine.Rendering.StencilOp)] _StencilOp ("Stencil Operation", Int) = 2

		_Offset("Offset", float) = -0.5
		[Enum(None,0,Alpha,1,Red,8,Green,4,Blue,2,RGB,14,RGBA,15)] _ColorMask("Color Mask", Int) = 14

		_Cutoff ("Mask alpha cutoff", Range(0.0, 1.0)) = 0.1
	}
	
	CGINCLUDE

	#pragma multi_compile_local _ PIXELSNAP_ON
    #pragma multi_compile _ ETC1_EXTERNAL_ALPHA
    #include "UnitySprites.cginc"

    fixed4 _MaskTint;
    fixed _Cutoff;

    struct appdata_masking
    {
        float4 vertex : POSITION;
        half2 texcoord : TEXCOORD0;
    };

    struct v2f_masking
    {
        float4 pos : SV_POSITION;
        half2 uv : TEXCOORD0;
        UNITY_VERTEX_OUTPUT_STEREO
    };

    v2f_masking vert(appdata_masking IN)
    {
        v2f_masking OUT;

        UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);

        OUT.pos = UnityObjectToClipPos(IN.vertex);
        OUT.uv = IN.texcoord;

        return OUT;
    }


    fixed4 frag(v2f_masking IN) : SV_Target
    {
        fixed4 c = SampleSpriteTexture(IN.uv) * _MaskTint;
        clip (c.a - _Cutoff);
        return c;
    }

	ENDCG
		
	SubShader
	{
		Tags 
		{ 
			"RenderType"="Opaque" 
			"Queue" = "Geometry" 
			"CanUseSpriteAtlas"="True"
		}
		LOD 100
		Offset [_Offset], [_Offset]
		ColorMask [_ColorMask]

		Stencil
		{
			Ref [_Stencil]
			Comp [_StencilComp]
			Pass [_StencilOp] 
		}

		Pass
		{
			CGPROGRAM
			#pragma target 3.0
			#pragma vertex vert
			#pragma fragment frag
			ENDCG
		}
	}
}