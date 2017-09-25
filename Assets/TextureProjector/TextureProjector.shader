Shader "Projector/TextureProjector" 
{
    Properties 
    {
        _MainTex ("Main Texture", 2D) = "white" {}
    }

    Subshader 
    {
        Tags
        {
            "Queue" = "Transparent"
        }
        Pass
        {
            ZWrite Off

            CGPROGRAM

            #pragma  vertex   vert
            #pragma  fragment frag
            #include "UnityCG.cginc"
            
            struct v2f
            {
                float4 uvTexture  : TEXCOORD0;
                float4 pos        : SV_POSITION;
            };

            sampler2D _MainTex;
            float4x4 unity_Projector;
            float4x4 unity_ProjectorClip;
            
            v2f vert (float4 vertex : POSITION)
            {
                v2f o;
                o.pos       = UnityObjectToClipPos (vertex);
                o.uvTexture = mul (unity_Projector, vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return tex2Dproj (_MainTex, UNITY_PROJ_COORD(i.uvTexture));
            }

            ENDCG
        }
    }
}