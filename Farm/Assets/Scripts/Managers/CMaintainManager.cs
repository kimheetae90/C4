using UnityEngine;
using System.Collections;

public class CMaintainManager : SceneManager {

	protected override void Awake()
	{
		base.Awake ();
	}
	
	void Start()
	{
	}
	
	void Update () {
		UpdateState ();
	}
	
	public override void DispatchInputData (InputData _inputData)
	{
	}
	
	public override void DispatchGameMessage (GameMessage _gameMessage)
	{
		_gameMessage.Destroy ();
	}
	
	protected override void UpdateState ()
	{		
	}
	
	protected override void ChangeState (GameState _gameState)
	{
		gameState = _gameState;
	}

	
	
	
	
	
	///////////////////////////////////////////////////////////////////////////////
	//////////////////////// 			구현               ////////////////////////
	///////////////////////////////////////////////////////////////////////////////



}
