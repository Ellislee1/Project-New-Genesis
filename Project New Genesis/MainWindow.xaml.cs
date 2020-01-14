using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Media;
using WMPLib;


namespace Project_New_Genesis
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static String PATH = "Audio Files";
        public static String today = DateTime.Now.ToString("dd-MMM");
        public Player player;
        public bool playerShown = false;
        readonly DailyReading todaysReading = null;
        public static String[] audioFiles;
        public MainWindow()
        {
            InitializeComponent();
            string[] names = GetNames();
            lblDate.Content = "Today's Date: " + today;

            foreach (string x in names)
            {
                lstBox.Items.Add(x);
            }

            FileHandling reader = new FileHandling();

            List<DailyReading> readings = reader.GetFile("Readings.txt");
            foreach (DailyReading reading in readings)
            {
                if (reading.Date.Equals(today))
                {
                    todaysReading = reading;
                    break;
                }
            }

            bool flag = lstReadings.IsInitialized;
            while (!flag)
            {
                flag = lstReadings.IsInitialized;
            }

            DefaultReadings();

        }

        private static string[] GetNames()
        {
            string[] temp = { "Temp" };
            return temp;
        }

        private void LstBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                string[] file = { lstBox.SelectedItem.ToString()+".mp3" };
                Console.WriteLine(file[0]);

                if (playerShown)
                {
                    player.Close();
                    playerShown = false;
                }

                player = new Player(file, today);
                player.Show();
                playerShown = true;
            } catch (Exception) { }
            
        }

        private void BtnPlayAll_Click(object sender, RoutedEventArgs e)
        {
         
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { 
            try
            {
                lstBox.Items.Clear();
                int book = cboBooks.SelectedIndex;
                List<String> books;

                switch (book)
                {
                    case 0:
                        books = GetBooks("Gen");
                        AddToList(books);
                        break;

                    case 1:
                        books = GetBooks("Matt");
                        AddToList(books);
                        break;

                    default:
                        List<String> defaultFiles = audioFiles.ToList();
                        AddToList(defaultFiles);
                        break;

                }
            }
            catch (Exception) { }
            

        }

        private static List<String> GetBooks(string prefix)
        {
            List<String> bookChapters = new List<String>();
            foreach (string chapter in audioFiles)
            {

                if (chapter.StartsWith(prefix))
                {
                    bookChapters.Add(chapter);
                }
                else if (bookChapters.Count > 0)
                {
                    break;
                }

            }
            Console.WriteLine("Number of Books " + bookChapters.Count);
            return bookChapters;
        }

        private void AddToList(List<String> chapters)
        {
            foreach (string x in chapters)
            {
                lstBox.Items.Add(x);
            }
        }

        private void CboReadingSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = cboReadingSelector.SelectedIndex;

            switch(index)
            {
                case 1:
                    AddToReadingList(todaysReading.Reading1);
                    break;
                case 2:
                    AddToReadingList(todaysReading.Reading2);
                    break;
                case 3:
                    AddToReadingList(todaysReading.Reading3);
                    break;
                default:
                    try
                    {
                        DefaultReadings();
                    }
                    catch (Exception) { }
                    
                    break;

            }
        }

        private void AddToReadingList(List<String> chapters)
        {
            lstReadings.Items.Clear();
            foreach (string chapter in chapters)
            {
                lstReadings.Items.Add(chapter);
            }
        }

        private void DefaultReadings()
        {
            lstReadings.Items.Clear();
            lstReadings.Items.Add("Readings 1");
            foreach (string chapter in todaysReading.Reading1)
            {
                lstReadings.Items.Add(chapter);
            }

            lstReadings.Items.Add("Readings 2");
            foreach (string chapter in todaysReading.Reading2)
            {
                lstReadings.Items.Add(chapter);
            }

            lstReadings.Items.Add("Readings 3");
            foreach (string chapter in todaysReading.Reading3)
            {
                lstReadings.Items.Add(chapter);
            }
        }

        private void LstReadings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!lstReadings.SelectedItem.ToString().StartsWith("Readings") & !lstReadings.SelectedItem.ToString().Equals(null)) {
                string[] file = { lstReadings.SelectedItem.ToString() + ".mp3" };
                Console.WriteLine(file[0]);

                if (playerShown)
                {
                    player.Close();
                    playerShown = false;
                }

                player = new Player(file, today);
                player.Show();
                playerShown = true;
            }
        }
    }   
}
