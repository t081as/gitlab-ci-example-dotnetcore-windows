namespace MyProject
{
    partial class HelloForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HelloForm));
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanelLower = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonSay = new System.Windows.Forms.Button();
            this.textBoxResult = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.labelName = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.tableLayoutPanelMain.SuspendLayout();
            this.flowLayoutPanelLower.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            resources.ApplyResources(this.tableLayoutPanelMain, "tableLayoutPanelMain");
            this.tableLayoutPanelMain.Controls.Add(this.flowLayoutPanelLower, 0, 2);
            this.tableLayoutPanelMain.Controls.Add(this.textBoxResult, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            // 
            // flowLayoutPanelLower
            // 
            resources.ApplyResources(this.flowLayoutPanelLower, "flowLayoutPanelLower");
            this.flowLayoutPanelLower.Controls.Add(this.buttonSay);
            this.flowLayoutPanelLower.Name = "flowLayoutPanelLower";
            // 
            // buttonSay
            // 
            resources.ApplyResources(this.buttonSay, "buttonSay");
            this.buttonSay.Name = "buttonSay";
            this.buttonSay.UseVisualStyleBackColor = true;
            // 
            // textBoxResult
            // 
            resources.ApplyResources(this.textBoxResult, "textBoxResult");
            this.textBoxResult.Name = "textBoxResult";
            this.textBoxResult.ReadOnly = true;
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Controls.Add(this.labelName);
            this.flowLayoutPanel1.Controls.Add(this.textBoxName);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // labelName
            // 
            resources.ApplyResources(this.labelName, "labelName");
            this.labelName.Name = "labelName";
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBoxName, "textBox1");
            this.textBoxName.Name = "textBox1";
            // 
            // HelloForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Name = "HelloForm";
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelMain.PerformLayout();
            this.flowLayoutPanelLower.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelLower;
        private System.Windows.Forms.Button buttonSay;
        private System.Windows.Forms.TextBox textBoxResult;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxName;
    }
}

