using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

namespace Project1
{
	public abstract class Weapon
	{
		protected Sprite sprite;
		protected Vector3 pos;
		protected bool alignPlayer;
		protected GraphicsContext graphics;
		
		private WeaponType type;
		private int ammo;
		
		/// <summary>
		/// Initializes a new instance of the <see cref="Project1.Weapon"/> class.
		/// </summary>
		/// <param name='type'>
		/// Type of weapon.
		/// </param>
		/// <param name='ammo'>
		/// Ammo for weapon.
		/// </param>
		public Weapon (WeaponType type, int ammo)
		{
			this.type = type;
			if(type != WeaponType.Sword){
				this.ammo = ammo;
			}else{
				this.ammo = -1;
			}
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="Project1.Weapon"/> class with INFINITE ammo.
		/// </summary>
		/// <param name='type'>
		/// Type of weapon
		/// </param>
		public Weapon (WeaponType type):this(type,-1){}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="Project1.Weapon"/> class as a BASIC Sword.
		/// </summary>
		public Weapon ():this(WeaponType.Sword,-1){}
		
		//gets direction and attacks
		public abstract void Attack ();
		
		public abstract void Update (Vector3 pos);
		
		public abstract void Render ();
		
		
	}
}

