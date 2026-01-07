using DGSpaceEngineers.Modules;
using Sandbox.ModAPI.Ingame;
using VRage.Game;

namespace SpaceEngineers.Modules
{
    public class CargoTypeModule
    {
        const string blueprintDefinitionType = "BlueprintDefinition";
        const string myObjectBuilderType = "MyObjectBuilder";

        public string GetComponentName(string componentName)
        {
            return string.Join("_", myObjectBuilderType, blueprintDefinitionType, componentName);
        }

        public MyDefinitionId GetComponentId(string componentName, LoggerModule logger)
        {
            var fullComponentName = GetComponentName(componentName);
            return MyDefinitionId.Parse(fullComponentName);
        }

        public MyDefinitionId GetAnyComponentId()
        {
            var fullComponentName = string.Join("_", myObjectBuilderType, "Component");
            return MyDefinitionId.Parse(fullComponentName);
        }

        public MyDefinitionId GetAnyOreId()
        {
            var fullComponentName = string.Join("_", myObjectBuilderType, "Ore");
            return MyDefinitionId.Parse(fullComponentName);
        }

        public MyInventoryItemFilter GetFilter(Func<CargoTypeModule,MyDefinitionId> func)
        {
            var definitionId = func(this);
            return new MyInventoryItemFilter(definitionId);
        }
    }
}
