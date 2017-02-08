namespace BigEgg.Tools.ConsoleExtension.Parameters
{
    using System;
    using System.Collections.Generic;

    using BigEgg.Tools.ConsoleExtension.Parameters.Logicals;
    using BigEgg.Tools.ConsoleExtension.Parameters.Output;
    using BigEgg.Tools.ConsoleExtension.Parameters.Results;
    using System.ComponentModel.Composition.Hosting;

    /// <summary>
    /// The parser to parse the console arguments
    /// </summary>
    public class Parser : IDisposable
    {
        private readonly ParserSettings settings;
        private readonly CompositionContainer container;

        private readonly IProcessorEngine engine;
        private readonly ITextBuilder textBuilder;

        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="Parser"/> class with a specific setting, <seealso cref="ParserSettings"/>.
        /// </summary>
        /// <param name="container">The composition container.</param>
        public Parser(CompositionContainer container)
            : this(container,
                   ParserSettings.Builder().WithDefault()
                                           .Build())
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Parser" /> class with a specific setting, <seealso cref="ParserSettings" />.
        /// </summary>
        /// <param name="container">The composition container.</param>
        /// <param name="settings">The parser settings.</param>
        public Parser(CompositionContainer container, ParserSettings settings)
        {
            this.container = container;
            this.settings = settings;

            engine = container.GetExportedValue<IProcessorEngine>();
            textBuilder = container.GetExportedValue<ITextBuilder>();
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="Parser"/> class.
        /// </summary>
        ~Parser()
        {
            Dispose(false);
        }


        /// <summary>
        /// Parses the console arguments to command.
        /// </summary>
        /// <param name="args">The console arguments.</param>
        /// <param name="types">The supported command types.</param>
        /// <returns>The command type</returns>
        public object Parse(IEnumerable<string> args, params Type[] types)
        {
            if (args == null) throw new ArgumentNullException("args");
            if (types == null) throw new ArgumentNullException("types");
            if (types.Length == 0) throw new ArgumentException("types");

            ParserResult result = engine.Handle(args, types, settings.CaseSensitive);

            settings.HelpWriter.Write(textBuilder.Build(result, settings.MaximumDisplayWidth));
            return result.ResultType == ParserResultType.ParseSuccess ?
                ((ParseSuccessResult)result).Value :
                null;
        }

        #region Implement IDisposable Interface
        /// <summary>
        /// Releases managed resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposed) return;

            if (disposing)
            {
                if (settings != null)
                    settings.Dispose();

                disposed = true;
            }
        }
        #endregion
    }
}
