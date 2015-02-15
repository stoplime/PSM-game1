//******************************** Steffen Lim *******************************
using System;
using System.Collections.Generic;
using System.Diagnostics;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;

using Sce.PlayStation.HighLevel.UI;

namespace Project1
{
	public class AppMain
	{
		//Global Vars: *************************************************
		private static GraphicsContext graphics;
		private static DeveloperDisplay hud;
		private static Player player;
		
		//**************************************************************
		public static void Main (string[] args)
		{
			Initialize ();

			while (true) {
				hud.StartTime = hud.Miliseconds;
				SystemEvents.CheckEvents ();
				Update ();
				Render ();
				hud.StopTime = hud.Miliseconds;
				hud.TimeDelta = hud.StopTime-hud.StartTime;
			}
		}
		//**************************************************************
		public static void Initialize ()
		{
			// system setup:
			graphics = new GraphicsContext ();
			UISystem.Initialize(graphics);
			hud = new DeveloperDisplay(graphics);
			
			//initialize textures:
			Texture2D playerTex = new Texture2D("/Application/assets/playerSheet.png",false);
			
			Texture2D spiderTex = new Texture2D("/Application/assets/SpiderSheet.png",false);
			Texture2D turretTex = new Texture2D("/Application/assets/Turret.png",false);
			
			
			Texture2D swordTex = new Texture2D("/Application/assets/sword.png",false);
			Texture2D pistolTex = new Texture2D("/Application/assets/pistol.png",false);
			Texture2D flameThrowerTex = new Texture2D("/Application/assets/flameThrower.png",false);
			
			Texture2D swordSwingTex = new Texture2D("/Application/assets/swordSwingSheet.png",false);
			Texture2D pistolBulletTex = new Texture2D("/Application/assets/bulletSmall.png",false);
			Texture2D flameTex = new Texture2D("/Application/assets/bulletSmall.png",false);
			
			List<Texture2D> enemyTextures = new List<Texture2D>();
			
			
			List<Texture2D> weaponTextures = new List<Texture2D>();
			weaponTextures.Add(swordTex);
			weaponTextures.Add(pistolTex);
			weaponTextures.Add(flameThrowerTex);
			
			List<Texture2D> bulletTextures = new List<Texture2D>();
			bulletTextures.Add(swordSwingTex);
			bulletTextures.Add(pistolBulletTex);
			bulletTextures.Add(flameTex);
			
			//initialize static texture lists
			GameStats.EnemyTexs = enemyTextures;
			GameStats.WeaponTexs = weaponTextures;
			GameStats.BulletTexs = bulletTextures;
			
			//objects
			player = new Player(graphics,playerTex);
			GameStats.Enemies = new List<GameObject>();
			GameStats.Ebullets = new List<BaseBullet>();
			
			//Vars:
			GameStats.Players.Add(player);
			for (int i = 0; i < 3; i++) {
				GameStats.Ammo.Add(-1);
			}
			
			
		}
		//**************************************************************
		public static void Update ()
		{
			// Query gamepad for current state
			var gamePadData = GamePad.GetData (0);
			// update system : UI, Music, GameState
			hud.UpdateFPS();
			
			//update lists
			for (int i = 0; i < GameStats.Pbullets.Count; i++) {
				GameStats.Pbullets[i].Update();
				if (GameStats.Pbullets[i].Kill) {
					GameStats.Pbullets.RemoveAt(i);
				}
			}
			for (int i = 0; i < GameStats.Ebullets.Count; i++) {
				GameStats.Ebullets[i].Update();
				if (GameStats.Ebullets[i].Kill) {
					GameStats.Ebullets.RemoveAt(i);
				}
			}
			
			//update player
			foreach(GameObject p in GameStats.Players)
			{
				if (p is Player) {
					Player pl = (Player)p;
					pl.Update(gamePadData);
				}
			}
			//update enemies
			foreach(GameObject e in GameStats.Enemies)
			{
				if (e is BaseEnemy) {
					BaseEnemy en = (BaseEnemy)e;
					en.Update();
				}
			}
			//update items
			
			
			
		}
		
		//**************************************************************
		public static void Render ()
		{
			// Clear the screen
			graphics.SetClearColor (0.0f, 0.0f, 0.0f, 0.0f);
			graphics.Clear ();
			// Draw Game
			foreach(GameObject p in GameStats.Players)
			{
				if (p is Player) {
					Player pl = (Player)p;
					pl.Render();
				}
			}
			foreach(GameObject e in GameStats.Enemies)
			{
				if (e is BaseEnemy) {
					BaseEnemy en = (BaseEnemy)e;
					en.Render();
				}
			}
			foreach(BaseBullet b in GameStats.Pbullets)
			{
				b.Render();
			}
			foreach(BaseBullet b in GameStats.Ebullets)
			{
				b.Render();
			}
			
			// Texts
			
			
			// Draw debuging
			hud.Render();

			// Present the screen
			graphics.SwapBuffers ();
		}
	}
}
