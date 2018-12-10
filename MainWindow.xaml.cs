using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region PrivateMembers

        private MarkType[] Results;

        private bool Player1Turn;

        private bool GameEnded;
        #endregion


        #region Constructor
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }

        #endregion
        public void NewGame()
        {
            //creates array
            Results = new MarkType[9];

            //sets all squares to blank
            for (var i = 0; i < Results.Length; i++)
            {
                Results[i] = MarkType.Blank;
            }

            Player1Turn = true;

            //casts as a list and iterate buttons on the grid
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                //change to default values
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Black;
            });

            GameEnded = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (GameEnded)
            {
                NewGame();
                return;
            }

            //cast as sender object type button
            var button = (Button)sender;

            var col = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = col + (row * 3);

            //do nothing if empty
            if (Results[index] != MarkType.Blank)
                return;

            //cell value by turn
            Results[index] = Player1Turn ? MarkType.X : MarkType.O;

            //player 1 x; player 2 0
            button.Content = Player1Turn ? "X" : "O";

            //change cell color
            if (!Player1Turn)
            {
                button.Background = Brushes.LightGray;
                button.Foreground = Brushes.Maroon;
            }
            //toggle player turns
            Player1Turn ^= true;

            //Chcek for winner
            CheckForWinner();
        }

        private void CheckForWinner()
        {
            #region Horizontal Wins
            //results for row 0
            if (Results[0] != MarkType.Blank && ((Results[0] & Results[1] & Results[2]) == Results[0]))
            {
                GameEnded = true;
                //highlight winning sequence
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Coral;
                MessageBox.Show("Winner!");
            }

            //results for row 1
            if (Results[3] != MarkType.Blank && ((Results[3] & Results[4] & Results[5]) == Results[3]))
            {
                GameEnded = true;
                //highlight winning sequence
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Coral;
                MessageBox.Show("Winner!");
            }

            //results for row 2
            if (Results[6] != MarkType.Blank && ((Results[6] & Results[7] & Results[8]) == Results[6]))
            {
                GameEnded = true;
                //highlight winning sequence
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Coral;
                MessageBox.Show("Winner!");
            }
            #endregion

            #region Vertical Wins
            //results for col 0
            if (Results[0] != MarkType.Blank && ((Results[0] & Results[3] & Results[6]) == Results[0]))
            {
                GameEnded = true;
                //highlight winning sequence
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Coral;
                MessageBox.Show("Winner!");
            }

            //results for col 1
            if (Results[1] != MarkType.Blank && ((Results[1] & Results[4] & Results[7]) == Results[1]))
            {
                GameEnded = true;
                //highlight winning sequence
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Coral;
                MessageBox.Show("Winner!");
            }

            //results for col 2
            if (Results[2] != MarkType.Blank && ((Results[2] & Results[5] & Results[8]) == Results[2]))
            {
                GameEnded = true;
                //highlight winning sequence
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Coral;
                MessageBox.Show("Winner!");
            }
            #endregion

            #region Diagonal Wins
            //results diagonal 1
            if (Results[0] != MarkType.Blank && ((Results[0] & Results[4] & Results[8]) == Results[0]))
            {
                GameEnded = true;
                //highlight winning sequence
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Coral;
                MessageBox.Show("Winner!");
            }

            //results diagonal 2
            if (Results[2] != MarkType.Blank && ((Results[2] & Results[4] & Results[6]) == Results[2]))
            {
                GameEnded = true;
                //highlight winning sequence
                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Coral;
                MessageBox.Show("Winner!");
            }
            #endregion

            //check for no winner
            if (!Results.Any(result => result == MarkType.Blank))
            {
                GameEnded = true;

                //check for no winner and change every cell
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.PaleTurquoise;
                    button.Background = Brushes.LightSalmon;
                });
                MessageBox.Show("Tie game. Click anywhere to play again.");

            }

        }

        }
    }

