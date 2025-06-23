namespace SteamGameViewerApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        // Controls
        private System.Windows.Forms.TextBox txtVanityUrl;
        private System.Windows.Forms.Button profileInfo;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ListBox lstPlayedGames;
        private System.Windows.Forms.ListBox lstPileOfShame;
        private System.Windows.Forms.ListBox lstFriends;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblFriendsList;
        private System.Windows.Forms.Label totalCost;
        private System.Windows.Forms.Label steamid;
        private System.Windows.Forms.Label steamid3;
        private System.Windows.Forms.Label steamid64dec;
        private System.Windows.Forms.Label steamidhex;
        private System.Windows.Forms.Label profiles;
        private System.Windows.Forms.Label profileCreated;
        private System.Windows.Forms.PictureBox profilePicture;
        private System.Windows.Forms.LinkLabel profileLink;
        private System.Windows.Forms.Label totalGames;


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtVanityUrl = new System.Windows.Forms.TextBox();
            this.profileInfo = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.lstPlayedGames = new System.Windows.Forms.ListBox();
            this.lstPileOfShame = new System.Windows.Forms.ListBox();
            this.lstFriends = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblFriendsList = new System.Windows.Forms.Label();
            this.totalCost = new System.Windows.Forms.Label();
            this.steamid = new System.Windows.Forms.Label();
            this.steamid3 = new System.Windows.Forms.Label();
            this.steamid64dec = new System.Windows.Forms.Label();
            this.steamidhex = new System.Windows.Forms.Label();
            this.profiles = new System.Windows.Forms.Label();
            this.profileCreated = new System.Windows.Forms.Label();
            this.profilePicture = new System.Windows.Forms.PictureBox();
            this.profileLink = new System.Windows.Forms.LinkLabel();
            this.steamid32 = new System.Windows.Forms.Label();

            // totalGames
            this.totalGames = new System.Windows.Forms.Label();
            this.totalGames.AutoSize = true;
            this.totalGames.Location = new System.Drawing.Point(17, 890);
            this.totalGames.Name = "totalGames";
            this.totalGames.Size = new System.Drawing.Size(120, 13);
            this.totalGames.TabIndex = 22;
            this.totalGames.Text = "Total Games: 0";
            this.Controls.Add(this.totalGames);

            ((System.ComponentModel.ISupportInitialize)(this.profilePicture)).BeginInit();
            this.SuspendLayout();
            // 
            // txtVanityUrl
            // 
            this.txtVanityUrl.Location = new System.Drawing.Point(116, 20);
            this.txtVanityUrl.Name = "txtVanityUrl";
            this.txtVanityUrl.Size = new System.Drawing.Size(200, 20);
            this.txtVanityUrl.TabIndex = 0;
            // 
            // profileInfo
            // 
            this.profileInfo.Location = new System.Drawing.Point(326, 18);
            this.profileInfo.Name = "profileInfo";
            this.profileInfo.Size = new System.Drawing.Size(100, 23);
            this.profileInfo.TabIndex = 1;
            this.profileInfo.Text = "Profile Info";
            this.profileInfo.UseVisualStyleBackColor = true;
            this.profileInfo.Click += new System.EventHandler(this.BtnFetch_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(432, 17);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // lstPlayedGames
            // 
            this.lstPlayedGames.Location = new System.Drawing.Point(20, 268);
            this.lstPlayedGames.Name = "lstPlayedGames";
            this.lstPlayedGames.Size = new System.Drawing.Size(345, 589);
            this.lstPlayedGames.TabIndex = 3;
            // 
            // lstPileOfShame
            // 
            this.lstPileOfShame.Location = new System.Drawing.Point(371, 268);
            this.lstPileOfShame.Name = "lstPileOfShame";
            this.lstPileOfShame.Size = new System.Drawing.Size(216, 589);
            this.lstPileOfShame.TabIndex = 4;
            this.lstPileOfShame.SelectedIndexChanged += new System.EventHandler(this.lstPileOfShame_SelectedIndexChanged);
            // 
            // lstFriends
            // 
            this.lstFriends.Location = new System.Drawing.Point(593, 268);
            this.lstFriends.Name = "lstFriends";
            this.lstFriends.Size = new System.Drawing.Size(216, 589);
            this.lstFriends.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Vanity Username:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 252);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Played Games:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(371, 252);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Pile of Shame:";
            // 
            // lblFriendsList
            // 
            this.lblFriendsList.AutoSize = true;
            this.lblFriendsList.Location = new System.Drawing.Point(590, 252);
            this.lblFriendsList.Name = "lblFriendsList";
            this.lblFriendsList.Size = new System.Drawing.Size(63, 13);
            this.lblFriendsList.TabIndex = 9;
            this.lblFriendsList.Text = "Friends List:";
            this.lblFriendsList.Click += new System.EventHandler(this.lblFriendsList_Click);
            // 
            // totalCost
            // 
            this.totalCost.AutoSize = true;
            this.totalCost.Location = new System.Drawing.Point(17, 874);
            this.totalCost.Name = "totalCost";
            this.totalCost.Size = new System.Drawing.Size(102, 13);
            this.totalCost.TabIndex = 10;
            this.totalCost.Text = "Total Cost: RM 0.00";
            // 
            // steamid
            // 
            this.steamid.AutoSize = true;
            this.steamid.Location = new System.Drawing.Point(213, 52);
            this.steamid.Name = "steamid";
            this.steamid.Size = new System.Drawing.Size(51, 13);
            this.steamid.TabIndex = 11;
            this.steamid.Text = "SteamID:";
            // 
            // steamid3
            // 
            this.steamid3.AutoSize = true;
            this.steamid3.Location = new System.Drawing.Point(213, 65);
            this.steamid3.Name = "steamid3";
            this.steamid3.Size = new System.Drawing.Size(57, 13);
            this.steamid3.TabIndex = 12;
            this.steamid3.Text = "SteamID3:";
            // 
            // steamid64dec
            // 
            this.steamid64dec.AutoSize = true;
            this.steamid64dec.Location = new System.Drawing.Point(213, 92);
            this.steamid64dec.Name = "steamid64dec";
            this.steamid64dec.Size = new System.Drawing.Size(92, 13);
            this.steamid64dec.TabIndex = 13;
            this.steamid64dec.Text = "SteamID64 (Dec):";
            this.steamid64dec.Click += new System.EventHandler(this.steamid64dec_Click);
            // 
            // steamidhex
            // 
            this.steamidhex.AutoSize = true;
            this.steamidhex.Location = new System.Drawing.Point(213, 105);
            this.steamidhex.Name = "steamidhex";
            this.steamidhex.Size = new System.Drawing.Size(91, 13);
            this.steamidhex.TabIndex = 14;
            this.steamidhex.Text = "SteamID64 (Hex):";
            this.steamidhex.Click += new System.EventHandler(this.steamidhex_Click);
            // 
            // profiles
            // 
            this.profiles.AutoSize = true;
            this.profiles.Location = new System.Drawing.Point(213, 118);
            this.profiles.Name = "profiles";
            this.profiles.Size = new System.Drawing.Size(44, 13);
            this.profiles.TabIndex = 16;
            this.profiles.Text = "Profiles:";
            // 
            // profileCreated
            // 
            this.profileCreated.AutoSize = true;
            this.profileCreated.Location = new System.Drawing.Point(213, 223);
            this.profileCreated.Name = "profileCreated";
            this.profileCreated.Size = new System.Drawing.Size(79, 13);
            this.profileCreated.TabIndex = 17;
            this.profileCreated.Text = "Profile Created:";
            this.profileCreated.Click += new System.EventHandler(this.profileCreated_Click);
            // 
            // profilePicture
            // 
            this.profilePicture.Location = new System.Drawing.Point(23, 52);
            this.profilePicture.Name = "profilePicture";
            this.profilePicture.Size = new System.Drawing.Size(184, 184);
            this.profilePicture.TabIndex = 18;
            this.profilePicture.TabStop = false;
            this.profilePicture.Click += new System.EventHandler(this.profilePicture_Click);
            // 
            // profileLink
            // 
            this.profileLink.AutoSize = true;
            this.profileLink.Location = new System.Drawing.Point(252, 118);
            this.profileLink.Name = "profileLink";
            this.profileLink.Size = new System.Drawing.Size(40, 13);
            this.profileLink.TabIndex = 20;
            this.profileLink.TabStop = true;
            this.profileLink.Text = "profiles";
            this.profileLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel2_Click);
            // 
            // steamid32
            // 
            this.steamid32.AutoSize = true;
            this.steamid32.Location = new System.Drawing.Point(213, 79);
            this.steamid32.Name = "steamid32";
            this.steamid32.Size = new System.Drawing.Size(63, 13);
            this.steamid32.TabIndex = 21;
            this.steamid32.Text = "SteamID32:";
            this.steamid32.Click += new System.EventHandler(this.steamid32_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(883, 962);
            this.Controls.Add(this.steamid32);
            this.Controls.Add(this.txtVanityUrl);
            this.Controls.Add(this.profileInfo);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.lstPlayedGames);
            this.Controls.Add(this.lstPileOfShame);
            this.Controls.Add(this.lstFriends);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblFriendsList);
            this.Controls.Add(this.totalCost);
            this.Controls.Add(this.steamid);
            this.Controls.Add(this.steamid3);
            this.Controls.Add(this.steamid64dec);
            this.Controls.Add(this.steamidhex);
            this.Controls.Add(this.profiles);
            this.Controls.Add(this.profileCreated);
            this.Controls.Add(this.profilePicture);
            this.Controls.Add(this.profileLink);
            this.Name = "Form1";
            this.Text = "Steam Game Viewer";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.profilePicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label steamid32;
    }
}