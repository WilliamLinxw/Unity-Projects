using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class WormholeDemo : MonoBehaviour{

	public float fudge = 0.1f;
	public Renderer wormhole;
	public Material SkyboxA;
	public Material SkyboxB;
	private Cubemap CubemapA;
	private Cubemap CubemapB;

	public void Start(){
		RenderSettings.skybox = new Material(RenderSettings.skybox);
	}

	public virtual void OnCollisionEnter(Collision collision){
		collision.transform.position = collision.transform.position + (transform.position - collision.transform.position) * (2f + fudge);
		Texture temp = RenderSettings.skybox.GetTexture("_Tex");
		RenderSettings.skybox.SetTexture("_Tex", wormhole.material.GetTexture("_Exit"));
		wormhole.material.SetTexture("_Exit", temp);
	}
}