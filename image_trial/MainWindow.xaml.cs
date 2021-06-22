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
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading;



namespace image_trial
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

     //assigning class to be called
        Class1 classy;
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //creating list to receive data
            List<AllResult> listyy = new List<AllResult>();

            //calling web API and pulling requested data sets from class1
            string path = "https://deckofcardsapi.com/api/deck/new/draw/?count=52";

            using (var client = new HttpClient())
            {
                string json = client.GetStringAsync(path).Result;
                classy = JsonConvert.DeserializeObject<Class1>(json);
            }


            
            //adding parsed data from web API to a list
            foreach (var cardinfo in classy.cards)
            {

                listyy.Add(cardinfo);
            }

            //assigning each image box a value from the deck of cards
            var selected = listyy[0];
            img.Source = new BitmapImage(new Uri(selected.image));
            var selected2 = listyy[1];
            img2.Source = new BitmapImage(new Uri(selected2.image));
            var selected3 = listyy[2];
            img3.Source = new BitmapImage(new Uri(selected3.image));
            var selected4 = listyy[3];
            img4.Source = new BitmapImage(new Uri(selected4.image));
        }


        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {



        }
        //implementing counter in order to identify the value of certain cards/ how many are cards are on the table
        int counter = 4;
        private void hit_Click(object sender, RoutedEventArgs e)
        {

            List<AllResult> listyyadded = new List<AllResult>();
            counter += 1;
            //incrementing the counter because when this button is clicked a new card is played on the table


            foreach (var cardinfo in classy.cards)
            {

                listyyadded.Add(cardinfo);
             


            }
            //for both of these conditionals if player chooses to hit values/ images will be added
            if (counter == 6)
            {
                var hit2 = listyyadded[counter-1];
                img6.Source = new BitmapImage(new Uri(hit2.image));

            }
            if (counter == 5)
            {
                var hit1 = listyyadded[counter - 1];
                img5.Source = new BitmapImage(new Uri(hit1.image));
            }
     
            
        }

        private void view_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void stay_Click(object sender, RoutedEventArgs e)
        {
            //initializing scorecard
            int userpoints = 0;
            int cpupoints = 0;
            
            List<AllResult> listyyadded = new List<AllResult>();


            foreach (var cardinfo in classy.cards)
            {

                listyyadded.Add(cardinfo);
           



            }
         //because on the API data set there is not numerical values for facecards so these are conditions to assign the face cards their numerical value
         //this is also for when CPU or player receives blackjack resulting in an automatic win of the hand
            for (int i = 0; i < counter; i++)
            {
                if ((listyyadded[0].value == "KING" && listyyadded[1].value == "ACE") || (listyyadded[0].value == "QUEEN" && listyyadded[1].value == "ACE") || (listyyadded[0].value == "JACK" && listyyadded[1].value == "ACE") || (listyyadded[1].value == "KING" && listyyadded[0].value == "ACE") || (listyyadded[1].value == "QUEEN" && listyyadded[0].value == "ACE") || (listyyadded[1].value == "JACK" && listyyadded[0].value == "ACE"))
                {
                    listy.Items.Add("You Have Blackjack!");
                    userpoints = 21;
                    
                }
                if ((listyyadded[2].value == "KING" && listyyadded[3].value == "ACE") || (listyyadded[2].value == "QUEEN" && listyyadded[3].value == "ACE") || (listyyadded[2].value == "JACK" && listyyadded[3].value == "ACE") || (listyyadded[3].value == "KING" && listyyadded[2].value == "ACE") || (listyyadded[3].value == "QUEEN" && listyyadded[2].value == "ACE") || (listyyadded[3].value == "JACK" && listyyadded[2].value == "ACE"))
                {
                    listy.Items.Add("CPU Has Blackjack!");
                    cpupoints = 21;
                    
                }
                else if (listyyadded[i].value == "KING" || listyyadded[i].value == "QUEEN" || listyyadded[i].value == "JACK")
                {
                    if (i == 0) { userpoints = userpoints + 10; }
                    if (i == 1) { userpoints = userpoints + 10; }
                    if (i == 4) { userpoints = userpoints + 10; }
                    if (i == 5) { userpoints = userpoints + 10; }
                    if (i == 2) { cpupoints = cpupoints + 10; }
                    if (i == 3) { cpupoints = cpupoints + 10; }

                  

                }
                else if (listyyadded[i].value == "ACE")
                {
                    if (i == 0) { userpoints = userpoints + 1; }
                    if (i == 1) { userpoints = userpoints + 1; }
                    if (i == 4) { userpoints = userpoints + 1; }
                    if (i == 5) { userpoints = userpoints + 1; }
                    if (i == 2) { cpupoints = cpupoints + 1; }
                    if (i == 3) { cpupoints = cpupoints + 1; }
                  

                }

                else
                {
                    //this is adding values to the list for the cards whose API does contain numerical values. No conversion is needed
                    if (i == 0) { userpoints = userpoints + Convert.ToInt32(listyyadded[i].value); }
                    if (i == 1) { userpoints = userpoints + Convert.ToInt32(listyyadded[i].value); }
                    if (i == 4) { userpoints = userpoints + Convert.ToInt32(listyyadded[i].value); }
                    if (i == 5) { userpoints = userpoints + Convert.ToInt32(listyyadded[i].value); }
                    if (i == 2) { cpupoints = cpupoints + Convert.ToInt32(listyyadded[i].value); }
                    if (i == 3) { cpupoints = cpupoints + Convert.ToInt32(listyyadded[i].value); }

                   
                }

                //possibly come back and try to get ace 1 or 11
                // maybe send message box and have them choose

            }
            
            //This is for when the dealer does not have 17 he must flip another card
            if (cpupoints < 17)
            {

                var deal = listyyadded[7];
                img7.Source = new BitmapImage(new Uri(deal.image));
                if (deal.value == "ACE")
                {
                    cpupoints = cpupoints + 1;
                }
                if (deal.value == "KING" || deal.value == "QUEEN" || deal.value == "JACK")
                {
                    cpupoints = cpupoints + 10;
                }
                else
                {
                    cpupoints = cpupoints + Convert.ToInt32(listyyadded[7].value);
                }
                
              
            }

            //adding items to the list box for the scoring system
            listy.Items.Add("User Points = " + userpoints);
            listy.Items.Add("CPU Points = " + cpupoints);
            listy.Items.Add("**********************");

          


       
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            //this button resets all imageboxes
            img.Source = null;
            img2.Source = null;
            img3.Source = null;
            img4.Source = null;
            img5.Source = null;
            img6.Source = null;
            img7.Source = null;
            
            
           
            //resetting counter
           counter = 4;
            //resetting the list of values pulled from API
             List<AllResult> listyyadded = new List<AllResult>();
            listyyadded.Clear();
            
        }

        private void points_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

      
    }
}
