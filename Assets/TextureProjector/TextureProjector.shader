Shader "Projector/TextureProjector" 
{
    Properties 
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    Subshader 
    {
        Pass
        {
            CGPROGRAM

            #pragma  vertex   vert
            #pragma  fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv     : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 uv     : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4x4 unity_Projector;
            float4x4 unity_ProjectorClip;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv     = mul(unity_Projector, v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return tex2Dproj(_MainTex, UNITY_PROJ_COORD(i.uv));
            }

            ENDCG
        }
    }
}