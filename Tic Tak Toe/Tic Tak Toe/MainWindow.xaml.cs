using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tic_Tak_Toe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Members


        #endregion

        private Marktype[] mResults;

        private bool mPlayer1Turn;


        private bool mGameEnded;

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
            NewGame();
        }
        #endregion

        private void NewGame()
        {
            mResults = new Marktype[9];

            for (int i = 0; i < mResults.Length; i++)
            {
                mResults[i] = Marktype.Free;
            }

            mPlayer1Turn = true;

            Container.Children.Cast<Button>().ToList().ForEach(Button =>
            {
                Button.Content = string.Empty;
                Button.Background = Brushes.White;
                Button.Foreground = Brushes.Blue;
            });

            mGameEnded = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (mGameEnded)
            {
                NewGame();
                return;
            }

            var button = (Button)sender;

            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);

            if (mResults[index] != Marktype.Free)
                return;

            mResults[index] = mPlayer1Turn ? Marktype.Cross : Marktype.Nought;

            button.Content = mPlayer1Turn ? "X" : "O";

            if (!mPlayer1Turn)
            {
                button.Foreground = Brushes.Red;
            }

            //toggle the player turn
            mPlayer1Turn ^= true;

            CheckForWinner();
        }

        /// <summary>
        /// check if there is a winner
        /// </summary>
        private void CheckForWinner()
        {
            #region horizontal wins
            //horizontal wins
            // -Row 0
            if (mResults[0] != Marktype.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                mGameEnded = true;

                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.LightGreen;
            }
            // -Row 1
            if (mResults[3] != Marktype.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                mGameEnded = true;

                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.LightGreen;
            }
            // -Row 2
            if (mResults[6] != Marktype.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                mGameEnded = true;

                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.LightGreen;
            }
            #endregion
            #region Vertical wins
            //Vertical wins
            // -Column 0
            if (mResults[0] != Marktype.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                mGameEnded = true;

                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.LightGreen;
            }

            // -Column 1
            if (mResults[1] != Marktype.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                mGameEnded = true;

                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.LightGreen;
            }

            // -Column 2
            if (mResults[2] != Marktype.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                mGameEnded = true;

                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.LightGreen;
            }
            #endregion
            #region diagonal wins
            //diagonal wins
            // top Left buttom Right
            if (mResults[0] != Marktype.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                mGameEnded = true;

                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.LightGreen;
            }

            // top Right buttom Left
            if (mResults[2] != Marktype.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                mGameEnded = true;

                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.LightGreen;
            }
            #endregion
            #region No wins
            //check game end and no one win
            if (!mResults.Any(i => i == Marktype.Free))
            {
                mGameEnded = true;

                Container.Children.Cast<Button>().ToList().ForEach(Button =>
                {
                    Button.Background = Brushes.Coral;
                });
            }
            #endregion
        }
    }
}
