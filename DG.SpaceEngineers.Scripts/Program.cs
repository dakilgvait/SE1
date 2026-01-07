using System;
using Sandbox.ModAPI.Ingame;
using VRage.Game;

namespace DG.SpaceEngineers.Scripts
{
    public sealed class Program : MyGridProgram
    {
        // Your code goes between the next #endregion and #region

        public Program()
        {
            Runtime.UpdateFrequency = UpdateFrequency.Once;
        }

        public void Main(string argument, UpdateType updateSource)
        {

        }
    }
}