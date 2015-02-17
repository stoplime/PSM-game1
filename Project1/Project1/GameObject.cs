//******************************** Steffen Lim *******************************
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
		protected int hp;
		
		private Sprite hpSprite;
		
		public float GetRadius
		{
			get{
				if (sprite != null) {
					return sprite.Width/2;
				}
				return 0;
			}
		}
		
		public void HpDisp (int hpMax,Vector3 pos,float width,float height,int offsetY)
		{
			hpSprite.Width = ((float)hp/hpMax)*width;
			hpSprite.Height = height;
			if (hp < (float)hpMax/5) {
				hpSprite.SetColor(1f,0f,0f,0.5f);
			}else if (hp < (float)hpMax/2) {
				hpSprite.SetColor(1f,0.5f,0f,0.5f);
			}else{
				hpSprite.SetColor(0f,1f,0f,0.5f);
			}
			hpSprite.Position = pos;
			hpSprite.Position.Y -= offsetY;
		}
		
		public Vector3 Pos
		{
			get{return pos;}
			set{pos = value;}
		}
		
		public GameObject (GraphicsContext graphics)
		{
			hpSprite = new Sprite(graphics,GameStats.pixel);
			hpSprite.Center = new Vector2(0.5f,1f);
		}
		
		public virtual void GotHit(WeaponType t)
		{
			switch (t) {
			case WeaponType.Sword:
				hp-=50;
				break;
			case WeaponType.Pistol:
				hp-=10;
				break;
			case WeaponType.FlameThrower:
				hp-=1;
				break;
			}
		}
		
		public virtual void Update(){}
		public virtual void Update(GamePadData gamePadData){}
		
		public virtual void Render()
		{
			hpSprite.Render();
		}
		
	}
}

