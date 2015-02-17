//******************************** Steffen Lim *******************************
using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;

namespace Project1
{
	public class Trooper : BaseEnemy
	{
		private Vector3 vel;
		private Sprite spriteHead;
		private Pistol pistol;
		
		public const int hpMax = 20;
		
		public Trooper (GraphicsContext graphics, Vector3 pos):base(graphics)
		{
			hp = hpMax;
			sprite = new Sprite(graphics,GameStats.EnemyTexs[1],64,64);
			sprite.Center = new Vector2(0.5f,0.5f);
			sprite.Position = pos;
			this.pos = pos;
			spriteHead = new Sprite(graphics,GameStats.EnemyTexs[1],64,64);
			spriteHead.Center = new Vector2(0.5f,0.5f);
			spriteHead.Position = pos;
			spriteHead.SetTextureCoord(64,0,128,64);
			vel = Vector3.Zero;
			
			spriteHead.Rotation = GetAngle();
			pistol = new Pistol(graphics,false);
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
                vel += avoidenceVector*2/(closest*closest*neighborCount);
			}
			
		}
		
		public override void Update ()
		{
			GetPlayer();
			vel = player.Pos.Subtract(this.pos)/500;
			
			avoidNeighbors(GameStats.Enemies);
			
			float distance = player.Pos.Distance(this.pos);
			if (distance < 150) {
				pistol.Attack();
			}
			pistol.Angle = GetAngle();
			pistol.Update(pos);
			if (distance < 100) {
				vel = Vector3.Zero;
			}
			
			HpDisp(hpMax,pos,64,5,40);
			
			pos += vel;
			sprite.Position = pos;
			spriteHead.Position = pos;
			spriteHead.Rotation = GetAngle();
			
			if (kill) {
				Died();
			}
		}
		
		public override void Render ()
		{
			sprite.Render();
			spriteHead.Render();
			base.Render ();
		}
	}
}

