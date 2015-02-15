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
		
		
		public Spider (GraphicsContext graphics, Vector3 pos)
		{
			sprite = new Sprite(graphics,GameStats.EnemyTexs[0],64,64);
			sprite.Center = new Vector2(0.5f,0.5f);
			sprite.Position = pos;
			vel = Vector3.Zero;
			
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
                vel += avoidenceVector*10/((1+closest*closest)*neighborCount);
			}
			
		}
		
		public override void GotHit ()
		{
			
		}
		
		public override void Update ()
		{
			
			
		}
		
		public override void Render ()
		{
			
			base.Render ();
		}
	}
}

