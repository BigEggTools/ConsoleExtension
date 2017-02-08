namespace BigEgg.Tools.ConsoleExtension.Tests
{
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BigEgg.Tools.ConsoleExtension.Parameters.Tokens;
    using BigEgg.Tools.ConsoleExtension.Parameters.Utils;
    using ConsoleExtension.Parameters.Output;

    [TestClass]
    public class TestClassBase
    {
        protected TestClassBase()
        {
            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new TypeCatalog(
                typeof(ProgramInfo), typeof(Tokenizer),
                typeof(OutputFormat), typeof(TextBuilder)
            ));
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
