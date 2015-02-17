//******************************** Steffen Lim *******************************
using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;

namespace Project1
{
	public class Bullet : BaseBullet
	{
		private float angle;
		
		public Bullet (GraphicsContext graphics, Vector3 basePos, MoveDir direction, bool alignPlayer, float orAngle):base(graphics,basePos,direction,alignPlayer)
		{
			sprite = new Sprite(graphics,GameStats.BulletTexs[(int)WeaponType.Pistol]);
			sprite.Center = new Vector2(0.5f,0.5f);
			sprite.Position = pos;
			delay = 120;
			
			sprite.Rotation = GameStats.Angle;
			this.angle = orAngle;
			if(!alignPlayer){
				sprite.Rotation = orAngle;
			}
		}
		
		public Bullet (GraphicsContext graphics, Vector3 basePos, MoveDir direction, bool alignPlayer):this(graphics,basePos,direction,alignPlayer,0){}
		
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
					targets[i].GotHit(WeaponType.Pistol);
					this.kill = true;
					break;
				}
			}
			if (alignPlayer) {
				switch (direction) {
				case MoveDir.Up:
					pos.Y -= GameStats.BULLET_SPD;
					break;
				case MoveDir.Left:
					pos.X -= GameStats.BULLET_SPD;
					break;
				case MoveDir.Down:
					pos.Y += GameStats.BULLET_SPD;
					break;
				default:
					pos.X += GameStats.BULLET_SPD;
					break;
				}
			}else{
				Vector3 v = new Vector3(FMath.Cos(angle),FMath.Sin(angle),0);
				v*= GameStats.BULLET_SPD;
				pos += v;
			}
			
			sprite.Position = pos;
			base.Update ();
		}
		
		public override void Render ()
		{
			
			base.Render ();
		}
		
	}
}

