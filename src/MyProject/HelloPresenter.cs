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
namespace MyProject
{
    /// <summary>
    /// Represents a presenter managing an instance of an object implementing
    /// the <see cref="IHelloView"/> interface.
    /// </summary>
    public class HelloPresenter
    {
        /// <summary>
        /// The reference to the view.
        /// </summary>
        private IHelloView view;

        /// <summary>
        /// Initializes a new instance of the <see cref="HelloPresenter"/>,
        /// </summary>
        /// <param name="view">The reference to the view.</param>
        public HelloPresenter(IHelloView view)
        {
            this.view = view;
            this.view.SayHello += View_SayHello;
        }

        /// <summary>
        /// Handles the <see cref="IHelloView.SayHello"/> event.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">An empty <see cref="EventArgs"/>.</param>
        private void View_SayHello(object sender, EventArgs e)
        {
            this.view.HelloMessage = string.Format(HelloPresenterResources.HelloMsg, this.view.UserName);
        }
    }
}
