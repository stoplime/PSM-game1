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
		private static Spawner spawner;
		private static Sprite bg;
		
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
			Texture2D floorTex = new Texture2D("/Application/assets/floor.png",false);
			
			Texture2D playerTex = new Texture2D("/Application/assets/playerSheet.png",false);
			
			Texture2D spiderTex = new Texture2D("/Application/assets/SpiderSheet.png",false);
			Texture2D trooperTex = new Texture2D("/Application/assets/Trooper.png",false);
			Texture2D turretTex = new Texture2D("/Application/assets/Turret.png",false);
			
			Texture2D swordTex = new Texture2D("/Application/assets/sword.png",false);
			Texture2D pistolTex = new Texture2D("/Application/assets/pistol.png",false);
			Texture2D flameThrowerTex = new Texture2D("/Application/assets/flameThrower.png",false);
			
			Texture2D swordSwingTex = new Texture2D("/Application/assets/swordSwingSheet.png",false);
			Texture2D pistolBulletTex = new Texture2D("/Application/assets/bulletSmall.png",false);
			Texture2D flameTex = new Texture2D("/Application/assets/bulletSmall.png",false);
			
			List<Texture2D> enemyTextures = new List<Texture2D>();
			enemyTextures.Add(spiderTex);
			enemyTextures.Add(trooperTex);
			enemyTextures.Add(turretTex);
			
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
			GameStats.Players.Add(player);
			Spider s = new Spider(graphics, new Vector3(graphics.Screen.Width/2,graphics.Screen.Height/2,0));
			GameStats.Enemies.Add(s);
			
			bg = new Sprite(graphics,floorTex);
			
			//Vars:
			
			
			spawner = new Spawner(graphics);
			
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
			GameStats.Players[0].Update(gamePadData);
			//update enemies
			
			
			foreach(GameObject e in GameStats.Enemies)
			{
				e.Update();
			}
			
			//update items
			
			spawner.Update();
			
			
			
		}
		
		//**************************************************************
		public static void Render ()
		{
			// Clear the screen
			graphics.SetClearColor (0.0f, 0.3f, 0.3f, 0.0f);
			graphics.Clear ();
			// Draw Game
			bg.Render();
			foreach(GameObject e in GameStats.Enemies)
			{
				e.Render();
			}
			foreach(GameObject p in GameStats.Players)
			{
				p.Render();
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
