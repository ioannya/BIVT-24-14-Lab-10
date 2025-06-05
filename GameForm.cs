using System;
using System.Drawing;
using System.Windows.Forms;
using Model;

namespace MusicalTiles_
{
    public partial class GameForm : Form
    {
        private string playerName;
        private int difficulty;
        private MainMenuForm menuForm;

        private int score = 0;
        private Label scoreLabel;

        private System.Windows.Forms.Timer fallTimer;
        private Button[] lanes;
        private Button[] activeTiles;
        private IMusicalButton[] activeMusicalButtons; // бизнес-логика

        private ButtonGenerator buttonGenerator;
        private int tileSpeed = 5;
        private Random rand = new Random();

        public GameForm(string playerName, int difficulty, MainMenuForm menuForm)
        {
            InitializeComponent();

            this.playerName = playerName;
            this.difficulty = difficulty;
            this.menuForm = menuForm;

            this.Text = "Musical Tiles - Игра";
            this.ClientSize = new Size(100 * difficulty, 300);

            InitializeGame();

            fallTimer = new System.Windows.Forms.Timer();
            fallTimer.Interval = 30;
            fallTimer.Tick += FallTimer_Tick;
            fallTimer.Start();
        }

        private void InitializeGame()
        {
            scoreLabel = new Label()
            {
                Text = "Очки: 0",
                Location = new Point(10, 10),
                AutoSize = true,
                Font = new Font("Arial", 14)
            };
            this.Controls.Add(scoreLabel);

            lanes = new Button[difficulty];
            activeTiles = new Button[difficulty];
            activeMusicalButtons = new IMusicalButton[difficulty];
            buttonGenerator = new ButtonGenerator();

            int buttonWidth = 80;
            int buttonHeight = 30;
            int spacing = 10;
            int startX = 10;
            int y = this.ClientSize.Height - buttonHeight - 30;

            for (int i = 0; i < difficulty; i++)
            {
                Button btn = new Button()
                {
                    Text = "",
                    Size = new Size(buttonWidth, buttonHeight),
                    Location = new Point(startX + i * (buttonWidth + spacing), y),
                    BackColor = Color.Gray
                };
                this.Controls.Add(btn);
                lanes[i] = btn;
            }

            Button finishButton = new Button()
            {
                Text = "Закончить игру",
                Location = new Point(10, 50),
                Size = new Size(150, 30)
            };
            finishButton.Click += FinishButton_Click;
            this.Controls.Add(finishButton);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            int index = keyData switch
            {
                Keys.A => 0,
                Keys.S => 1,
                Keys.D => 2,
                Keys.F => 3,
                Keys.G => 4,
                Keys.H => 5,
                Keys.J => 6,
                Keys.K => 7,
                _ => -1
            };

            if (index >= 0 && index < difficulty)
                TryHit(index);

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void FinishButton_Click(object sender, EventArgs e)
        {
            FinishGame();
        }
    }
}
