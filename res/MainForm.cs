using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JackWin2
{
    public partial class Form : System.Windows.Forms.Form
    {
        //private GameSet game = new GameSet();
        DataTable gameData;
        DataTable betData;

        public Form()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {//loadfile
            gameData = GameIO.LoadDataFromFile(gameData);
            GridGameView.DataSource = gameData;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {//savefile
            if (GameIO.SaveDataToFile(gameData))
                MessageBox.Show("Данные успешно сохранены!", "Success!");
            else MessageBox.Show("Произошла ошибка!", "Error!");
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {//Добавляем данные из сети
            gameData = GameIO.MergeDataFromNet(gameData);
            GridGameView.DataSource = gameData;
        }

        private void toolStripButton5_Click_1(object sender, EventArgs e)
        {//Сохраняем ставки
            if (GameIO.SaveBetsToFile((DataTable)GridBetView.DataSource))
                MessageBox.Show("Данные успешно сохранены!", "Success!");
            else MessageBox.Show("Произошла ошибка!", "Error!");
        }

        private void toolStripButton4_Click_1(object sender, EventArgs e)
        {
            //Загружаем ставки
            betData = GameIO.LoadBetsFromFile();
            GridBetView.DataSource = betData;
        }
    }
}
