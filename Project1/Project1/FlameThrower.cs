//******************************** Steffen Lim *******************************
using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;


namespace Project1
{
	public class FlameThrower : Weapon
	{
		private float angle;
		
		public float Angle
		{
			get{return angle;}
			set{angle = value;}
		}
		public Vector3 Pos 
		{
			get {return pos;}
			set {pos = value;}
		}
		
		public FlameThrower (GraphicsContext graphics, bool alignPlayer):base(WeaponType.FlameThrower)
		{
			this.graphics = graphics;
			this.alignPlayer = alignPlayer;
			angle = 0;
			
			sprite = new Sprite(graphics,GameStats.WeaponTexs[(int)WeaponType.FlameThrower]);
			sprite.Center = GameStats.flameTCenter;
		}
		
		public override void Attack ()
		{
			Flame f;
			if (alignPlayer) {
				f = new Flame(graphics,pos+GameStats.flameAdjust,GameStats.Direction,alignPlayer);
			}else{
				Vector3 adjustPos = pos;
				adjustPos.X += 15*FMath.Cos(angle);
				adjustPos.Y += 15*FMath.Sin(angle);
				f = new Flame(graphics,adjustPos,MoveDir.Right,alignPlayer,angle);
			}
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

