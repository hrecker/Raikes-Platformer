﻿using System;

public enum HitboxHarmType
{
	PLAYER 			= 0x00000001,
	ENEMY			= 0x00000010,
	ALL				= 0x11111111,
	ALL_BUT_PLAYER	= 0x11111110,
	ALL_BUT_ENEMY	= 0x11111101
}