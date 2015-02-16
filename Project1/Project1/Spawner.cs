using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;

namespace Project1
{
	public class Spawner
	{
		public const int spawnTimeRange = 300;
		public const int spawnDomain = 100;		//spawns outside a radius of 100 from players
		private const int spawnDomainSQ = spawnDomain*spawnDomain;
		private static Random rand;
		private int spawnTime;
		private GraphicsContext graphics;
		private Vector3 spawnPos;
		
		
		public Spawner (GraphicsContext graphics)
		{
			this.graphics = graphics;
			rand = new Random();
			Spawn();
		}
		
		public void Spawn ()
		{
			int eType = rand.Next(0,3);
			
			switch (eType) {
			case 1:
				spawnPos = TSpawnPoint();
				Turret turret = new Turret(graphics,spawnPos);
				GameStats.Enemies.Add(turret);
				break;
			case 2:
				spawnPos = TSpawnPoint();
				Trooper trooper = new Trooper(graphics,spawnPos);
				GameStats.Enemies.Add(trooper);
				break;
			default:
				spawnPos = SpiderSpawnPoint();
				Spider spider = new Spider(graphics,spawnPos);
				GameStats.Enemies.Add(spider);
				break;
			}
		}
		
		private Vector3 TSpawnPoint ()	//for troopers/turrets
		{
			Vector3 tPos = new Vector3();
			bool tooClose = true;
			while(tooClose)
			{
				tPos.X = rand.Next(0,graphics.Screen.Width);
				tPos.Y = rand.Next(0,graphics.Screen.Height);
				foreach(Player p in GameStats.Players){
					if (p.Pos.DistanceSquared(tPos) > spawnDomainSQ) {
						tooClose = false;
					}
				}
			}
			return tPos;
		}
		
		private Vector3 SpiderSpawnPoint ()
		{
			Vector3 sPos = new Vector3();
			int side = rand.Next(0,4);
			switch (side) {
			case 1:
				sPos.X = graphics.Screen.Width + 50;
				sPos.Y = rand.Next(0,graphics.Screen.Height);
				break;
			case 2:
				sPos.Y = graphics.Screen.Height + 50;
				sPos.X = rand.Next(0,graphics.Screen.Width);
				break;
			case 3:
				sPos.X = -50;
				sPos.Y = rand.Next(0,graphics.Screen.Height);
				break;
			default:
				sPos.Y = -50;
				sPos.X = rand.Next(0,graphics.Screen.Width);
				break;
			}
			return sPos;
		}
		
		public void Update ()
		{
			spawnTime--;
			if (spawnTime <= 0) {
				spawnTime = rand.Next(0,spawnTimeRange)+GameStats.SPAWN_SPD;
				int amount = rand.Next(1,5);
				for (int i = 0; i < amount; i++) {
					Spawn();
				}
			}
		}
		
		
	}
}

