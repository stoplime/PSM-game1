using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;

namespace Project1
{
	public abstract class BaseEnemy : GameObject
	{
		protected float speed;
		protected Player player;
		protected int delay;
		protected bool kill;
		
		public bool Kill
		{
			get{return kill;}
			set{kill = value;}
		}
		
		public BaseEnemy ()
		{
			if (!GetPlayer()) {
				kill = true;
			}else{
				kill = false;
			}
		}
		
		public bool GetPlayer ()
		{
			if (GameStats.Players[0] != null) {
				Player closestP = (Player)GameStats.Players[0];
				float closeDist = GameStats.Players[0].Pos.DistanceSquared(this.pos);
				for (int i = 0; i < GameStats.Players.Count; i++) {
					float dist = GameStats.Players[i].Pos.DistanceSquared(this.pos);
					if (dist < closeDist) {
						closeDist = dist;
						closestP = (Player)GameStats.Players[i];
					}
				}
				player = closestP;
				return true;
			}else{
				return false;
			}
		}
		
		public float GetAngle ()
		{
			return player.Pos.Angle(this.pos);
		}
		
		public abstract void Update();
		
		public virtual void Render()
		{
			
			sprite.Render();
		}
		
	}
}

