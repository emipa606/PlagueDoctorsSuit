using System.Linq;
using System.Reflection;
using Verse;

namespace GraphicApparelDetour;

[StaticConstructorOnStartup]
internal static class InjectorThingy
{
    static InjectorThingy()
    {
        LongEventHandler.QueueLongEvent(InjectStuff, "Initializing", true, null);
    }

    private static Assembly Assembly => Assembly.GetAssembly(typeof(InjectorThingy));

    private static string AssemblyName => Assembly.FullName.Split(',').First();

    public static void InjectStuff()
    {
        return;
        var method = typeof(ApparelGraphicRecordGetter).GetMethod("TryGetGraphicApparel",
            BindingFlags.Static | BindingFlags.Public);
        var method2 = typeof(ApparelGraphicRecordGetter).GetMethod("TryGetGraphicApparel",
            BindingFlags.Static | BindingFlags.NonPublic);
        if (!Detours.TryDetourFromTo(method, method2))
        {
            Log.Error($"{AssemblyName} failed to get injected properly.");
        }
    }
}