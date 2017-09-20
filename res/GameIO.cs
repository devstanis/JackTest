using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JackWin2
{
    static class GameIO
    {
        static string filePathDataLoad = "arc.txt";
        static string filePathDataSave = "arc.txt";
        static string filePathBetLoad = "bets.txt";
        static string filePathBetSave = "bets.txt";

        static string webPage = "http://www.stoloto.ru/6x45/archive";

        private static string tableName="table645";

        /// <summary>
        /// Создает пустую форматированную таблицу
        /// </summary>
        /// <param name="name">Имя таблицы</param>
        /// <returns>Пустая форматированная таблица</returns>
        public static DataTable GetEmptyDataTable(string name)
        {
            DataTable dTable = new DataTable(name);

            // создаем столбцы для таблицы dTable
            DataColumn runNumColumn = new DataColumn("runNum", Type.GetType("System.Int32"));
            runNumColumn.Unique = false; // столбец будет иметь уникальное значение
            //runNumColumn.AllowDBNull = true; // не может принимать null

            DataColumn runDateColumn = new DataColumn("runDate", Type.GetType("System.String"));
            runDateColumn.AllowDBNull = false; // не может принимать null
            DataColumn numbersColumn = new DataColumn("numbers", Type.GetType("System.String"));
            numbersColumn.AllowDBNull = false; // не может принимать null
            DataColumn jackColumn = new DataColumn("jack", Type.GetType("System.String"));
            DataColumn jackWinColumn = new DataColumn("jackWin", Type.GetType("System.Int32"));

            dTable.Columns.Add(runNumColumn);
            dTable.Columns.Add(runDateColumn);
            dTable.Columns.Add(numbersColumn);
            dTable.Columns.Add(jackColumn);
            dTable.Columns.Add(jackWinColumn);

            // определяем первичный ключ таблицы dTable
            //dTable.PrimaryKey = new DataColumn[] { dTable.Columns["runNum"] };

            return dTable;
        }

        /// <summary>
        /// Получает таблицу тиражей из файла
        /// </summary>
        /// <returns>Таблица тиражей</returns>
        public static DataTable LoadDataFromFile(DataTable mainData)
        {
            string newData = File.ReadAllText(filePathDataLoad, Encoding.UTF8);
            return MergeData(mainData, GetRunsFromText(newData));
        }

        /// <summary>
        /// Получает таблицу ставок из файла
        /// </summary>
        /// <returns>Таблица ставок</returns>
        public static DataTable LoadBetsFromFile()
        {
            string newData = File.ReadAllText(filePathBetLoad, Encoding.UTF8);
            return GetRunsFromText(newData);
        }

        /// <summary>
        /// Загружает данные из сети
        /// </summary>
        /// <returns>Множество загруженных данных</returns>
        public static DataTable LoadDataFromNet()
        {
            string data = GetTextFromNet();
            return GetRunsFromText(data);
        }

        /// <summary>
        /// Добавляет к переданной таблице данные из сети
        /// </summary>
        /// <param name="mainData">основные данные</param>
        /// <returns>Обновленная таблица</returns>
        public static DataTable MergeDataFromNet(DataTable mainData)
        {

            return MergeData(mainData==null ? GetEmptyDataTable("645"):mainData, LoadDataFromNet());
        }

        /// <summary>
        /// Добавляет данные 
        /// </summary>
        /// <param name="mainData">Основная таблица, к кторой надо добаваить данные</param>
        /// <param name="newData">Новые данные</param>
        /// <returns>Результирующая таблица</returns>
        private static DataTable MergeData(DataTable mainData, DataTable newData)
        {
            if ((mainData == null && newData == null) || newData==null) return null;
            if (mainData == null) mainData = GetEmptyDataTable(tableName);
            for(int i = newData.Rows.Count-1;i>=0; i--)
            {
               DataRow[] rows = mainData.Select("runNum = " + newData.Rows[i]["runNum"]);
                if (rows.Length == 0)
                {
                    DataRow newRow = mainData.NewRow();
                    newRow.ItemArray = new object[] { newData.Rows[i]["runNum"], newData.Rows[i]["runDate"],
                        newData.Rows[i]["numbers"],newData.Rows[i]["jack"],newData.Rows[i]["jackWin"]};
                    mainData.Rows.Add(newRow);
                }
            }
            return mainData;
        }

        /// <summary>
        /// Сохраняет тиражи в файл
        /// </summary>
        /// <param name="data">Таблица данных</param>
        /// <returns>true-OK, false-ошибка</returns>
        public static bool SaveDataToFile(DataTable data)
        {
            return SaveTextToFile(GetTextFromData(data), filePathDataSave);
        }

        /// <summary>
        /// Сохраняет ставки в файл
        /// </summary>
        /// <param name="data">Таблица сттавок</param>
        /// <returns>true-OK, false-ошибка</returns>
        public static bool SaveBetsToFile(DataTable data)
        {
            return SaveTextToFile(GetTextFromData(data), filePathBetSave);
        }

        /// <summary>
        /// Преобразует множество тиражей в строку
        /// </summary>
        /// <param name="set">Множество</param>
        /// <returns>Строка с данными тиражей</returns>
        private static string GetTextFromData(DataTable data)
        {
            if (data == null) return "";
            StringBuilder text = new StringBuilder();
            foreach (DataRow dr in data.Rows)
            {
                text.Append(dr[1] +Environment.NewLine);       //1 дата
                text.Append(dr[0].ToString() + Environment.NewLine);   //2 номер тиража
                text.Append(dr[2] + Environment.NewLine);//3 числа
                text.Append(dr[3] + Environment.NewLine);     //4 джекпот
            }
            return text.ToString();
        }

        /// <summary>
        /// Сохраняет строку в файл
        /// </summary>
        /// <param name="text">Сохраняемая строка</param>
        /// <returns>true-OK, false-ошибка</returns>
        private static bool SaveTextToFile(string text, string filePath)
        {
            try
            {
                File.WriteAllText(filePath, text);
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                return false;
            }
            return true;
        }

        /// <summary>
        /// Преобразует строку в множество тиражей
        /// </summary>
        /// <param name="text"></param>
        /// <returns>Таблица данных тиражей тиражей</returns>
        private static DataTable GetRunsFromText(string text)
        {
            string[] data = text.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            //SortedList<int, Run> runList = new SortedList<int, Run>();
            DataTable dTable = GetEmptyDataTable("645");

            string runDate;
            string nums;
            int nomTir;
            string jk;

            int cnt = 0;
            while (cnt < data.Length)
            {
                bool error = false;
                runDate = data[cnt].TrimEnd();
                cnt++;

                try
                {
                    nomTir = int.Parse(data[cnt].TrimEnd());
                    cnt++;
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка дата: " + runDate.ToString());
                    MessageBox.Show("Ошибка обработки номера тиража: " + ex.Message);
                    nomTir = 0;
                    nums = "";
                    jk = "";
                    error = true;
                }
                nums = data[cnt].Replace("  ", " ").TrimEnd();
                cnt++;
                jk = data[cnt].TrimEnd();
                cnt++;

                if (error) return null;
                DataRow dr = dTable.NewRow();
                dr.ItemArray = new object[] { nomTir, runDate, nums, jk, 0 };
                dTable.Rows.Add(dr);
            }
            return dTable;
        }

        /// <summary>
        /// Загружает данные из страницы в сети
        /// </summary>
        /// <returns>Строка данных</returns>
        private static string GetTextFromNet()
        {
            //Получение статистики 6 из 45
            WebBrowser MyWeb = new WebBrowser();
            MyWeb.ScriptErrorsSuppressed = true;
            MyWeb.Navigate(webPage);
            MessageBox.Show("Подождите, пока загрузится страничка", "Пауза для загрузки");
            HtmlElementCollection colDiv = MyWeb.Document.GetElementsByTagName("div");
            StringBuilder sb = new StringBuilder();
            foreach (HtmlElement el in colDiv)
            {
                if (el.GetAttribute("classname") == "main")
                {
                    HtmlElementCollection colMain = el.Children;
                    foreach (HtmlElement elm in colMain)
                    {
                        if (elm.GetAttribute("classname") == "draw_date") sb.Append(elm.InnerText + Environment.NewLine);
                        if (elm.GetAttribute("classname") == "draw") sb.Append(elm.InnerText + Environment.NewLine);
                        if (elm.GetAttribute("classname") == "numbers")
                        {
                            HtmlElementCollection colNum = elm.Children[0].Children;
                            string line = colNum[0].InnerText;
                            sb.Append(line + Environment.NewLine);
                        }
                        if (elm.GetAttribute("classname") == "prize ") sb.Append(elm.InnerText + Environment.NewLine);
                    }
                }
            }
            return sb.ToString();
        }


    }
}
