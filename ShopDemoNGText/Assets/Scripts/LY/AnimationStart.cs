using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class AnimationStart : MonoBehaviour {

#if UNITY_EDITOR
	public MovieTexture movieTeture;
	public RawImage rawImage;
	public float timeout = 4f;
#endif
	
	void Start () {		

#if UNITY_EDITOR
		rawImage.GetComponent<RawImage>().texture = movieTeture;
		movieTeture.Play();
		Invoke("ChangeScene",timeout);
#endif

#if ((UNITY_ANDROID || UNITY_IPHONE ) && !UNITY_EDITOR)
		Handheld.PlayFullScreenMovie("1.mp4", Color.black, FullScreenMovieControlMode.Hidden);
		Application.LoadLevel (1);
#endif


		
	}

	private void ChangeScene()
	{
		Application.LoadLevel (1);
	}

}
