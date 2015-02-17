//******************************** Steffen Lim *******************************
using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;

namespace Project1
{
	public class Spider : BaseEnemy
	{
		private Vector3 vel;
		private int iter;
		private int count;
		private float speed;
		private int biteDelay;
		
		public const int biteDelayMAX = 60;
		public const int hpMax = 50;
		
		public Spider (GraphicsContext graphics, Vector3 pos):base(graphics)
		{
			hp = hpMax;
			sprite = new Sprite(graphics,GameStats.EnemyTexs[0],64,64);
			sprite.Center = new Vector2(0.5f,0.5f);
			sprite.Position = pos;
			this.pos = pos;
			vel = Vector3.Zero;
			iter = 0;
			count = 0;
			speed = 0;
			biteDelay = 0;
			
			sprite.Rotation = GetAngle();
			
		}
		
		public void avoidNeighbors (List<GameObject> objectList)
		{
			Vector3 avoidenceVector = Vector3.Zero;
			int neighborCount = 0;
			float closest = 50;
			
			foreach(GameObject b in objectList){
				if(this != b){
					float close = Vector3.Distance(this.pos,b.Pos);
	                if(close < 50){
						neighborCount++;
						avoidenceVector += Vector3.Subtract(this.pos,b.Pos);
						if(close < closest){
							closest = close;
						}
					}
				}
			}
         	if(neighborCount > 0){
                vel += avoidenceVector/100;
			}
			
		}
		
		public override void Update ()
		{
			GetPlayer();
			vel = player.Pos.Subtract(this.pos)/200;
			
			avoidNeighbors(GameStats.Enemies);
			
			if (pos.Distance(player.Pos) < 30) {
				if (biteDelay >= biteDelayMAX) {
					player.GotHit(WeaponType.Sword);
					biteDelay = 0;
				}
			}
			
			speed = vel.Length();
			
			HpDisp(hpMax,pos,64,5,35);
			
			pos += vel;
			sprite.Position = pos;
			sprite.Rotation = GetAngle();
			
			if (kill) {
				Died();
			}
			count++;
			biteDelay++;
		}
		
		public override void Render ()
		{
			if (count*speed/5 > 1) {
				if (iter < 3) {
					iter++;
				}else{
					iter = 0;
				}
				count = 0;
				sprite.SetTextureCoord(iter*sprite.Width,0,(iter+1)*sprite.Width,sprite.Height);
			}
			sprite.Render();
			base.Render ();
		}
	}
}

