using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;

namespace Project1
{
	public static class GameStats
	{
		public static List<GameObject> Enemies, Players = new List<GameObject>();
		public static List<BaseBullet> Ebullets, Pbullets = new List<BaseBullet>();
		
		public static List<WeaponType> ObtainedWeapon = new List<WeaponType>();
		public static List<long> Ammo = new List<long>();
		
		public static List<Texture2D> EnemyTexs = new List<Texture2D>();
		public static List<Texture2D> WeaponTexs = new List<Texture2D>();
		public static List<Texture2D> BulletTexs = new List<Texture2D>();
		
		public static int Hp = 10;
		public static MoveDir Direction = MoveDir.Right;
		
		
		public const float MIN_SPD = 1.5f;
		public const float MAX_SPD = 3f;
		public const float BULLET_SPD = 5f;
		public const float FLAME_SPD = 3.5f;
		public const int RELOAD_SPD = 30;
		
		public static Vector2 SwordCenter = new Vector2(-0.12f,0.32f);
		public static Vector2 pistolCenter = new Vector2(0.23f,0.35f);
		public static Vector2 flameTCenter = new Vector2(0.23f,0.35f);
		
		public static Vector3 bulletAdjust
		{
			get{
				switch (Direction) {
				case MoveDir.Up:
					return new Vector3(6,-32,0);
				case MoveDir.Left:
					return new Vector3(-32,-6,0);
				case MoveDir.Down:
					return new Vector3(-6,32,0);
				default:
					return new Vector3(32,6,0);
				}
			}
		}
		
		public static Vector3 flameAdjust
		{
			get{
				switch (Direction) {
				case MoveDir.Up:
					return new Vector3(6,-38,0);
				case MoveDir.Left:
					return new Vector3(-38,-6,0);
				case MoveDir.Down:
					return new Vector3(-6,38,0);
				default:
					return new Vector3(38,6,0);
				}
			}
		}
		
		public static float Angle
		{
			get{
				switch (Direction) {
				case MoveDir.Up:
					return -FMath.PI/2;
				case MoveDir.Left:
					return FMath.PI;
				case MoveDir.Down:
					return FMath.PI/2;
				default:
					return 0;
				}
			}
		}
		
	}
}

