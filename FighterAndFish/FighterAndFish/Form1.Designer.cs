namespace FighterAndFish
{
    partial class Form1
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
            SpaceFighterRB = new System.Windows.Forms.RadioButton();
            SpaceSceneRB = new System.Windows.Forms.RadioButton();
            PostProcessingRB = new System.Windows.Forms.RadioButton();
            groupBox1 = new System.Windows.Forms.GroupBox();
            FrequencyValueLB = new System.Windows.Forms.Label();
            AmplitudeValueLB = new System.Windows.Forms.Label();
            trackBar2 = new System.Windows.Forms.TrackBar();
            AmplitudeTB = new System.Windows.Forms.TrackBar();
            FrequencyLB = new System.Windows.Forms.Label();
            AmplitudeLB = new System.Windows.Forms.Label();
            TintBlueCB = new System.Windows.Forms.CheckBox();
            UnderWaterSceneRB = new System.Windows.Forms.RadioButton();
            BlackandWhiteRB = new System.Windows.Forms.RadioButton();
            AddSpaceFighterBT = new System.Windows.Forms.Button();
            DiffuseCB = new System.Windows.Forms.CheckBox();
            SpecularCB = new System.Windows.Forms.CheckBox();
            NormalCB = new System.Windows.Forms.CheckBox();
            WireFrameCB = new System.Windows.Forms.CheckBox();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)AmplitudeTB).BeginInit();
            SuspendLayout();
            // 
            // SpaceFighterRB
            // 
            SpaceFighterRB.AutoSize = true;
            SpaceFighterRB.Checked = true;
            SpaceFighterRB.Location = new System.Drawing.Point(32, 30);
            SpaceFighterRB.Name = "SpaceFighterRB";
            SpaceFighterRB.Size = new System.Drawing.Size(122, 19);
            SpaceFighterRB.TabIndex = 0;
            SpaceFighterRB.TabStop = true;
            SpaceFighterRB.Text = "SpaceFighterMaps";
            SpaceFighterRB.UseVisualStyleBackColor = true;
            // 
            // SpaceSceneRB
            // 
            SpaceSceneRB.AutoSize = true;
            SpaceSceneRB.Location = new System.Drawing.Point(32, 144);
            SpaceSceneRB.Name = "SpaceSceneRB";
            SpaceSceneRB.Size = new System.Drawing.Size(90, 19);
            SpaceSceneRB.TabIndex = 1;
            SpaceSceneRB.TabStop = true;
            SpaceSceneRB.Text = "Space Scene";
            SpaceSceneRB.UseVisualStyleBackColor = true;
            // 
            // PostProcessingRB
            // 
            PostProcessingRB.AutoSize = true;
            PostProcessingRB.Location = new System.Drawing.Point(32, 194);
            PostProcessingRB.Name = "PostProcessingRB";
            PostProcessingRB.Size = new System.Drawing.Size(108, 19);
            PostProcessingRB.TabIndex = 2;
            PostProcessingRB.TabStop = true;
            PostProcessingRB.Text = "Post Processing";
            PostProcessingRB.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(FrequencyValueLB);
            groupBox1.Controls.Add(AmplitudeValueLB);
            groupBox1.Controls.Add(trackBar2);
            groupBox1.Controls.Add(AmplitudeTB);
            groupBox1.Controls.Add(FrequencyLB);
            groupBox1.Controls.Add(AmplitudeLB);
            groupBox1.Controls.Add(TintBlueCB);
            groupBox1.Controls.Add(UnderWaterSceneRB);
            groupBox1.Controls.Add(BlackandWhiteRB);
            groupBox1.Location = new System.Drawing.Point(45, 232);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(420, 206);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "groupBox1";
            // 
            // FrequencyValueLB
            // 
            FrequencyValueLB.AutoSize = true;
            FrequencyValueLB.Location = new System.Drawing.Point(365, 166);
            FrequencyValueLB.Name = "FrequencyValueLB";
            FrequencyValueLB.Size = new System.Drawing.Size(38, 15);
            FrequencyValueLB.TabIndex = 8;
            FrequencyValueLB.Text = "label1";
            // 
            // AmplitudeValueLB
            // 
            AmplitudeValueLB.AutoSize = true;
            AmplitudeValueLB.Location = new System.Drawing.Point(365, 109);
            AmplitudeValueLB.Name = "AmplitudeValueLB";
            AmplitudeValueLB.Size = new System.Drawing.Size(38, 15);
            AmplitudeValueLB.TabIndex = 7;
            AmplitudeValueLB.Text = "label1";
            // 
            // trackBar2
            // 
            trackBar2.Location = new System.Drawing.Point(126, 160);
            trackBar2.Maximum = 400;
            trackBar2.Name = "trackBar2";
            trackBar2.Size = new System.Drawing.Size(223, 45);
            trackBar2.TabIndex = 6;
            // 
            // AmplitudeTB
            // 
            AmplitudeTB.Location = new System.Drawing.Point(126, 109);
            AmplitudeTB.Maximum = 100;
            AmplitudeTB.Name = "AmplitudeTB";
            AmplitudeTB.Size = new System.Drawing.Size(223, 45);
            AmplitudeTB.TabIndex = 5;
            AmplitudeTB.Value = 1;
            // 
            // FrequencyLB
            // 
            FrequencyLB.AutoSize = true;
            FrequencyLB.Location = new System.Drawing.Point(45, 166);
            FrequencyLB.Name = "FrequencyLB";
            FrequencyLB.Size = new System.Drawing.Size(62, 15);
            FrequencyLB.TabIndex = 4;
            FrequencyLB.Text = "Frequency";
            // 
            // AmplitudeLB
            // 
            AmplitudeLB.AutoSize = true;
            AmplitudeLB.Location = new System.Drawing.Point(45, 109);
            AmplitudeLB.Name = "AmplitudeLB";
            AmplitudeLB.Size = new System.Drawing.Size(63, 15);
            AmplitudeLB.TabIndex = 3;
            AmplitudeLB.Text = "Amplitude";
            // 
            // TintBlueCB
            // 
            TintBlueCB.AutoSize = true;
            TintBlueCB.Location = new System.Drawing.Point(46, 67);
            TintBlueCB.Name = "TintBlueCB";
            TintBlueCB.Size = new System.Drawing.Size(72, 19);
            TintBlueCB.TabIndex = 2;
            TintBlueCB.Text = "Tint Blue";
            TintBlueCB.UseVisualStyleBackColor = true;
            // 
            // UnderWaterSceneRB
            // 
            UnderWaterSceneRB.AutoSize = true;
            UnderWaterSceneRB.Location = new System.Drawing.Point(15, 47);
            UnderWaterSceneRB.Name = "UnderWaterSceneRB";
            UnderWaterSceneRB.Size = new System.Drawing.Size(125, 19);
            UnderWaterSceneRB.TabIndex = 1;
            UnderWaterSceneRB.TabStop = true;
            UnderWaterSceneRB.Text = "Under Water Scene";
            UnderWaterSceneRB.UseVisualStyleBackColor = true;
            // 
            // BlackandWhiteRB
            // 
            BlackandWhiteRB.AutoSize = true;
            BlackandWhiteRB.Location = new System.Drawing.Point(15, 22);
            BlackandWhiteRB.Name = "BlackandWhiteRB";
            BlackandWhiteRB.Size = new System.Drawing.Size(110, 19);
            BlackandWhiteRB.TabIndex = 0;
            BlackandWhiteRB.TabStop = true;
            BlackandWhiteRB.Text = "Black and White";
            BlackandWhiteRB.UseVisualStyleBackColor = true;
            // 
            // AddSpaceFighterBT
            // 
            AddSpaceFighterBT.Location = new System.Drawing.Point(113, 169);
            AddSpaceFighterBT.Name = "AddSpaceFighterBT";
            AddSpaceFighterBT.Size = new System.Drawing.Size(299, 23);
            AddSpaceFighterBT.TabIndex = 4;
            AddSpaceFighterBT.Text = "Add Space Fighter";
            AddSpaceFighterBT.UseVisualStyleBackColor = true;
            AddSpaceFighterBT.Click += AddSpaceFighterBT_Click;
            // 
            // DiffuseCB
            // 
            DiffuseCB.AutoSize = true;
            DiffuseCB.Location = new System.Drawing.Point(82, 53);
            DiffuseCB.Name = "DiffuseCB";
            DiffuseCB.Size = new System.Drawing.Size(63, 19);
            DiffuseCB.TabIndex = 5;
            DiffuseCB.Text = "Diffuse";
            DiffuseCB.UseVisualStyleBackColor = true;
            DiffuseCB.CheckedChanged += DiffuseCB_CheckedChanged;
            // 
            // SpecularCB
            // 
            SpecularCB.AutoSize = true;
            SpecularCB.Location = new System.Drawing.Point(82, 80);
            SpecularCB.Name = "SpecularCB";
            SpecularCB.Size = new System.Drawing.Size(71, 19);
            SpecularCB.TabIndex = 6;
            SpecularCB.Text = "Specular";
            SpecularCB.UseVisualStyleBackColor = true;
            SpecularCB.CheckedChanged += SpecularCB_CheckedChanged;
            // 
            // NormalCB
            // 
            NormalCB.AutoSize = true;
            NormalCB.Location = new System.Drawing.Point(82, 105);
            NormalCB.Name = "NormalCB";
            NormalCB.Size = new System.Drawing.Size(66, 19);
            NormalCB.TabIndex = 7;
            NormalCB.Text = "Normal";
            NormalCB.UseVisualStyleBackColor = true;
            NormalCB.CheckedChanged += NormalCB_CheckedChanged;
            // 
            // WireFrameCB
            // 
            WireFrameCB.AutoSize = true;
            WireFrameCB.Location = new System.Drawing.Point(82, 130);
            WireFrameCB.Name = "WireFrameCB";
            WireFrameCB.Size = new System.Drawing.Size(83, 19);
            WireFrameCB.TabIndex = 8;
            WireFrameCB.Text = "WireFrame";
            WireFrameCB.UseVisualStyleBackColor = true;
            WireFrameCB.CheckedChanged += WireFrameCB_CheckedChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(493, 450);
            Controls.Add(WireFrameCB);
            Controls.Add(NormalCB);
            Controls.Add(SpecularCB);
            Controls.Add(DiffuseCB);
            Controls.Add(AddSpaceFighterBT);
            Controls.Add(groupBox1);
            Controls.Add(PostProcessingRB);
            Controls.Add(SpaceSceneRB);
            Controls.Add(SpaceFighterRB);
            Name = "Form1";
            Text = "Form1";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar2).EndInit();
            ((System.ComponentModel.ISupportInitialize)AmplitudeTB).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.RadioButton SpaceFighterRB;
        private System.Windows.Forms.RadioButton SpaceSceneRB;
        private System.Windows.Forms.RadioButton PostProcessingRB;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton BlackandWhiteRB;
        private System.Windows.Forms.CheckBox TintBlueCB;
        private System.Windows.Forms.RadioButton UnderWaterSceneRB;
        private System.Windows.Forms.Label AmplitudeLB;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.TrackBar AmplitudeTB;
        private System.Windows.Forms.Label FrequencyLB;
        private System.Windows.Forms.Label FrequencyValueLB;
        private System.Windows.Forms.Label AmplitudeValueLB;
        private System.Windows.Forms.Button AddSpaceFighterBT;
        private System.Windows.Forms.CheckBox DiffuseCB;
        private System.Windows.Forms.CheckBox SpecularCB;
        private System.Windows.Forms.CheckBox NormalCB;
        private System.Windows.Forms.CheckBox WireFrameCB;
    }
}