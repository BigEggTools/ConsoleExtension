namespace BigEgg.Tools.ConsoleExtension.Tests
{
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BigEgg.Tools.ConsoleExtension.Parameters;

    [TestClass]
    public class TestClassBase
    {
        protected TestClassBase()
        {
            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(Parser).Assembly));
            Container = new CompositionContainer(catalog);
            CompositionBatch batch = new CompositionBatch();
            batch.AddExportedValue(Container);
            Container.Compose(batch);
        }

        protected CompositionContainer Container { get; private set; }


        [TestInitialize]
        public void TestInitialize()
        {
            OnTestInitialize();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            OnTestCleanup();
        }


        protected virtual void OnTestInitialize() { }

        protected virtual void OnTestCleanup() { }
    }
}
