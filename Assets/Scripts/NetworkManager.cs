using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks
{
	private const string LEVEL = "level";
	private const byte MAX_PLAYERS = 2;
	[SerializeField] private Text connectionStatusText;
	[SerializeField] private GameInitializer gameInitializer;
	//[SerializeField] private GameInitializer gameInitializer;
	//private MultiplayerChessGameController chessGameController;

	private GameSettings.EGameMode playerLevel;

	void Awake()
	{
		PhotonNetwork.AutomaticallySyncScene = true;
		if(GameMode.Instance.GetGameState() == State.MULTI) connectionStatusText.gameObject.SetActive(true);
		else connectionStatusText.gameObject.SetActive(false);
		playerLevel = GameSettings.Instance.GameMode();
	}

	//public void SetDependencies(MultiplayerChessGameController chessGameController)
	//{
	//	this.chessGameController = chessGameController;
	//}

	public void Connect()
	{
		if (PhotonNetwork.IsConnected)
		{
			PhotonNetwork.JoinRandomRoom(new ExitGames.Client.Photon.Hashtable() { { LEVEL, playerLevel } }, MAX_PLAYERS);
			//PhotonNetwork.JoinRandomRoom();
		}
		else
		{
			PhotonNetwork.ConnectUsingSettings();
		}
	}

	private void Update()
	{
		SetConnectionStatusText(PhotonNetwork.NetworkClientState.ToString());
	}

	#region Photon Callbacks

	public override void OnConnectedToMaster()
	{

		Debug.LogError($"Connected to server. Looking for random room with level {playerLevel}");
		PhotonNetwork.JoinRandomRoom(new ExitGames.Client.Photon.Hashtable() { { LEVEL, playerLevel } }, MAX_PLAYERS);
		//PhotonNetwork.JoinRandomRoom();
	}

	public override void OnJoinRandomFailed(short returnCode, string message)
	{
		Debug.LogError($"Joining random room failed becuse of {message}. Creating new one with player level {playerLevel}");	
		PhotonNetwork.CreateRoom(null, new RoomOptions
		{
			CustomRoomPropertiesForLobby = new string[] { LEVEL },
			MaxPlayers = MAX_PLAYERS,
			CustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { LEVEL, playerLevel } }
		});
		//PhotonNetwork.CreateRoom(null);
	}

	public override void OnJoinedRoom()
	{
		Debug.LogError($"Player {PhotonNetwork.LocalPlayer.ActorNumber} joined a room with level: {(GameSettings.EGameMode)PhotonNetwork.CurrentRoom.CustomProperties[LEVEL]}");
		PrepareTeamSelectionOptions();
		//uiManager.ShowTeamSelectionScreen();
		PhotonView pv =  gameInitializer.CreateMultiplayerBoard();
		connectionStatusText.gameObject.SetActive(false);
		if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
        {
			int _selectedData = Random.Range(0, SudokuData.Instance.SudokuGame[GameSettings.Instance.GetGameMode()].Count);
			pv.RPC("SetSelectedData", RpcTarget.All, _selectedData);
			gameInitializer.InitializeMultiplayerController();
		} 
	}


	private void PrepareTeamSelectionOptions()
	{
		if (PhotonNetwork.CurrentRoom.PlayerCount > 1)
		{
			var player = PhotonNetwork.CurrentRoom.GetPlayer(1);
			//if (player.CustomProperties.ContainsKey(TEAM))
			//{
			//	var occupiedTeam = player.CustomProperties[TEAM];
			//	uiManager.RestrictTeamChoice((TeamColor)occupiedTeam);
			//}
		}
	}

	public override void OnPlayerEnteredRoom(Player newPlayer)
	{
		Debug.LogError($"Player {newPlayer.ActorNumber} entered a room");
		gameInitializer.InitializeMultiplayerController();

	}

	#endregion

	public void SetPlayerLevel(GameSettings.EGameMode level)
	{
		PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { LEVEL, level } });
	}

	internal bool IsRoomFull()
	{
		return PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers;
	}

	public void SetConnectionStatusText(string text)
	{
		connectionStatusText.text = text;
	}

}
