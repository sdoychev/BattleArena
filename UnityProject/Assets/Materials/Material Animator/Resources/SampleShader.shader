Shader "Custom/SampleShader" {
	Properties {
		_MyColor ("My Color",Color) = (0.5,0.5,0.5,1)
		_MySecColor ("My Sec Color",Color) = (0,0,0,1)
		_MyMainTex ("My Texture (RGB)", 2D) = "white" {}
		_MySecTex ("My Sec Texture (A)", 2D) = "black" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" "IgnorePojector"="true"}
		LOD 200
		
		Material {
			Ambient [_MyColor]
			Diffuse [_MyColor]
			Emission [_MySecTex]
		}
		
		Lighting On
		SeparateSpecular On
		
		Pass {
			SetTexture [_MyMainTex] {
				combine texture * primary double
			}
			
			SetTexture [_MySecTex] {
				constantColor [_MySecColor]
				combine constant lerp(texture) previous double
			}
		}
	} 
	FallBack "Diffuse"
}
