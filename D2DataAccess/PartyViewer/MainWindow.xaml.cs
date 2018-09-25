using D2DataAccess.Data;
using D2DataAccess.Enums;
using D2DataAccess.Models;
using D2DataAccess.Simple;
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

namespace PartyViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CharacterOverview Player1 { get; set; } = new CharacterOverview() { Light = 0 };
        
        protected const string ApiKey = "57a4ba76e897450f8b106635fd20460b";
        Destiny2Api Api = new Destiny2Api(ApiKey, new UserAgentHeader("Destiny 2 Party Viewer", "1.0.0", "Party Viewer C#", 0, "https://www.d2-partyviewer.com", "baileymiller@live.com"), @"C:\Users\Bailey Miller\Desktop\Destiny 2 Manifest\worldAssets\world.content");


        public MainWindow()
        {
            InitializeComponent();
            DataContext = Player1;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            // Search Player 1

            var profile = await Api.SearchDestinyPlayer(Player1Name.Text, BungieMembershipType.TigerBlizzard);

            if (profile.Response != null)
            {
                // Load Characters
                Player1 = await Api.GetCharacterBreakdown(profile.Response.First().membershipId, BungieMembershipType.TigerBlizzard, 2305843009299655689);
                
                
            }

        }
    }
}
