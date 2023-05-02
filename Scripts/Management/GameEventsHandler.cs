// The Game Events used across the Game.
// Anytime there is a need for a new event, it should be added here.

using System;
using System.Collections.Generic;
using UnityEngine;

public static class GameEventsHandler
{
    public static readonly GameStartEvent GameStartEvent = new GameStartEvent();
    public static readonly GameOverEvent GameOverEvent = new GameOverEvent();
    public static readonly MoneyCollectEvent MoneyCollectEvent = new MoneyCollectEvent();
    public static readonly FinisherStartEvent FinisherStartEvent = new FinisherStartEvent();
    public static readonly PlayerFinishLevelEvent PlayerFinishLevelEvent = new PlayerFinishLevelEvent();
    public static readonly TutorialShowEvent TutorialShowEvent = new TutorialShowEvent();
    public static readonly TutorialToggleEvent TutorialToggleEvent = new TutorialToggleEvent();
    public static readonly AmbianceChangeEvent AmbianceChangeEvent = new AmbianceChangeEvent();
    public static readonly PlayerBoostEvent PlayerBoostEvent = new PlayerBoostEvent();
    public static readonly GateWeightEvent GateWeightEvent = new GateWeightEvent();
    public static readonly GateSizeEvent GateSizeEvent = new GateSizeEvent();
    public static readonly GateSpikeEvent GateSpikeEvent = new GateSpikeEvent();
    public static readonly DebugCallEvent DebugCallEvent = new DebugCallEvent();
    public static readonly PlayerObstacleHitEvent PlayerObstacleHitEvent = new PlayerObstacleHitEvent();
    public static readonly PlayerCollectibleEvent PlayerCollectibleEvent = new PlayerCollectibleEvent();
    public static readonly LevelLoadEvent LevelLoadEvent = new LevelLoadEvent();
    public static readonly UpgradeButtonPressEvent UpgradeButtonPressEvent = new UpgradeButtonPressEvent();
    public static readonly StartScenePlayEvent StartScenePlayEvent = new StartScenePlayEvent();
    public static readonly FinisherEndEvent FinisherEndEvent = new FinisherEndEvent();
    public static readonly FinisherPlayerReachGroundEvent FinisherPlayerReachGroundEvent = new FinisherPlayerReachGroundEvent();
    public static readonly FinisherReplayEvent FinisherReplayEvent = new FinisherReplayEvent();
    public static readonly PlayerProgressEvent PlayerProgressEvent = new PlayerProgressEvent();
    public static readonly PlayerWeightChangeEvent PlayerWeightChangeEvent = new PlayerWeightChangeEvent();
}

public class GameEvent {}
public class GateEvent : GameEvent{} 
public class GameStartEvent : GameEvent
{
    public float PlayerHeight;
}

public class GameOverEvent : GameEvent
{
    public bool IsWin;
}

public class MoneyCollectEvent : GameEvent
{
    
}

public class FinisherStartEvent : GameEvent
{
    public Vector3 PlayerPosition;
}

public class StartScenePlayEvent : GameEvent{}

public class FinisherEndEvent : GameEvent
{
}

public class  PlayerFinishLevelEvent : GameEvent{}

public class TutorialShowEvent : GameEvent
{
}

public class TutorialToggleEvent : GameEvent
{
    public bool Toggle;
}


public class AmbianceChangeEvent : GameEvent
{
    public int Number;
}

public class PlayerCollectibleEvent : GameEvent
{
    
}
public class PlayerObstacleHitEvent : GameEvent
{
    public bool IsLethal;
    public Vector3 Contact;
}
public class PlayerBoostEvent : GateEvent
{
    
}
public class GateWeightEvent : GateEvent
{
    public bool IsGood;

}
public class GateSizeEvent : GateEvent
{
    public bool IsGood;
}
public class GateSpikeEvent : GateEvent{}

public class LevelLoadEvent : GameEvent
{
    public float[] Length;
}

public class UpgradeButtonPressEvent: GameEvent
{
    
}
public class FinisherPlayerReachGroundEvent : GameEvent
{
    
}
public class FinisherReplayEvent : GameEvent
{
}
public class PlayerProgressEvent : GameEvent
{
    public float Height;
}
public class PlayerWeightChangeEvent : GameEvent
{
    public float Progress;
}
public class DebugCallEvent : GameEvent
{
    public float Speed;
    public float Strafe;
    public float MinSize;
    public float MaxSize;
    public float DeltaSize;
}





