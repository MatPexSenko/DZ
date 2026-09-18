using System.Runtime.CompilerServices;
using Zenject;

namespace SnakeGame
{
    public static class ZenjectExtensions
    {
        public static void Install(this DiContainer container, Installer installer)
        {
            container.Inject(installer);
            installer.InstallBindings();
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DiContainer Install(this DiContainer container, Installer installer, params object[] extraArgs)
        {
            container.Inject(installer, extraArgs);
            installer.InstallBindings();
            return container;
        }
    }
}