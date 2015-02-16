using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;

namespace Project1
{
	public class Turret : BaseEnemy
	{
		private Sprite spriteHead;
		private FlameThrower fire;
		
		public Turret (GraphicsContext graphics, Vector3 pos)
		{
			this.pos = pos;
			sprite = new Sprite(graphics,GameStats.EnemyTexs[2],64,64);
			sprite.Center = new Vector2(0.5f,0.5f);
			sprite.Position = pos;
			spriteHead = new Sprite(graphics,GameStats.EnemyTexs[2],64,64);
			spriteHead.Center = new Vector2(0.5f,0.5f);
			spriteHead.Position = pos;
			spriteHead.SetTextureCoord(64,0,128,64);
			
			spriteHead.Rotation = GetAngle();
		}
		
		public override void Update ()
		{
			GetPlayer();
			
			spriteHead.Rotation = GetAngle();
			
			if (kill) {
				GotHit();
			}
			
		}
		
		public override void Render ()
		{
			sprite.Render ();
			spriteHead.Render();
		}
	}
}

