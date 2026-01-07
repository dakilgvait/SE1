using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using VRage.Game;
using VRage.ObjectBuilders;

// Change this namespace for each script you create.
namespace DGSpaceEngineers.Scripts.AutoAssembler
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
            var cmp = "ComputerComponent";
            var assemblers = new List<IMyAssembler>();
            var sorters = new List<IMyConveyorSorter>();
            var containers = new List<IMyCargoContainer>();
            var events = new List<IMyEventControllerBlock>();
            var lcds = new List<IMyTextPanel>();

            GridTerminalSystem.GetBlocksOfType(assemblers, x => x.CustomName.Contains("[Assembler]"));
            GridTerminalSystem.GetBlocksOfType(sorters, x => x.CustomName.Contains("[Sorter]"));
            GridTerminalSystem.GetBlocksOfType(containers, x => x.CustomName.Contains("[Cargo]"));
            GridTerminalSystem.GetBlocksOfType(events, x => x.CustomName.Contains("[Event]"));

            var blocks = new List<IMyTerminalBlock>();
            GridTerminalSystem.GetBlocks(blocks);

            var lcd = lcds.First();
            lcd.ContentType = VRage.Game.GUI.TextPanel.ContentType.TEXT_AND_IMAGE;
            lcd.WriteText("start", false);

            foreach (var block in blocks)
            {
                block.CustomName = block.CustomName.Replace("[Type]", Format("[{0}]", cmp));
            }

            foreach (var assembler in assemblers)
            {
                assembler.ClearQueue();
                assembler.AddQueueItem(MyDefinitionId.Parse(Format("MyObjectBuilder_BlueprintDefinition/{0}", cmp)), 100m);
                assembler.Repeating = true;
            }

            foreach (var sorter in sorters)
            {
                if (sorter.CustomName.Contains("[Ore]"))
                {
                    sorter.SetFilter(MyConveyorSorterMode.Whitelist, new List<MyInventoryItemFilter>() {
                        new MyInventoryItemFilter("MyObjectBuilder_Ore/(null)", true),
                        new MyInventoryItemFilter(Format("MyObjectBuilder_Component/{0}", cmp.Replace("Component", "")))
                    });
                }
                else if (sorter.CustomName.Contains("[Cargo]"))
                {
                    sorter.SetFilter(MyConveyorSorterMode.Whitelist, new List<MyInventoryItemFilter>() {
                        new MyInventoryItemFilter("MyObjectBuilder_Component/(null)", true)
                    });
                }
                else if (sorter.CustomName.Contains("[Component]"))
                {
                    sorter.SetFilter(MyConveyorSorterMode.Whitelist, new List<MyInventoryItemFilter>() {
                        new MyInventoryItemFilter("MyObjectBuilder_Component/(null)", true)
                    });
                }
            }
        }

        public string Format(string template, params string[] args)
        {
            return string.Format(template, args);
        }
    }
}