﻿using UnityEngine;
using System.Collections;


public enum MessageName
{
    Play_MonsterAttackFence, // 몬스터가 울타리를 공격함. int형 변수, 몬스터의 id를 넘김.
	Play_MonsterAttackFarm, // 몬스터가 농장을 공격함 int형 변수 몬스터의 id를 넘김.
    Play_MonsterAttackPlayer, // 몬스터가 플레이어를 공격함.  int형 변수 player_id와 monster_power를 넘김.
    Play_MonsterAttackTool, // 몬스터가 도구를 공격함. tool_id와 monster_power를 넘김.
    Play_MonsterDamagedByMissle, // 몬스터가 총알에 맞음. monster_id와 missle_power를 받음.
    Play_MonsterDied,//몬스터가 체력이 다해 죽음. int형 변수 몬스터의 id를 넘김.
    Play_MonsterReturn,//제한시간이 다해서 몬스터가 원래 있던 위치로 돌아감.

    Play_FenceDamagedByFence, // 울타리가 툴에게 공격당함.
    Play_ToolDamagedByMonster, // 툴이 공격당함.
    Play_ToolAttackMonster, // 툴이 공격함.
    Play_PlayerDamagedByMonster, // 플레이어가 몬스터에게 공격당함.
    Play_PlayerMove, // 플레이어가 특정 위치로 이동함


    Play_MissleAttackMonster, //미사일이 몬스터를 공격함. monster_id와 missle_power,missle_id를 넘김.
    Play_MissleOrderedByTool,//미사일이 tool에 의해 공격하라고 명령받음(?) tool_id과 tool의 현재 position을 받음
    Play_MissleDisappear,// 미사일이 맵 밖으로 사라져서 setactive를 꺼줘야할 때 사용.
	
    Play_OneWaveOver, // 한 웨이브가 끝남

    Play_StageClear, // 스테이지 클리어.
    Play_StageFailed,//스테이지 클리어 실패.

    Play_SceneChangeToHome,
    Play_SceneChangeToNextStage,
    Play_SceneChangeToRestart,

    Play_CameraMove, // 드래그해서 카메라를 이동시킴.

    Play_UIWaveTimerAmount,// 타이머 UI에서 사용할 웨이브의 (현재 남은 시간/총 주어진 시간) 을 UI에게 넘겨줌.
    Play_UIMaintainTimerAmount,//타이머 UI에서 사용할 정비시간의 (현재 남은 시간/총 주어진 시간) 을 UI에게 넘겨줌.

    Play_MaintainOver,//정비시간이 지나고 gamestate를 다시 웨이브가 몰려오는 start state로 바꿈. wavecount를 넘김.(UI컨트롤러에서 사용)

    Play_FenceDie,//펜스가 사라진것을 펜스컨트롤러에게 알림. id를 넘김
    Play_FenceDisappear_MonsterMove//펜스가 사라진것을 몬스터 컨트롤러에게 알려서 몬스터를 다시 움직이게 함.

}


public enum GameState
{
	/* GameLoading */
	GameLoading_Ready,	//게임을 처음 켰을 때 상태, 터치를 받아 Main을 로드하기를 기다리는 상태이다.
	GameLoading_LoadMainScene,	//Main을 로드를 시작함.

    /* Loading */
    Loading_Ready,              // 다음 씬으로 넘어가기 위해 로딩 씬으로 전환된 상태.
    Loading_LoadMain,           // Main 씬으로 이동하는 상태
    Loading_LoadSelectChapter,      // SelectChapter 씬으로 이동하는 상태
    Loading_LoadSelectStage,        // SelectStage 씬으로 이동하는 상태
    Loading_LoadPlay,               // Play 씬으로 이동하는 상태
	
	/* Main */
	Main_Ready,	//Main이 처음 실행되었을 때 상태.
	Main_LoadMaintain,	//버튼을 눌러서 정비화면으로 가는 중.
    Main_LoadSelectChapter, // 버튼을 눌러서 챕터 선택 화면으로 가는 중.

    /* Select Chapter */
    SelectChapter_Ready, // SelectChapter가 처음 실행 되었을 때.
    SelectChapter_LoadSelectStage, // 버튼을 눌러서 스테이지 선택 화면으로 가는 중.

    /* Select Stage */
	SelectStage_Ready, // SelectStage가 처음 실행 되었을 때.
    SelectStage_LoadStage, // 스테이지를 눌러서 플레이 화면으로 가는 중.

	/* Play */
	Play_Init,	//Init을 하는 중.
	Play_Reset,	//Init 후 초기화 하는 중.
	Play_Ready,	//초기화를 마치고 기다리는 중.
	Play_Start,
	Play_Management,
    Play_Clear,
    Play_Failed, //스테이지 클리어 실패

}

public enum ObjectState
{
	/* Play_Monster */
    Play_Monster_Reset,//초기화 시키는 기능.
    Play_Monster_Pause,// 일시정지 시키는 기능.
	Play_Monster_Ready, // 준비된 상태.
	Play_Monster_Move, // 몬스터가 이동하는 중.
	Play_Monster_Attack, // 몬스터가 다른 오브젝트(농장, 플레이어, 도구)에 부딛혀서 공격하는 중
	Play_Monster_Hitted, // 몬스터가 미사일에 맞은 상태.
	Play_Monster_Return,//제한시간이 끝나서 몬스터가 다시 되돌아가는 상태.
	Play_Monster_Die, // 몬스터가 죽은상태.
	
	/* Play_Missle */
    Play_Missle_Reset,//초기화
    Play_Missle_Pause,//일시정지
	Play_Missle_Ready,//미사일이 준비된 상태
	Play_Missle_Move, //미사일이 쏴져서 움직이고 있는 상태.
	
	/* Play_Player */
    Play_Player_Reset,//초기화
    Play_Player_Pause,//일시정지
    Play_Player_Ready,// 준비된 상태.
	Play_Player_CanHold,    // 플레이어가 아무것도 잡지 않아 도구를 잡을 수 있는 상태
	Play_Player_CanNotHold, // 플레이어가 도구를 잡을 수 없는 상태
	Play_Player_Die,        // 체력이 0 이하라서 움직일 수 없는 상태.
	
	/* Play_Tool */
    Play_Tool_Reset,//초기화
    Play_Tool_Pause,//일시정지
	Play_Tool_CanHeld,       // 툴이 플레이어에게 잡힐 수 있는 상태
	Play_Tool_CanNotHeld,    // 툴이 플레이어에게 잡혀있어 잡힐 수 없는 상태.
	Play_Tool_UnAvailable,   // 툴의 hp가 0이라서 사용 불가능한 상태.
	Play_Tool_CanAttack,      // 적이 사정거리 안에 들어오거나 농장 안에 있는 상태가 아니라서 툴이 공격 가능한 상태.  
	
	/* Play_Fence */
	Play_Fence_Died,        // 울타리의 hp가 0이라서 사용 불가능한 상태
	Play_Fence_Alive        // 울타리가 사용 가능한 상태.
}

public enum PlayerState
{
    Play_Player_CanHold, // 플레이어가 아무것도 잡지 않아 도구를 잡을 수 있는 상태
    Play_Player_CanNotHold // 플레이어가 도구를 잡을 수 없는 상태
}



