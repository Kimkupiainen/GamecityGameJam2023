Shader "Custom/LCD Screen" {
    Properties{
        _MainTex("Base (RGB)", 2D) = "white" {}
        _CameraTex("Camera Texture", 2D) = "white" {}
        _OverlayTex("Overlay Texture", 2D) = "white" {}
        _OverlayTiling("Overlay Tiling", Range(1, 256)) = 1
        _OverlayIntensity("Overlay Intensity", Range(0, 25)) = 1
    }
        SubShader{
            Tags {"Queue" = "Transparent" "RenderType" = "Opaque"}
            LOD 100

            Pass {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct appdata {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f {
                    float2 uv : TEXCOORD0;
                    float2 overlayUv : TEXCOORD1;
                    float4 vertex : SV_POSITION;
                };

                sampler2D _MainTex;
                sampler2D _CameraTex;
                sampler2D _OverlayTex;
                float _OverlayTiling;
                float _OverlayIntensity;

                v2f vert(appdata v) {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    o.overlayUv = v.uv * _OverlayTiling;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target {
                    fixed4 col = tex2D(_MainTex, i.uv);
                    fixed4 camCol = tex2D(_CameraTex, i.uv);
                    col *= camCol;
                    fixed4 overlayCol = tex2D(_OverlayTex, i.overlayUv);
                    overlayCol.rgb *= _OverlayIntensity;
                    col.rgb *= overlayCol.rgb;
                    return col;
                }
                ENDCG
            }
        }
            FallBack "Diffuse"
}