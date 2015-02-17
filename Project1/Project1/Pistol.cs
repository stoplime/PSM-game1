//******************************** Steffen Lim *******************************
using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;

namespace Project1
{
	public class Pistol : Weapon
	{
		private int reload;
		private float angle;
		
		public float Angle
		{
			get{return angle;}
			set{angle = value;}
		}
		
		public Pistol (GraphicsContext graphics, bool alignPlayer):base(WeaponType.Pistol)
		{
			this.graphics = graphics;
			this.alignPlayer = alignPlayer;
			reload = 0;
			
			sprite = new Sprite(graphics,GameStats.WeaponTexs[(int)WeaponType.Pistol]);
			sprite.Center = GameStats.pistolCenter;
			
		}
		
		public override void Attack ()
		{
			if (reload > GameStats.RELOAD_SPD) {
				reload = 0;
				Bullet b;
				if (alignPlayer) {
					b = new Bullet(graphics,pos+GameStats.bulletAdjust,GameStats.Direction,alignPlayer);
				}else{
					Vector3 adjustPos = pos;
					adjustPos.X += 15*FMath.Cos(angle);
					adjustPos.Y += 15*FMath.Sin(angle);
					b = new Bullet(graphics,adjustPos,MoveDir.Right,alignPlayer,angle);
				}
				if (!alignPlayer) {
					GameStats.Ebullets.Add(b);
				}else{
					GameStats.Pbullets.Add(b);
				}
			}
		}
		
		public override void Update (Vector3 pos)
		{
			reload++;
			this.pos = pos;
			sprite.Rotation = GameStats.Angle;
			sprite.Position = pos;
		}
		
		public override void Render ()
		{
			sprite.Render();
		}
		
	}
}

