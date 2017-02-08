namespace BigEgg.Tools.ConsoleExtension.Parameters.Results
{
    using System;

    /// <summary>
    /// The parse success result, contains an instance with parsed value.
    /// </summary>
    /// <seealso cref="BigEgg.Tools.ConsoleExtension.Parameters.Results.ParserResult" />
    internal sealed class ParseSuccessResult : ParserResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParseSuccessResult"/> class.
        /// </summary>
        /// <param name="value">The instance with parsed value.</param>
        /// <param name="commandType">The parsed value type.</param>
        /// <exception cref="System.ArgumentNullException">The instance and command type cannot be null.</exception>
        public ParseSuccessResult(object value, Type commandType)
            : base(ParserResultType.ParseSuccess)
        {
            if (value == null) { throw new ArgumentNullException("value"); }
            if (commandType == null) { throw new ArgumentNullException("commandType"); }

            Value = value;
            CommandType = commandType;
        }


        /// <summary>
        /// Gets the instance with parsed value.
        /// </summary>
        /// <value>
        /// The instance with parsed value.
        /// </value>
        public object Value { get; private set; }

        /// <summary>
        /// Gets the parsed value type.
        /// </summary>
        /// <value>
        /// The parsed value type.
        /// </value>
        public Type CommandType { get; private set; }
    }
}
