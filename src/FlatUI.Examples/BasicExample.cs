using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlatUI.Examples
{
	public partial class BasicExample : Form
	{
		private Boolean ProgressOngoing = false;

		public BasicExample()
		{
			InitializeComponent();
		}

		private void FlatButton_Click(object sender, EventArgs e)
		{
			if (!this.ProgressOngoing)
			{
				Int32 sleepAmount = (this.ProgressCheckBox.Checked ? 30 : 10);
				this.ProgressOngoing = true;
				this.FlatProgressBar.Value = 0;
				new Thread(() =>
				{
					while (this.FlatProgressBar.Value < this.FlatProgressBar.Maximum)
					{
						Thread.Sleep(sleepAmount);
						this.Invoke((MethodInvoker)delegate
						{
							this.FlatProgressBar.Increment(1);
						});
					}

					this.ProgressOngoing = false;
				}).Start();
			}
		}
	}
}
