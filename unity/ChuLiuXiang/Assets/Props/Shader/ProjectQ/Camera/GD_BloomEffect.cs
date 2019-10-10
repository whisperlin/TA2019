using UnityEngine;  
using System.Collections;  

[ExecuteInEditMode]  
public class GD_BloomEffect : GD_PostEffectBase  { 

	//分辨率  
	[Range(0, 9)]
	public int _DownSample = 2; 
	[Range(0,0.01f)]
	public float nose = 0;
	[Range(-1.0f,1.0f)]
	public float _Bloomrange = -0.15f;
	[Range(0,1)]
	public float _Add = 0;
	[Range(0,2)]
	public float _Red = 1;
	[Range(0,2)]
	public float _Green = 1;
	[Range(0,2)]
	public float _Blue = 1;
	//	public AnimationCurve redChannel = new AnimationCurve(new Keyframe(0f,0f), new Keyframe(1f,1f));
	//	public AnimationCurve greenChannel = new AnimationCurve(new Keyframe(0f,0f), new Keyframe(1f,1f));
	//	public AnimationCurve blueChannel = new AnimationCurve(new Keyframe(0f,0f), new Keyframe(1f,1f));

	//采样率  
	void OnRenderImage(RenderTexture source, RenderTexture destination){  
		if (_Material) { 

			RenderTexture temp1 = RenderTexture.GetTemporary(source.width >> _DownSample, source.height >> _DownSample, 0, source.format);  
			Graphics.Blit(source, temp1); 
			_Material.SetTexture("_EffectTex",temp1);
			_Material.SetFloat("_Bloomrange", _Bloomrange);  
			_Material.SetFloat("_Nose", nose);
			_Material.SetFloat("_Add", _Add);
			_Material.SetFloat ("_Red",_Red);
			_Material.SetFloat ("_Green",_Green);
			_Material.SetFloat ("_Blue",_Blue);
			Graphics.Blit (source, destination,_Material);
			RenderTexture.ReleaseTemporary(temp1); 
		}
	}
}