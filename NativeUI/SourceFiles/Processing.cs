// NativeUI for Grand Theft Auto IV: The Complete Edition (1.2.0.59)
// Made by ItsClonkAndre, fork by hardVatsuki
// Version 1.0

using System;
using System.Reflection;

using GTA;

namespace NativeUI {
    
    internal class Processing : Script {

        public Processing()
        {
            AppDomain.CurrentDomain.AssemblyResolve += OnCurrentDomainAssemblyResolve;

            Interval = 1;
            Tick += OnTick;
            PerFrameDrawing += OnPerFrameDrawing;
            KeyDown += OnKeyDown;
        }

        private Assembly OnCurrentDomainAssemblyResolve(object sender, ResolveEventArgs args)
        {
            string assemblyName = new AssemblyName(args.Name).Name;

            if (assemblyName == "NativeUI") {
                return Assembly.GetExecutingAssembly();
            }

            return null;
        }

        private void OnTick(object sender, EventArgs e)
        {
            Menu.ProcessController();
        }

        private void OnPerFrameDrawing(object sender, GraphicsEventArgs e)
        {
            Menu.ProcessDrawing(e);
        }

        private void OnKeyDown(object sender, GTA.KeyEventArgs e)
        {
            Menu.ProcessKeyPress(e);
        }
    }
}