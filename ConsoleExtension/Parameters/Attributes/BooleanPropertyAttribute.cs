namespace BigEgg.Tools.ConsoleExtension.Parameters
{
    using System;

    /// <summary>
    /// The attribute of adding the metadata of the parameter model's property
    /// </summary>
    /// <seealso cref="BigEgg.Tools.ConsoleExtension.Parameters.PropertyBaseAttribute" />
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class BooleanPropertyAttribute : PropertyBaseAttribute
    {
        /// <param name="longName">The name of the command.</param>
        /// <param name="shortName">The name of the command.</param>
        /// <param name="helpMessage">The help message of the command.</param>
        public BooleanPropertyAttribute(string longName, string shortName, string helpMessage)
            : base(PropertyAttributeType.Boolean, longName, shortName, helpMessage)
        { }
    }
}
