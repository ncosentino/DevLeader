using System;
using System.Collections.Generic;
using System.Text;

namespace Example.Application
{
    /// <summary>
    /// A class that can process input.
    /// </summary>
    public class Processor : IProcessor
    {
        #region Constants

        private const char FIRST_CAPITAL_CHAR = (char)0x41; // A
        private const char LAST_CAPITAL_CHAR = (char)0x5A; // Z
        private const char FIRST_LOWER_CHAR = (char)0x61; // a
        private const char LAST_LOWER_CHAR = (char)0x7A; // z

        #endregion

        #region Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="Processor"/> class from being created.
        /// </summary>
        private Processor()
        {
        }

        #endregion

        #region Events

        /// <summary>
        /// The event that is triggered when checking if this can process the current input.
        /// </summary>
        public event EventHandler<CheckProcessEventArgs> CheckProcess;

        #endregion

        #region Exposed Members

        /// <summary>
        /// Creates an instance of an <see cref="IProcessor"/>.
        /// </summary>
        /// <returns>A new <see cref="IProcessor"/> instance.</returns>
        public static IProcessor Create()
        {
            return new Processor();
        }

        /// <summary>
        /// Capitalizes the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>
        /// The capitalized result.
        /// </returns>
        public string Capitalize(string input)
        {
            string output = string.Empty;

            foreach (var character in input)
            {
                // this is already capitalized!
                if (character >= FIRST_CAPITAL_CHAR && character <= LAST_CAPITAL_CHAR)
                {
                    output += character;
                }
                // we can easily capitalize these characters...
                else if (character >= FIRST_LOWER_CHAR && character <= LAST_LOWER_CHAR)
                {
                    output += character.ToString().ToUpperInvariant();
                }
                // uh oh... what should we do here? let's delegate it!
                else if (OnCheckProcess(character.ToString()))
                {
                    output += character;
                }
            }

            return output;
        }

        #endregion

        #region Internal Members

        /// <summary>
        /// Called when checking if we can process the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>True if we can process the input, or false if we need to skip it.</returns>
        private bool OnCheckProcess(string input)
        {
            var handler = CheckProcess;

            // if we have an event handler, we should delegate this check to 
            // someone else who can make the decision for us.
            if (handler != null)
            {
                var args = new CheckProcessEventArgs(input);
                handler.Invoke(this, args);
                return !args.Skip;
            }

            // nobody to handle this, so let's assume we're not skipping it.
            return true;
        }

        #endregion
    }
}