
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using Sandbox.ModAPI.Interfaces.Terminal;
using SpaceEngineers.Modules;
using VRage.Game;
using VRage.Game.ModAPI;

namespace DGSpaceEngineers.Modules
{
    public class AutoAssemblerV1
    {
        public AutoAssemblerV1(IMyGridTerminalSystem gridSystem)
        {
            var logger = Logger = new LoggerModule(gridSystem);
            logger.Debug(string.Format("{Name} initializing...", nameof(AutoAssemblerV1)));
            Initialize(gridSystem);
            logger.Information(string.Format("{Name} initialize finished", nameof(AutoAssemblerV1)));
        }

        public IMyAssembler Assembler { get; private set; }
        public IMyConveyorSorter SorterOre { get; private set; }
        public IMyConveyorSorter SorterCargo { get; private set; }
        public IMyConveyorSorter SorterComponent { get; private set; }
        public IMyCargoContainer Cargo { get; private set; }
        public IMyEventControllerBlock Event { get; private set; }
        public LoggerModule Logger { get; private set; }
        public CargoTypeModule CargoTypeModule { get; private set; }

        protected virtual void Initialize(IMyGridTerminalSystem gridSystem)
        {
            CargoTypeModule = new CargoTypeModule();
            Assembler = InitializeAssemblers(gridSystem).Single();
            Cargo = InitializeContainers(gridSystem).Single();
            Event = InitializeEvents(gridSystem).Single();
            var sorters = InitializeSorters(gridSystem);

            foreach (var sorter in sorters)
            {
                if (sorter.CustomName.Contains("[Ore]"))
                {
                    Logger.Debug(string.Format("Ore sorter: {Name}", sorter.CustomName));
                    SorterOre = sorter;
                }
                else if (sorter.CustomName.Contains("[Cargo]"))
                {
                    Logger.Debug(string.Format("Cargo sorter: {Name}", sorter.CustomName));
                    SorterCargo = sorter;
                }
                else if (sorter.CustomName.Contains("[Component]"))
                {
                    Logger.Debug(string.Format("Component sorter: {Name}", sorter.CustomName));
                    SorterComponent = sorter;
                }
                else
                {
                    Logger.Debug(string.Format("Unknown sorter: {Name}", sorter.CustomName));
                }
            }
        }

        protected virtual List<IMyAssembler> InitializeAssemblers(IMyGridTerminalSystem gridSystem)
        {
            var assemblers = new List<IMyAssembler>();
            gridSystem.GetBlocksOfType(assemblers, x => x.CustomName.Contains("[Assembler]"));
            Logger.Debug(string.Format("Assemblers: {Count}", assemblers.Count));

            return assemblers;
        }

        protected virtual List<IMyConveyorSorter> InitializeSorters(IMyGridTerminalSystem gridSystem)
        {
            var sorters = new List<IMyConveyorSorter>();
            gridSystem.GetBlocksOfType(sorters, x => x.CustomName.Contains("[Sorter]"));
            Logger.Debug(string.Format("Sorters: {Count}", sorters.Count));

            return sorters;
        }

        protected virtual List<IMyCargoContainer> InitializeContainers(IMyGridTerminalSystem gridSystem)
        {
            var containers = new List<IMyCargoContainer>();
            gridSystem.GetBlocksOfType(containers, x => x.CustomName.Contains("[Cargo]"));
            Logger.Debug(string.Format("Containers: {Count}", containers.Count));

            return containers;
        }

        protected virtual List<IMyEventControllerBlock> InitializeEvents(IMyGridTerminalSystem gridSystem)
        {
            var events = new List<IMyEventControllerBlock>();
            gridSystem.GetBlocksOfType(events, x => x.CustomName.Contains("[Event]"));
            Logger.Debug(string.Format("Events: {Count}", events.Count));

            return events;
        }

        public void Run()
        {
            var componentName = "ComputerComponent";
            ConfigureOreSorter(componentName);
            ConfigureCargoSorter();
            ConfigureComponentSorter();
            ConfigureAssembler(componentName);
            ConfigureEvent();
        }

        protected void ConfigureOreSorter(string componentName)
        {
            var oreFilter = CargoTypeModule.GetFilter(x => x.GetAnyOreId());
            var componentFilter = CargoTypeModule.GetFilter(x => x.GetComponentId(componentName, Logger));

            var filter = new List<MyInventoryItemFilter>()
            {
                oreFilter,
                componentFilter
            };

            SorterOre.SetFilter(MyConveyorSorterMode.Whitelist, filter);
        }

        protected void ConfigureCargoSorter()
        {
            var componentFilter = CargoTypeModule.GetFilter(x => x.GetAnyComponentId());

            var filter = new List<MyInventoryItemFilter>()
            {
                componentFilter
            };

            SorterCargo.SetFilter(MyConveyorSorterMode.Whitelist, filter);
        }

        protected void ConfigureComponentSorter()
        {
            var componentFilter = CargoTypeModule.GetFilter(x => x.GetAnyComponentId());

            var filter = new List<MyInventoryItemFilter>()
            {
                componentFilter
            };

            SorterComponent.SetFilter(MyConveyorSorterMode.Whitelist, filter);
        }

        protected void ConfigureAssembler(string componentName)
        {
            Assembler.AddQueueItem(CargoTypeModule.GetComponentId(componentName, Logger), 100m);
            Assembler.Repeating = true;
        }

        protected void ConfigureEvent()
        {
            List<ITerminalAction> action = new List<ITerminalAction>();

            Event.GetActions(action);
        }
    }
}
