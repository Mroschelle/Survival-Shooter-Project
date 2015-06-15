using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour {
	public GameObject ServerUI;
	public GameObject PlayerUI;
	public GameObject player;
	public Text connections;
	NetworkView nView;
	string registeredGameName = "Micah_Survival_Shooter";
	bool IsRefreshing = false;
	float refreshRequestLength = 3.0f;
	HostData[] hostData;
	int numberConnections = 0;

	void Start() {
		nView = GetComponent<NetworkView>();
	}

	
	public void StartServer()
	{
		Network.InitializeServer (16, 25002, false);
		MasterServer.RegisterHost (registeredGameName, "Survival Shooter", "Good Server");
		ServerUI.SetActive (true);
		PlayerUI.SetActive (false);
	}
	
	void OnServerInitialized(){
		Debug.Log ("Server has be set up");
	}
	void OnMasterServerEvent(MasterServerEvent masterServerEvent)
	{
		if (masterServerEvent == MasterServerEvent.RegistrationSucceeded)
			Debug.Log ("Registeration Successfull");
	}
	
	public IEnumerator RefreshHostList()
	{
		Debug.Log ("Refreshing...");
		MasterServer.RequestHostList (registeredGameName);
		float timeStarted = Time.time;
		float timeEnd = Time.time + refreshRequestLength;
		
		while (Time.time < timeEnd) {
			
			hostData = MasterServer.PollHostList ();
			yield return new WaitForEndOfFrame();
		}
		if (hostData == null || hostData.Length == 0)
			Debug.Log ("No Active servers found");
		else
			Debug.Log (hostData.Length + " have been Found");
	}
	public void Disconnect(){
		if (Network.isServer) {
			MasterServer.UnregisterHost ();
			ServerUI.SetActive(false);
		}
		Network.Disconnect (200);
	}
	void OnApplicationQuit(){
		if (Network.isServer) {
			Network.Disconnect (200);
			MasterServer.UnregisterHost ();
		}
		if (Network.isClient) {
			Network.Disconnect (200);
		}
	}
	public void OnGUI()
	{
		/*if(GUI.Button (new Rect (200f,200f,150f,30f),"Start Player")){
			Debug.Log ("Spawning Player");
			Network.Instantiate (player, new Vector3 (0, 0, 0), Quaternion.identity, 0);
		}*/
		if (Network.isServer) {
			GUILayout.Label("Running as server...");

		}
		if (Network.isClient) {
			GUILayout.Label("Running as client...");
		}
		if (Network.isClient || Network.isServer)
			return;
			//Makes initial buttons dissappear
		
		if (GUI.Button (new Rect (25f, 25f, 150f, 30f), "Start New Server")) {
			StartServer ();
			//Starts Server
		}

		if (GUI.Button (new Rect (25f, 65f, 150f, 30f), "Refresh Server List")) 
		{
			StartCoroutine ("RefreshHostList");	
		}
		
		if (hostData != null) 
		{
			for(int i = 0; i < hostData.Length; i++)
			{
				if(GUI.Button (new Rect(25f, 125f, 150f, 30f), hostData[i].gameName))
				{
					Network.Connect (hostData[i]);

					nView.RPC ("UpdateConnections", RPCMode.Server);

				}
				
			}
		}
	}
	[RPC]
	void UpdateConnections(){
		Debug.Log ("Client Connected");
		numberConnections++;
		connections.text = "Connections: " + numberConnections;
	}
}