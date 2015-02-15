using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;

namespace Project1
{
	public class Sword : Weapon
	{
		private SwordSwing swing;
		
		public Sword (GraphicsContext graphics, bool alignPlayer):base()
		{
			this.graphics = graphics;
			this.alignPlayer = alignPlayer;
			
			sprite = new Sprite(graphics,GameStats.WeaponTexs[(int)WeaponType.Sword]);
			sprite.Center = GameStats.SwordCenter;
			
			
		}
		
		public override void Attack ()
		{
			if (swing == null) {
				swing = new SwordSwing(graphics,pos,GameStats.Direction,alignPlayer);
			}
			
		}
		
		public override void Update (Vector3 pos)
		{
			this.pos = pos;
			if (swing != null) {
				swing.GetMoving(pos);
				swing.Update();
				if (swing.Kill) {
					swing = null;
				}
			}
			sprite.Rotation = GameStats.Angle;
			sprite.Position = pos;
		}
		
		public override void Render ()
		{
			if (swing != null) {
				swing.Render();
			} else {
				sprite.Render();
			}
		}
		
	}
}

