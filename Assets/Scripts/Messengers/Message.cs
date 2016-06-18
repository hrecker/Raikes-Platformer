﻿//types of messages that can be sent
public enum Message
{
    BOUNCE,
    DIRECTION_CHANGE,
    HEALTH_LOST,
    HEALTH_UPDATED,
    HEALTH_PICKUP,
	POINTS_RECEIVED,
    HIT_BY_OTHER,
    HIT_OTHER,
    LETTER_DESTROYED,
    NO_HEALTH_REMAINING,
    PROJECTILE_EXPIRED,
    PROJECTILE_FIRED,
    STARTED_MOVEMENT,
    STATE_CHANGE,
    STOPPED_MOVEMENT,
    TURN,
	PLATFORM_LANDED_ON,
	PLATFORM_COLLAPSED,
	PLATFORM_RESPAWNED,
	LANDED_ON_TRAMPOLINE_PLATFORM
}