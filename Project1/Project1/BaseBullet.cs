using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;

namespace Project1
{
	public abstract class BaseBullet
	{
		protected Sprite sprite;				//bullet sprite
		protected Vector3 pos;					//pos of bullet
		protected Vector3 basePos;				//pos of bullet origin (where it fired)
		protected MoveDir direction;			//direction of bullet
		protected int delay;					//delay till death
		protected int count;					//frame counter
		protected bool kill;					//Death to bullet indicator
		protected bool alignPlayer;
		
		public bool Kill
		{
			get{return kill;}
		}
		
		public BaseBullet (GraphicsContext graphics, Vector3 basePos, MoveDir direction, bool alignPlayer)
		{
			this.basePos = basePos;
			this.pos = basePos;
			this.direction = direction;
			this.count = 0;
			this.kill = false;
			this.alignPlayer = alignPlayer;
		}
		
		public virtual void Update()
		{
			count++;
			if (count >= delay) {
				kill = true;
			}
		}
		
		public virtual void Render()
		{
			sprite.Render();
		}
		
	}
}

