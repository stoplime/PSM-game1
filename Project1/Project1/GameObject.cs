using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;

namespace Project1
{
	public abstract class GameObject
	{
		protected Sprite sprite;
		protected Vector3 pos;
		
		public float GetRadius
		{
			get{
				if (sprite != null) {
					return sprite.Width/2;
				}
				return 0;
			}
		}
		
		public Vector3 Pos
		{
			get{return pos;}
			set{pos = value;}
		}
		
		public GameObject ()
		{
		}
		
		public abstract void GotHit();
		
		public virtual void Update(){}
		public virtual void Update(GamePadData gamePadData){}
		
		public abstract void Render();
		
	}
}

