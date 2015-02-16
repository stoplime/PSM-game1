using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;

namespace Project1
{
	public abstract class BaseEnemy : GameObject
	{
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
			if (GameStats.Players.Count != 0) {
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
		
		public override void GotHit ()
		{
			for (int i = 0; i < GameStats.Enemies.Count; i++) {
				if (GameStats.Enemies[i] == this) {
					GameStats.Enemies.RemoveAt(i);
					return;
				}
			}
		}
		
		public float GetAngle ()
		{
			if (player != null) {
				return FMath.Atan2(player.Pos.Y-this.pos.Y,player.Pos.X-this.pos.X);
			}
			return 0f;
		}
		
		public override void Render()
		{
			
			sprite.Render();
		}
		
	}
}

