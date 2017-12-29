using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour, IProgressIndicator {
	public Text _loadingText;
	public Image _loadingImage;
	private string _displayText; 
	private float _progress = 0;

	// Use this for initialization
	void Start () {
	}

	void Reset(){
		_loadingText = CollectionUtils.Only(gameObject.GetComponentsInChildren<Text> ());
		_loadingImage =  CollectionUtils.Only(gameObject.GetComponentsInChildren<Image> ());
		if (_loadingText == null || _loadingImage == null){
			Debug.LogWarning("Loading bar component has no associated image or text");
			enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (_loadingText == null || _loadingImage == null) {
			enabled = false;
			return;
		}
		_loadingImage.rectTransform.anchorMax = new Vector2 (_progress, 1);
		_loadingText.text = 100*_progress+" % - "+_displayText;
	}

	public void SetProgress(float progress){
		_progress = progress;
	}

	public void SetText(string text){
		_displayText = text;
	}
}
