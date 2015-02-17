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
	public class DeveloperDisplay
	{
		private GraphicsContext graphics;
		private static Stopwatch clock;
		private long startTime, stopTime, timeDelta;
		private long elapseTime;
		private Text FPS;
		private Text debug;
		private int frameCount;
		
		public long TimeDelta
		{
			get{return timeDelta;}
			set{timeDelta = value;}
		}
		public long StartTime
		{
			get{return startTime;}
			set{startTime = value;}
		}
		public long StopTime
		{
			get{return stopTime;}
			set{stopTime = value;}
		}
		public long Miliseconds
		{
			get
			{
				if(clock != null){
					return clock.ElapsedMilliseconds;
				}
				return 0;
			}
		}
		
		public DeveloperDisplay (GraphicsContext graphics)
		{
			this.graphics = graphics;
			debug = new Text(10,60,200,-1,-1,-1,"debug");
			FPS = new Text(10,10,200,-1,-1,-1,"FPS: ??");
			
			
			if(clock == null){
				clock = new Stopwatch();
				clock.Start();
			}
			
			elapseTime = 0;
			frameCount = 0;
		}
		
		public void UpdateFPS()
		{
			elapseTime += timeDelta;
			if(elapseTime >= 1000)
			{
				FPS.Update("FPS: " + frameCount);
				frameCount = 0;
				elapseTime -= 1000;
			}
		}
		
		public void Update(string debugString)
		{
			debug.Update(debugString);
			
		}
		
		public void Render()
		{
			FPS.Render();
			debug.Render();
			frameCount++;
		}
	}
}

