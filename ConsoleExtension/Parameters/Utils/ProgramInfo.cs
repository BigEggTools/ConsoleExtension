namespace BigEgg.Tools.ConsoleExtension.Parameters.Utils
{
    using System;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Reflection;

    [Export(typeof(IProgramInfo))]
    internal class ProgramInfo : IProgramInfo
    {
        public ProgramInfo()
        {
            Title = GetAssembly().GetName().Name;
            Version = GetAssembly().GetName().Version.ToString();
            Copyright = GetAssemblyAttribute<AssemblyCopyrightAttribute>().Copyright;
            Product = GetAssemblyAttribute<AssemblyProductAttribute>().Product;
        }


        public string Title { get; private set; }

        public string Version { get; private set; }

        public string Copyright { get; private set; }

        public string Product { get; private set; }


        private TAttribute GetAssemblyAttribute<TAttribute>() where TAttribute : Attribute
        {
            return GetAssembly().GetCustomAttributes<TAttribute>().FirstOrDefault();
        }

        private Assembly GetAssembly()
        {
            return Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
        }
    }
}
