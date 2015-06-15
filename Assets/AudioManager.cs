using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {
	public Text playPauseButton;
	public AudioSource[] trackList;
	public int track = 0;
	public float volume = 0f;
	public Slider volumeLevel;
	bool play = false;
	NetworkView nView;
	
	void Start() {
		nView = GetComponent<NetworkView>();
	}
	public void TogglePlay(){
		play = !play;
		if (play) {
			nView.RPC("PlayBackgroundMusic",RPCMode.AllBuffered);
			playPauseButton.text = "Pause";
		} 
		else {
			playPauseButton.text = "Play";
			nView.RPC("PauseBackgroundMusic",RPCMode.AllBuffered);
		}
	}
	public void TrackSelect(int trackNumber) {
		nView.RPC("PauseBackgroundMusic",RPCMode.AllBuffered);
		nView.RPC("SetTrack",RPCMode.AllBuffered, trackNumber);
		nView.RPC("PlayBackgroundMusic",RPCMode.AllBuffered);
	}
	public void AdjustVolume(){
		volume = volumeLevel.value;
		nView.RPC("SetVolume", RPCMode.All, volume);
	}
	[RPC]
	void PlayBackgroundMusic() {
		trackList[track].Play ();
	}

	[RPC]
	void PauseBackgroundMusic() {
		trackList[track].Pause();
	}

	[RPC] 
	void SetTrack(int n){
		track = n;
	}
	[RPC]
	void SetVolume(float volume){
		trackList[track].volume = volume;
	}
}
