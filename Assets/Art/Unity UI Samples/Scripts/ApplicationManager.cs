using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class ApplicationManager : MonoBehaviour {
	
	[SerializeField]
	AudioClip soundtrack;


	private void Start()
	{
		SoundManager.Instance.PlayMusic(soundtrack);
	}
	public void LoadScene(string name)
	{
		SceneManager.LoadScene(name);
		if (name == "Main Menu")
        {
			Debug.Log("Now");
			FindObjectOfType<PanelManager>().OnEnable();
        }
	}

	public void Quit () 
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}
}
