using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;


namespace Project1
{
	public class FlameThrower : Weapon
	{
		public FlameThrower (GraphicsContext graphics, bool alignPlayer):base(WeaponType.FlameThrower)
		{
			this.graphics = graphics;
			this.alignPlayer = alignPlayer;
			
			sprite = new Sprite(graphics,GameStats.WeaponTexs[(int)WeaponType.FlameThrower]);
			sprite.Center = GameStats.flameTCenter;
		}
		
		public override void Attack ()
		{
			Flame f = new Flame(graphics,pos+GameStats.flameAdjust,GameStats.Direction,alignPlayer);
			if (!alignPlayer) {
				GameStats.Ebullets.Add(f);
			}else{
				GameStats.Pbullets.Add(f);
			}
			
		}
		
		public override void Update (Vector3 pos)
		{
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

