namespace PokemonGoBot
{
    partial class Settings
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
            this.authType = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.evolve = new System.Windows.Forms.CheckBox();
            this.cpThreshold = new System.Windows.Forms.NumericUpDown();
            this.trasnferType = new System.Windows.Forms.ComboBox();
            this.recycleInterval = new System.Windows.Forms.NumericUpDown();
            this.levelInterval = new System.Windows.Forms.NumericUpDown();
            this.levelOutput = new System.Windows.Forms.ComboBox();
            this.lon = new System.Windows.Forms.NumericUpDown();
            this.lat = new System.Windows.Forms.NumericUpDown();
            this.pass = new System.Windows.Forms.TextBox();
            this.user = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.cpThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.recycleInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.levelInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lat)).BeginInit();
            this.SuspendLayout();
            // 
            // authType
            // 
            this.authType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.authType.Enabled = false;
            this.authType.FormattingEnabled = true;
            this.authType.Items.AddRange(new object[] {
            "Google",
            "Ptc"});
            this.authType.Location = new System.Drawing.Point(146, 9);
            this.authType.Name = "authType";
            this.authType.Size = new System.Drawing.Size(100, 21);
            this.authType.TabIndex = 52;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(52, 13);
            this.label10.TabIndex = 51;
            this.label10.Text = "Auth type";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(146, 272);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 23);
            this.button1.TabIndex = 50;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // evolve
            // 
            this.evolve.AutoSize = true;
            this.evolve.Location = new System.Drawing.Point(14, 272);
            this.evolve.Name = "evolve";
            this.evolve.Size = new System.Drawing.Size(65, 17);
            this.evolve.TabIndex = 49;
            this.evolve.Text = "Evolve?";
            this.evolve.UseVisualStyleBackColor = true;
            // 
            // cpThreshold
            // 
            this.cpThreshold.Enabled = false;
            this.cpThreshold.Location = new System.Drawing.Point(146, 244);
            this.cpThreshold.Name = "cpThreshold";
            this.cpThreshold.Size = new System.Drawing.Size(100, 20);
            this.cpThreshold.TabIndex = 48;
            // 
            // trasnferType
            // 
            this.trasnferType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.trasnferType.FormattingEnabled = true;
            this.trasnferType.Items.AddRange(new object[] {
            "none",
            "leaveStrongest",
            "all",
            "duplicate",
            "cp"});
            this.trasnferType.Location = new System.Drawing.Point(146, 217);
            this.trasnferType.Name = "trasnferType";
            this.trasnferType.Size = new System.Drawing.Size(100, 21);
            this.trasnferType.TabIndex = 47;
            // 
            // recycleInterval
            // 
            this.recycleInterval.Location = new System.Drawing.Point(146, 192);
            this.recycleInterval.Name = "recycleInterval";
            this.recycleInterval.Size = new System.Drawing.Size(100, 20);
            this.recycleInterval.TabIndex = 46;
            this.recycleInterval.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // levelInterval
            // 
            this.levelInterval.Enabled = false;
            this.levelInterval.Location = new System.Drawing.Point(146, 167);
            this.levelInterval.Name = "levelInterval";
            this.levelInterval.Size = new System.Drawing.Size(100, 20);
            this.levelInterval.TabIndex = 45;
            this.levelInterval.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // levelOutput
            // 
            this.levelOutput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.levelOutput.FormattingEnabled = true;
            this.levelOutput.Items.AddRange(new object[] {
            "levelup",
            "time"});
            this.levelOutput.Location = new System.Drawing.Point(146, 139);
            this.levelOutput.Name = "levelOutput";
            this.levelOutput.Size = new System.Drawing.Size(100, 21);
            this.levelOutput.TabIndex = 44;
            // 
            // lon
            // 
            this.lon.DecimalPlaces = 7;
            this.lon.Location = new System.Drawing.Point(146, 114);
            this.lon.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.lon.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this.lon.Name = "lon";
            this.lon.Size = new System.Drawing.Size(100, 20);
            this.lon.TabIndex = 43;
            this.lon.Value = new decimal(new int[] {
            1224074109,
            0,
            0,
            -2147024896});
            // 
            // lat
            // 
            this.lat.DecimalPlaces = 7;
            this.lat.Location = new System.Drawing.Point(146, 88);
            this.lat.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.lat.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this.lat.Name = "lat";
            this.lat.Size = new System.Drawing.Size(100, 20);
            this.lat.TabIndex = 42;
            this.lat.Value = new decimal(new int[] {
            377879152,
            0,
            0,
            458752});
            // 
            // pass
            // 
            this.pass.Enabled = false;
            this.pass.Location = new System.Drawing.Point(146, 62);
            this.pass.Name = "pass";
            this.pass.Size = new System.Drawing.Size(100, 20);
            this.pass.TabIndex = 41;
            this.pass.Text = "fghfgh";
            this.pass.UseSystemPasswordChar = true;
            // 
            // user
            // 
            this.user.Enabled = false;
            this.user.Location = new System.Drawing.Point(146, 36);
            this.user.Name = "user";
            this.user.Size = new System.Drawing.Size(100, 20);
            this.user.TabIndex = 40;
            this.user.Text = "fghfgh";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 247);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 13);
            this.label9.TabIndex = 39;
            this.label9.Text = "CP Threshold";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 221);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(73, 13);
            this.label8.TabIndex = 38;
            this.label8.Text = "Transfer Type";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 195);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(112, 13);
            this.label7.TabIndex = 37;
            this.label7.Text = "Recycle Items Interval";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 169);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 13);
            this.label6.TabIndex = 36;
            this.label6.Text = "Level Time Interval";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 143);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 35;
            this.label5.Text = "Level Output";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 34;
            this.label4.Text = "Longitude";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 33;
            this.label3.Text = "Latitude";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 32;
            this.label2.Text = "Password";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "Username";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(260, 312);
            this.Controls.Add(this.authType);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.evolve);
            this.Controls.Add(this.cpThreshold);
            this.Controls.Add(this.trasnferType);
            this.Controls.Add(this.recycleInterval);
            this.Controls.Add(this.levelInterval);
            this.Controls.Add(this.levelOutput);
            this.Controls.Add(this.lon);
            this.Controls.Add(this.lat);
            this.Controls.Add(this.pass);
            this.Controls.Add(this.user);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Settings";
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.cpThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.recycleInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.levelInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lat)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox authType;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox evolve;
        private System.Windows.Forms.NumericUpDown cpThreshold;
        private System.Windows.Forms.ComboBox trasnferType;
        private System.Windows.Forms.NumericUpDown recycleInterval;
        private System.Windows.Forms.NumericUpDown levelInterval;
        private System.Windows.Forms.ComboBox levelOutput;
        private System.Windows.Forms.NumericUpDown lon;
        private System.Windows.Forms.NumericUpDown lat;
        private System.Windows.Forms.TextBox pass;
        private System.Windows.Forms.TextBox user;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}