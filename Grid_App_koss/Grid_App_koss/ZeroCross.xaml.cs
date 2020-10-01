using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Markup;

namespace Layouts_app
{
    public partial class ZeroCross : ContentPage
    {
        Grid ground;
        bool turn;
        Label cT;
        Image boxs;
        Button resetButton;
        const int gridColumns = 3;
        private const bool Zero = false;
        private const bool Cross = true;
        Dictionary<Image, int> ticTac;
        private Image[,] Pos;


        public ZeroCross()

        {
            ground = StartGame();
            ticTac = new Dictionary<Image, int>();

            resetButton = new Button()
            {
                Text = "Reset Game",
                BackgroundColor = Color.FromHex("DEF7FE"),
                HorizontalOptions = LayoutOptions.StartAndExpand,
                

            };
            cT = new Label()
            {
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.EndAndExpand,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),

            };
            
           
            StackLayout buttonLayout = new StackLayout()
            {
                BackgroundColor = Color.Black,
                Children = { resetButton }
            };
            StackLayout stackLayout = new StackLayout()
            {
                BackgroundColor = Color.Black,
                Children = { ground, buttonLayout, cT }
            };

            Content = stackLayout;
            RandomTurn();
            resetButton.Clicked += ResetButton_Clicked;



        }
        

         

        private void ResetButton_Clicked(object sender, EventArgs e)
        {
            foreach (Image image in ground.Children)
            {
                image.Source = "";
            }
            RandomTurn();
            ticTac = new Dictionary<Image, int>();


        }

        private Grid StartGame()
        {
            Grid ground = new Grid()

            {
                HeightRequest = 450,
            };

            for (int i = 0; i < gridColumns; i++)
            {
                ground.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                ground.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }
            Pos = new Image[3, 3];
            for (int i = 0; i < gridColumns; i++)
            {
                for (int j = 0; j < gridColumns; j++)
                {
                    boxs = new Image
                    {
                       
                        HeightRequest = 100,
                        BackgroundColor = Color.FromHex("DEF7FE")
                    };
                    ground.Children.Add(boxs, i, j);
                    var tap = new TapGestureRecognizer();
                    Pos[i, j] = boxs;
                    boxs.GestureRecognizers.Add(tap);
                    tap.Tapped += Box_Tapped;
                }
            }
            return ground;
        }
        

        private void Box_Tapped(object sender, EventArgs e)
        {
            Image boxs = sender as Image;
            if (turn == Cross && !ticTac.ContainsKey(boxs))
            {
                ticTac[boxs] = 1;
                UpdateT(boxs);
                
            }

            else if (turn == Zero && !ticTac.ContainsKey(boxs))
            {
                ticTac[boxs] = 2;
                UpdateT(boxs);
                


            }

           

        }



        private void RandomTurn()
        {
            Random random = new Random();
            turn = Convert.ToBoolean(random.Next(2));
            cT.Text = turn? "1 Player play" : "2 Player play";
        }
        private void UpdateT(Image boxs)
        {
            if (ticTac[boxs] == 1)
            {
                boxs.Source = "zero.png";
                turn = Zero;
                cT.Text = "1 Player play";

            }

            else 
            {
                boxs.Source = "cross.png";
                turn = Cross;
                cT.Text = "2 Player play";

            }
            WhoWin();
            
        }


        private int GetWinner()
        {
            if (turn == Zero)
            {
                return 1;
            }
            return 2;
        }
        
        private int CheckForWin()
        {
            if (Pos[0, 0].Source != null &&
                Pos[1, 1].Source != null &&
                Pos[2, 2].Source != null &&
                ticTac[Pos[0, 0]] == ticTac[Pos[1, 1]] &&
                ticTac[Pos[1, 1]] == ticTac[Pos[2, 2]])
            {
                return GetWinner();
            }
            if (Pos[0, 2].Source != null && Pos[1, 1].Source != null && Pos[2, 0].Source != null &&
                ticTac[Pos[0, 2]] == ticTac[Pos[1, 1]] &&
                ticTac[Pos[1, 1]] == ticTac[Pos[2, 0]])
            {
                return GetWinner();
            }

            for (int i = 0; i < 3; i++)
            {
                if (Pos[0, i].Source != null && Pos[1, i].Source != null && Pos[2, i].Source != null &&
                    ticTac[Pos[0, i]] ==ticTac[Pos[1, i]] &&
                    ticTac[Pos[1, i]] == ticTac[Pos[2, i]])
                {
                    return GetWinner();
                }
                if (Pos[i, 0].Source != null && Pos[i, 1].Source != null && Pos[i, 2].Source != null &&
                    ticTac[Pos[i, 0]] == ticTac[Pos[i, 1]] &&
                    ticTac[Pos[i, 1]] == ticTac[Pos[i, 0]])
                {
                    return GetWinner();
                }
            }

            return 0;
        }

        private async void WhoWin()
        {
            int win = CheckForWin();
            if (win == 1)
            {
                await DisplayAlert("Winner", "1 Player won.", "OK");
            }
            else if (win == 2)
            {
                await DisplayAlert("Winner", "2 Player won.", "OK");
            }
            else
            {
                await DisplayAlert("Winner", "Draw.", "OK");
            }
        }

    }
}
