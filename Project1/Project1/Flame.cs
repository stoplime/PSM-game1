//******************************** Steffen Lim *******************************
using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;


namespace Project1
{
	public class Flame : BaseBullet
	{
		private static Random rand;
		public const int MAX_DELAY = 45;
		public const float MAX_SPREAD = FMath.PI/4;
		private Vector3 vel;
		
		public Flame (GraphicsContext graphics, Vector3 basePos, MoveDir direction, bool alignPlayer, float orAangle) : base(graphics,basePos,direction,alignPlayer)
		{
			sprite = new Sprite(graphics,GameStats.BulletTexs[(int)WeaponType.FlameThrower]);
			sprite.Center = new Vector2(0.5f,0.5f);
			sprite.Position = pos;
			sprite.Rotation = GameStats.Angle;
			delay = MAX_DELAY;
			rand = new Random();
			
			float angle = (((float)rand.NextDouble())-(0.5f*(float)rand.NextDouble()))*MAX_SPREAD;
			if (alignPlayer) {
				angle += GameStats.Angle;
			}else{
				angle += orAangle;
				sprite.Rotation = orAangle;
			}
			vel = new Vector3(FMath.Cos(angle),FMath.Sin(angle),0)*(GameStats.FLAME_SPD/2*((float)rand.NextDouble()+1f));
		}
		
		public Flame (GraphicsContext graphics, Vector3 basePos, MoveDir direction, bool alignPlayer) : this (graphics,basePos,direction,alignPlayer,0){}
		
		public override void Update ()
		{
			List<GameObject> targets;
			if (alignPlayer) {
				targets = GameStats.Enemies;
			}else{
				targets = GameStats.Players;
			}
			
			for (int i = 0; i < targets.Count; i++) {
				float r = targets[i].GetRadius+4;
				if (pos.DistanceSquared(targets[i].Pos) <= r*r) {
					//target hits
					targets[i].GotHit(WeaponType.FlameThrower);
					this.kill = true;
					break;
				}
			}
			
			pos += vel;
			vel *= 0.99f;
			
			sprite.Position = pos;
			sprite.SetColor(1,1,1,1-(float)count/MAX_DELAY);
			base.Update ();
		}
		
		public override void Render ()
		{
			
			base.Render ();
		}
	}
}

