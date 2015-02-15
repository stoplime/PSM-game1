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
				Bullet b = new Bullet(graphics,pos+GameStats.bulletAdjust,GameStats.Direction,alignPlayer);
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

