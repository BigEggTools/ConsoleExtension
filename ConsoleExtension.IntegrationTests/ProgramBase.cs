namespace BigEgg.Tools.ConsoleExtension.IntegrationTests
{
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.Reflection;

    using BigEgg.Tools.ConsoleExtension.Parameters;

    public class ProgramBase
    {
        private static AggregateCatalog catalog;
        protected static CompositionContainer container;

        public static void Initialize()
        {
            catalog = new AggregateCatalog();
            // Add the Framework assembly to the catalog
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(Parser).Assembly));
            // Add the Bugger.Presentation assembly to the catalog
            catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));

            container = new CompositionContainer(catalog);
            CompositionBatch batch = new CompositionBatch();
            batch.AddExportedValue(container);
            container.Compose(batch);
        }
    }
}
