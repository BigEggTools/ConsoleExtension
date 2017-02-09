using BigEgg.Tools.ConsoleExtension.Parameters.Logicals;
using BigEgg.Tools.ConsoleExtension.Tests.Parameters.FakeParameters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigEgg.Tools.ConsoleExtension.Tests.Parameters.Logicals
{
    [TestClass]
    public class ProcessorEngineTest : TestClassBase
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var engine = Container.GetExportedValue<IProcessorEngine>();
            Assert.IsNotNull(engine);
        }

        [TestClass]
        public class MockProcessorTest : TestClassBase
        {
            private readonly CompositionContainer mockContainer;
            private Mock<IProcessor> mockProcessor1 = new Mock<IProcessor>();
            private Mock<IProcessor> mockProcessor2 = new Mock<IProcessor>();

            public MockProcessorTest()
            {
                AggregateCatalog catalog = new AggregateCatalog();
                catalog.Catalogs.Add(new TypeCatalog(
                    typeof(ProcessorEngine)
                ));
                mockContainer = new CompositionContainer(catalog);
                mockContainer.ComposeExportedValue(mockProcessor1.Object);
                mockContainer.ComposeExportedValue(mockProcessor2.Object);

                CompositionBatch batch = new CompositionBatch();
                batch.AddExportedValue(mockContainer);
                mockContainer.Compose(batch);
            }

            protected override void OnTestCleanup()
            {
                mockProcessor1.Reset();
                mockProcessor2.Reset();
            }

            [TestMethod]
            public void CanProcess()
            {
                var engine = mockContainer.GetExportedValue<IProcessorEngine>();
                mockProcessor1.SetupGet(p => p.ProcessorType).Returns(ProcessorType.Help);
                mockProcessor1.Setup(p => p.CanProcess(It.IsAny<ProcessorContext>())).Returns(true);

                engine.Handle(new List<string>() { "clone", "--repository", "url" }, new Type[] { typeof(GitClone) }, false);

                mockProcessor1.Verify(p => p.Process(It.IsAny<ProcessorContext>()), Times.Once);
            }

            [TestMethod]
            public void CannotProcess()
            {
                var engine = mockContainer.GetExportedValue<IProcessorEngine>();
                mockProcessor1.SetupGet(p => p.ProcessorType).Returns(ProcessorType.Help);
                mockProcessor1.Setup(p => p.CanProcess(It.IsAny<ProcessorContext>())).Returns(false);

                engine.Handle(new List<string>() { "clone", "--repository", "url" }, new Type[] { typeof(GitClone) }, false);

                mockProcessor1.Verify(p => p.Process(It.IsAny<ProcessorContext>()), Times.Never);
            }

            [TestMethod]
            public void ProcessSequence()
            {
                var processor1Executed = false;
                var engine = mockContainer.GetExportedValue<IProcessorEngine>();
                mockProcessor1.SetupGet(p => p.ProcessorType).Returns(ProcessorType.Help);
                mockProcessor1.Setup(p => p.CanProcess(It.IsAny<ProcessorContext>())).Callback(() => processor1Executed = true).Returns(true);
                mockProcessor2.SetupGet(p => p.ProcessorType).Returns(ProcessorType.CommandHelp);
                mockProcessor2.Setup(p => p.CanProcess(It.IsAny<ProcessorContext>())).Callback(() => Assert.IsTrue(processor1Executed)).Returns(true);

                engine.Handle(new List<string>() { "clone", "--repository", "url" }, new Type[] { typeof(GitClone) }, false);

                mockProcessor1.Verify(p => p.Process(It.IsAny<ProcessorContext>()), Times.Once);
            }
        }
    }
}
