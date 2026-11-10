using System.Windows;
using System.Windows.Controls;

namespace Battle_Simulator
{
    public partial class MainWindow : Window
    {
        public int heroHP = 100;
        public int enemyHP = 100;

        public Button AttackButtonControl => (Button)FindName("AttackButton");
        public Button SpecialAttackButtonControl => (Button)FindName("SpecialAttackButton");
        public TextBlock HeroHpTextControl => (TextBlock)FindName("HeroHpText");
        public TextBlock EnemyHpTextControl => (TextBlock)FindName("EnemyHpText");
        public ProgressBar HeroHpBarControl => (ProgressBar)FindName("HeroHpBar");
        public ProgressBar EnemyHpBarControl => (ProgressBar)FindName("EnemyHpBar");
        public TextBox BattleLogControl => (TextBox)FindName("BattleLog");

        public MainWindow()
        {
            InitializeComponent();
            UpdateHealthDisplay();
           
        }

        public void AttackButton_Click(object sender, RoutedEventArgs e)
        {
            AddBattleMessage("Hero attacked and dealt 15 damage to the monster.");
            enemyHP -= 15;
            if (enemyHP < 0) enemyHP = 0;
            UpdateHealthDisplay();

            if (IsBattleOver())
            {
                return;
            }

            EnemyAttack();
            FinishRound();
        }

        public void SpecialAttackButton_Click(object sender, RoutedEventArgs e)
        {
             int specialDamage = 30;

            if (Random.Shared.Next(100) < 40)
            {
                AddBattleMessage($"Special attack dealt {specialDamage} damage to the monster!");
                enemyHP -= specialDamage;
                if (enemyHP < 0) enemyHP = 0;
            }
            else
            {
                AddBattleMessage("Special attack missed and dealt no damage.");
            }

            UpdateHealthDisplay();

            if (IsBattleOver())
            {
                return;
            }

            EnemyAttack();
            FinishRound();
        }

        public void EnemyAttack()
        {
            AddBattleMessage("Monster attacked and dealt 10 damage to the hero.");
            heroHP -= 10;
            if (heroHP < 0) heroHP = 0;
            UpdateHealthDisplay();
        }

       public bool IsBattleOver()
        {
            if (enemyHP <= 0)
            {
                AddBattleMessage("Hero won! Monster was defeated.");
                EndBattle();
                return true;
            }

            if (heroHP <= 0)
            {
                AddBattleMessage("Monster won! Hero was defeated.");
                EndBattle();
                return true;
            }

            return false;
        }

        public void FinishRound()
        {
            AddBattleMessage("Round finished.");
            IsBattleOver();
        }

       public void EndBattle()
        {
            AttackButtonControl.IsEnabled = false;
            SpecialAttackButtonControl.IsEnabled = false;
        }

        public void UpdateHealthDisplay()
        {
            HeroHpTextControl.Text = $"HP: {heroHP} / 100";
            EnemyHpTextControl.Text = $"HP: {enemyHP} / 100";
            HeroHpBarControl.Maximum = 100;
            HeroHpBarControl.Value = heroHP;
            EnemyHpBarControl.Maximum = 100;
            EnemyHpBarControl.Value = enemyHP;
        }

        public void AddBattleMessage(string message)
        {
            BattleLogControl.AppendText(message + Environment.NewLine);
            BattleLogControl.ScrollToEnd();
        }

    }
}
