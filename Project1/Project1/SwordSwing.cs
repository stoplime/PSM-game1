using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;

namespace Project1
{
	public class SwordSwing : BaseBullet
	{
		private const int reach = 96;			//distance from center to center of enemies in reach
		private const int rch = reach*reach;	//reach squared (for optimization)
		private int iter;						//animate frame interations
		
		public SwordSwing (GraphicsContext graphics, Vector3 basePos, MoveDir direction, bool alignPlayer)
			:base(graphics,basePos,direction,alignPlayer)
		{
			sprite = new Sprite(graphics,GameStats.BulletTexs[(int)WeaponType.Sword],64,64);
			sprite.Center = GameStats.SwordCenter;
			sprite.Position = pos;
			delay = 30;
			iter = 0;
			
			sprite.Rotation = GameStats.Angle;
		}
		
		private MoveDir GetDir(Vector3 basePos, Vector3 targetPos)
		{
			float ang = FMath.Atan2(targetPos.Y-basePos.Y,targetPos.X-basePos.X);
			if (ang <= FMath.PI && ang > 0) {
				return MoveDir.Right;
			}
			else if (ang <= FMath.PI/2 && ang > -FMath.PI/2) {
				return MoveDir.Down;
			}
			else if (ang <= 0 && ang > -FMath.PI) {
				return MoveDir.Left;
			}
			else {
				return MoveDir.Up;
			}
		}
		
		public void GetMoving (Vector3 pos)
		{
			this.pos = pos;
		}
		
		public override void Update ()
		{
			List<GameObject> targets;
			if (alignPlayer) {
				targets = GameStats.Enemies;
			}else{
				targets = GameStats.Players;
			}
			
			if (count >= 10 && targets != null) {
				for (int i = 0; i < targets.Count; i++) {
					float distSq = Vector3.DistanceSquared(basePos,targets[i].Pos);			//distance
					if (distSq <= rch) {													//check distance
						if (direction == GetDir(basePos,targets[i].Pos)) {					//check angle
							targets[i].GotHit();											//iniciate damage
						}
					}
				}
			}
			sprite.Position = pos;
			sprite.Rotation = GameStats.Angle;
			
			base.Update();
		}
		
		public override void Render ()
		{
			if (count % 10 == 0) {
				if (iter < 2) {
					iter++;
					sprite.SetTextureCoord(iter*sprite.Width,0,(iter+1)*sprite.Width,sprite.Height);	//horizontal spritesheet
				}
			}
			base.Render ();
		}
	}
}

