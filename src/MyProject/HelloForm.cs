// The MIT License (MIT)
//
// Copyright © 2019-2020 Tobias Koch
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the “Software”), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Windows.Forms;

namespace MyProject
{
    /// <summary>
    /// Represents the main window of the application.
    /// </summary>
    public partial class HelloForm : Form, IHelloView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HelloForm"/> class.
        /// </summary>
        public HelloForm()
        {
            InitializeComponent();
        }

        /// <inheritdoc />
        public event EventHandler SayHello;

        /// <inheritdoc />
        public string HelloMessage
        {
            get => this.textBoxResult.Text;
            set
            {
                if (this.InvokeRequired)
                {
                    this.Invoke((Action)delegate { this.textBoxResult.Text = value; });
                }
                else
                {
                    this.textBoxResult.Text = value;
                }
            }
        }

        /// <inheritdoc />
        public string UserName
        {
            get => this.textBoxName.Text;
            set
            {
                if (this.InvokeRequired)
                {
                    this.Invoke((Action)delegate { this.textBoxName.Text = value; });
                }
                else
                {
                    this.textBoxName.Text = value;
                }
            }
        }

        /// <summary>
        /// Handles the a click of the <see cref="buttonSay"/>.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">An empty <see cref="EventArgs"/>.</param>
        private void buttonSay_Click(object sender, System.EventArgs e)
        {
            this.SayHello?.Invoke(this, EventArgs.Empty);
        }
    }
}
