using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SliderNumber : MonoBehaviour {
	public Slider slider;
	public float value;
	public int scale = 1;
	Text text;

	void Start() {
		text = GetComponent <Text> ();

	}
	public int GetValue(){
		return (int) (value * scale);
	}
	void Update() {
		value = slider.value;
		text.text = "" + (value * scale);
	}
}
