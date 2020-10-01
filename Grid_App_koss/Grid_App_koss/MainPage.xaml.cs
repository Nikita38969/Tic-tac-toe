using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Grid_App_koss
{
    public partial class MainPage : ContentPage
    {
        //BoxView box;
        BoxView[,] boxs = new BoxView[5, 5];
        int i, j;
        Image img;
        public MainPage()
        {
            /* Grid grid = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition{Height=new GridLength(1,GridUnitType.Star)},
                    new RowDefinition{Height=new GridLength(1,GridUnitType.Star)},
                    new RowDefinition{Height=new GridLength(1,GridUnitType.Star)},
                    new RowDefinition{Height=new GridLength(1,GridUnitType.Star)},
                    new RowDefinition{Height=new GridLength(1,GridUnitType.Star)}
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition{Width=new GridLength(1,GridUnitType.Star)},
                    new ColumnDefinition{Width=new GridLength(1,GridUnitType.Star)},
                    new ColumnDefinition{Width=new GridLength(1,GridUnitType.Star)},
                    new ColumnDefinition{Width=new GridLength(1,GridUnitType.Star)},
                    new ColumnDefinition{Width=new GridLength(1,GridUnitType.Star)}
                }
            }; */
            Grid grid = new Grid();
            for (int i = 0; i < 5; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }
            for (i = 0; i < 5; i++)
            {
                for (j = 0; j < 5; j++)
                {
                    //boxs[i,j] = new BoxView { Color = Color.FromRgb(200, 100, 50) };//box->array
                    img = new Image { Source = ImageSource.FromFile("nolik.png") };

                    //grid.Children.Add(boxs[i,j], i, j
                    grid.Children.Add(img, i, j);
                    var tap = new TapGestureRecognizer();
                    //tap.Tapped += Tap_Tapped;
                    //boxs[i,j].GestureRecognizers.Add(tap);
                    img.GestureRecognizers.Add(tap);
                    tap.Tapped += async (object sender, EventArgs e) =>
                    {
                        //BoxView box = sender as BoxView;
                        //if (box.Color==new Color(0,0,0))
                        //{
                        //    box.Color = Color.FromRgb(200, 100, 50);
                        //}
                        //else
                        //{
                        //    box.Color = new Color(0, 0, 0);
                        //}
                        Image img = sender as Image;
                        if (img.Source == ImageSource.FromFile("nolik.png"))
                        {
                            img.Source = ImageSource.FromFile("krestik.png");
                        }
                        else
                        {
                            img.Source = ImageSource.FromFile("nolik.png");
                        }
                    };
                }
            }
            Content = grid;
        }
        //private void Tap_Tapped(object sender, EventArgs e)
        //{
        //    BoxView box = sender as BoxView;
        //    box.Color = Color.FromRgb(0, 0, 0);
        //}
    }
}