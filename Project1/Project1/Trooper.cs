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
		
		public Trooper (GraphicsContext graphics, Vector3 pos)
		{
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
			
			if (vel.Length() > 2) {
				vel.Normalize();
				vel *= 2;
			}
			
			if (player.Pos.Distance(this.pos) < 100) {
				vel = Vector3.Zero;
			}
			vel.Z = 0;
			
			pos += vel;
			sprite.Position = pos;
			spriteHead.Position = pos;
			spriteHead.Rotation = GetAngle();
			
			if (kill) {
				GotHit();
			}
		}
		
		public override void Render ()
		{
			base.Render ();
			spriteHead.Render();
		}
	}
}

