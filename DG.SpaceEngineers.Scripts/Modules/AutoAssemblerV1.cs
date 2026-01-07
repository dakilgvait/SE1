using Sandbox.ModAPI;

namespace DGSpaceEngineers.Modules
{
    public class AutoAssemblerModuleV1
    {
        public IMyAssembler Assembler { get; private set; }
        public IMyConveyorSorter SorterOre { get; private set; }
        public IMyConveyorSorter SorterCargo { get; private set; }
        public IMyConveyorSorter SorterComponent { get; private set; }
        public IMyCargoContainer Cargo { get; private set; }
        public IMyTextPanel Logger { get; private set; }

        public AutoAssemblerModuleV1(IMyGridTerminalSystem gridSystem)
        {
            Initialize(gridSystem);
        }

        protected virtual void Initialize(IMyGridTerminalSystem gridSystem)
        {
            Logger = InitializeLoggers(gridSystem).SingleOrDefault();
            Assembler = InitializeAssemblers(gridSystem).Single();
            Cargo = InitializeContainers(gridSystem).Single();
            var sorters = InitializeSorters(gridSystem);

            foreach (var sorter in sorters)
            {
                if (sorter.CustomName.Contains("[Ore]"))
                {
                    SorterOre = sorter;
                }
                else if (sorter.CustomName.Contains("[Cargo]"))
                {
                    SorterCargo = sorter;
                }
                else if (sorter.CustomName.Contains("[Component]"))
                {
                    SorterComponent = sorter;
                }
            }
        }

        protected virtual List<IMyAssembler> InitializeAssemblers(IMyGridTerminalSystem gridSystem)
        {
            var assemblers = new List<IMyAssembler>();
            gridSystem.GetBlocksOfType(assemblers, x => x.CustomName.Contains("[Assembler]"));

            return assemblers;
        }

        protected virtual List<IMyConveyorSorter> InitializeSorters(IMyGridTerminalSystem gridSystem)
        {
            var sorters = new List<IMyConveyorSorter>();
            gridSystem.GetBlocksOfType(sorters, x => x.CustomName.Contains("[Sorter]"));

            return sorters;
        }

        protected virtual List<IMyCargoContainer> InitializeContainers(IMyGridTerminalSystem gridSystem)
        {
            var containers = new List<IMyCargoContainer>();
            gridSystem.GetBlocksOfType(containers, x => x.CustomName.Contains("[Cargo]"));

            return containers;
        }

        protected virtual List<IMyTextPanel> InitializeLoggers(IMyGridTerminalSystem gridSystem)
        {
            var loggers = new List<IMyTextPanel>();
            gridSystem.GetBlocksOfType(loggers, x => x.CustomName.Contains("[Logger]"));

            return loggers;
        }
    }
}
