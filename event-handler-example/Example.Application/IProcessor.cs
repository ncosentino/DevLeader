using System;
using System.Collections.Generic;
using System.Text;

namespace Example.Application
{
    /// <summary>
    /// An interface for processing input.
    /// </summary>
    public interface IProcessor
    {
        #region Events

        /// <summary>
        /// The event that is triggered when checking if this can process the current input.
        /// </summary>
        event EventHandler<CheckProcessEventArgs> CheckProcess;

        #endregion

        #region Exposed Members

        /// <summary>
        /// Capitalizes the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The capitalized result.</returns>
        string Capitalize(string input);

        #endregion
    }
}