using UnityEngine;
using UnityEngine.UI;

public class IndicatorMethod : MonoBehaviour {

	public static IndicatorMethod _instance;
	private Vector2 sizeChange;
	private Color initColour;

	void Awake(){
		if (_instance != null) {
			return;
		}
		initColour = GetComponent<Image> ().color;
		sizeChange = GetComponent<RectTransform> ().sizeDelta;
		_instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<RectTransform> ().sizeDelta = Vector2.Lerp (GetComponent<RectTransform> ().sizeDelta, sizeChange, 8*Time.deltaTime);
	}

	public void ChangeColour(Color newColour){
		GetComponent<Image> ().color = newColour;
	}

	public void RevertColour(){
		GetComponent<Image> ().color = initColour;
	}

	public void EnableIndicator(bool isEnabled){
		gameObject.SetActive(isEnabled);
	}

	public void SetSize(float size){
		sizeChange = new Vector2 (size, size);
	}
}
