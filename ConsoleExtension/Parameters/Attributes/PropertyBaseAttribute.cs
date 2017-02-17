namespace BigEgg.Tools.ConsoleExtension.Parameters
{
    using System;

    /// <summary>
    /// The abstract base attribute for parameter model's property
    /// </summary>
    /// <seealso cref="System.Attribute" />
    public abstract class PropertyBaseAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyBaseAttribute" /> class.
        /// </summary>
        /// <param name="propertyAttributeType">The type of property attribute.</param>
        /// <param name="longName">The name of the command.</param>
        /// <param name="shortName">The name of the command.</param>
        /// <param name="helpMessage">The help message of the command.</param>
        internal PropertyBaseAttribute(PropertyAttributeType propertyAttributeType, string longName, string shortName, string helpMessage)
        {
            PropertyAttributeType = propertyAttributeType;
            LongName = longName;
            ShortName = shortName;
            HelpMessage = helpMessage;
        }

        internal PropertyAttributeType PropertyAttributeType { get; set; }

        /// <summary>
        /// Gets the name of the command.
        /// </summary>
        /// <value>
        /// The name of the command.
        /// </value>
        public string LongName { get; private set; }

        /// <summary>
        /// Gets the name of the command.
        /// </summary>
        /// <value>
        /// The name of the command.
        /// </value>
        public string ShortName { get; private set; }

        /// <summary>
        /// Gets or sets the help message.
        /// </summary>
        /// <value>
        /// The help message.
        /// </value>
        /// <exception cref="System.ArgumentException">Throw if set help message to null, empty or whitespace</exception>
        public string HelpMessage { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether this parameter is required.
        /// </summary>
        /// <value>
        ///   <c>true</c> if required; otherwise, <c>false</c>.
        /// </value>
        public bool Required { get; set; }
    }
}
