//******************************** Steffen Lim *******************************
using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;

namespace Project1
{
	public class Player : GameObject
	{
		private static List<WeaponType> obtainedWeapon = GameStats.ObtainedWeapon;
		//private static List<long> ammo = GameStats.Ammo;
		private bool died;
		private float speed;
		private Weapon selectedWeapon;
		private GraphicsContext graphics;
		private int weaponID;
		private bool moving;
		private int iter;
		private int count;
		
		public const float MIN_SPD = GameStats.MIN_SPD;
		public const float MAX_SPD = GameStats.MAX_SPD;
		
//		public Vector3 Pos
//		{
//			get{return pos;}
//			set{pos = value;}
//		}
		public bool Died
		{
			get{return died;}
			set{died = value;}
		}
		public Player (GraphicsContext graphics, Texture2D spriteSheet):base(graphics)
		{
			this.graphics = graphics;
			sprite = new Sprite(graphics, spriteSheet, 64, 64);
			hp = GameStats.Hp;
			pos = new Vector3(graphics.Screen.Width/4,graphics.Screen.Height/2,0);
			speed = MIN_SPD;
			moving = false;
			died = false;
			iter = 0;
			count = 0;
			
			selectedWeapon = NewWeapon(WeaponType.Sword);
			weaponID = (int)WeaponType.Sword;
			if (!obtainedWeapon.Contains(WeaponType.Sword)) {
				obtainedWeapon.Add(WeaponType.Sword);
			}
			
			//***************** Pre-aquired Weapons *****************
			if (!obtainedWeapon.Contains(WeaponType.Pistol)) {
				obtainedWeapon.Add(WeaponType.Pistol);
			}
			if (!obtainedWeapon.Contains(WeaponType.FlameThrower)) {
				obtainedWeapon.Add(WeaponType.FlameThrower);
			}
			
			sprite.Center = new Vector2(0.5f,0.5f);
			sprite.Position = pos;
			sprite.Rotation = GameStats.Angle;
		}
		
		public override void GotHit (WeaponType wType)
		{
			base.GotHit(wType);
			if (hp <= 0) {
				//Game Over
			}
		}
		
		public Weapon NewWeapon (WeaponType type)
		{
			switch (type) {
			case WeaponType.Pistol:
				return new Pistol(graphics,true);
			case WeaponType.FlameThrower:
				return new FlameThrower(graphics,true);
			default:
				return new Sword(graphics,true);
			}
		}
		
		public override void Update(GamePadData gamePadData)
		{
			// Movement:
			if ((gamePadData.Buttons & GamePadButtons.Circle) != 0) {
				speed = MAX_SPD;
			}else{
				speed = MIN_SPD;
			}
			
			float width = sprite.Width;
			float height = sprite.Height;
			
			if ((gamePadData.Buttons & GamePadButtons.Up) != 0) {
				if(pos.Y > height/2){
					pos.Y-=speed;
				}
				GameStats.Direction = MoveDir.Up;
			}
			if ((gamePadData.Buttons & GamePadButtons.Down) != 0) {
				if(pos.Y < graphics.Screen.Height-height/2){
					pos.Y+=speed;
				}
				GameStats.Direction = MoveDir.Down;
			}
			if ((gamePadData.Buttons & GamePadButtons.Left) != 0) {
				if(pos.X > width/2){
				pos.X-=speed;
				}
				GameStats.Direction = MoveDir.Left;
			}
			if ((gamePadData.Buttons & GamePadButtons.Right) != 0) {
				if(pos.X < graphics.Screen.Width-width/2){
				pos.X+=speed;
				}
				GameStats.Direction = MoveDir.Right;
			}
			if ((gamePadData.Buttons & (GamePadButtons.Up | GamePadButtons.Down | GamePadButtons.Left | GamePadButtons.Right)) != 0) {
				moving = true;
				sprite.Rotation = GameStats.Angle;
			}else{
				moving = false;
			}
			
			//Switch Weapons
			if ((gamePadData.ButtonsDown & GamePadButtons.R) != 0) {
				if (weaponID < obtainedWeapon.Count-1) {
					weaponID++;
				}else{
					weaponID = 0;
				}
				selectedWeapon = NewWeapon(obtainedWeapon[weaponID]);
			}
			if ((gamePadData.ButtonsDown & GamePadButtons.L) != 0) {
				if (weaponID > 0) {
					weaponID--;
				}else{
					weaponID = obtainedWeapon.Count-1;
				}
				selectedWeapon = NewWeapon(obtainedWeapon[weaponID]);
			}
			
			//Fire
			if ((gamePadData.Buttons & GamePadButtons.Cross) != 0){
				selectedWeapon.Attack();
			}
			
			//HP
			HpDisp(GameStats.Hp,new Vector3(graphics.Screen.Width/2,40,0),600,30,0);
			
			selectedWeapon.Update(pos);
			
			sprite.Position = pos;
			count++;
			if (count % 1000 == 0) {
				count = 0;
			}
		}
		
		public override void Render()
		{
			if (moving) {
				if (count % 10 == 0) {
					if (iter < 7) {
						iter++;
					}else {
						iter = 0;
					}
					sprite.SetTextureCoord(iter*sprite.Width,0,(iter+1)*sprite.Width,sprite.Height);	//horizontal spritesheet
				}
			}else{
				if (iter != 0) {
					iter = 0;
					sprite.SetTextureCoord(iter*sprite.Width,0,(iter+1)*sprite.Width,sprite.Height);	//horizontal spritesheet
				}
			}
			
			sprite.Render();
			selectedWeapon.Render();
			base.Render();
		}
		
	}
}

