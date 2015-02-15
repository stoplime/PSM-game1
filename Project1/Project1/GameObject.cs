using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;

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
					return sprite.Width;
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
		
		
	}
}

