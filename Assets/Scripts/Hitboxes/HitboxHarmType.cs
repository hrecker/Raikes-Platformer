// On a Hitbox: The type of enemy that this Hitbox can hit
// On a Hurtbox: The type of object that this object is
public enum HitboxHarmType
{
	PLAYER 			= 0x00000001,
	ENEMY			= 0x00000010,
	POWERUP_BLOCK	= 0x00000100,
	ALL				= 0x11111111,
	ALL_BUT_PLAYER	= 0x11111110,
	//We don't want enemies to be able
	//to spawn powerups.
	ALL_BUT_ENEMY	= 0x11111001
}