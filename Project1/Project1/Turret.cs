//******************************** Steffen Lim *******************************
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
		
		public const int hpMax = 100;
		
		public Turret (GraphicsContext graphics, Vector3 pos):base(graphics)
		{
			hp = hpMax;
			this.pos = pos;
			sprite = new Sprite(graphics,GameStats.EnemyTexs[2],64,64);
			sprite.Center = new Vector2(0.5f,0.5f);
			sprite.Position = pos;
			spriteHead = new Sprite(graphics,GameStats.EnemyTexs[2],64,64);
			spriteHead.Center = new Vector2(0.5f,0.5f);
			spriteHead.Position = pos;
			spriteHead.SetTextureCoord(64,0,128,64);
			
			spriteHead.Rotation = GetAngle();
			
			fire = new FlameThrower(graphics,false);
			fire.Pos = pos;
		}
		
		public override void Update ()
		{
			GetPlayer();
			
			spriteHead.Rotation = GetAngle();
			
			HpDisp(hpMax,pos,64,5,30);
			
			if (player.Pos.Distance(pos)<150) {
				fire.Angle = GetAngle();
				fire.Attack();
			}
			fire.Update(pos);
			if (kill) {
				Died();
			}
			
		}
		
		public override void Render ()
		{
			sprite.Render ();
			spriteHead.Render();
			base.Render();
		}
	}
}

