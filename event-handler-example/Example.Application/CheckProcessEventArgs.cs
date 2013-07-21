using System;
using System.Collections.Generic;
using System.Text;

namespace Example.Application
{
    /// <summary>
    /// A class that contains information about the check process event.
    /// </summary>
    public class CheckProcessEventArgs : EventArgs
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckProcessEventArgs"/> class.
        /// </summary>
        /// <param name="input">The input.</param>
        public CheckProcessEventArgs(string input)
        {
            this.Input = input;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the input.
        /// </summary>
        public string Input
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not to skip this input.
        /// </summary>
        public bool Skip
        {
            get;
            set;
        }

        #endregion
    }
}