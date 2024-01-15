using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;
using LiveChartsStatisticsPalayer.Models;

namespace LiveChartsStatisticsPalayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int seasonId = 3;   //2016-2017
        int teamId = 9;     //Chicago Bulls
       
        public MainWindow()
        {
            InitializeComponent();
            CreateButtonPlayers();
            CreateButtonTeams();
            var countMatchup = new string[27];
            GenerateDiagramTeam(App.DB.Team.First());
            for(int i = 1; i < countMatchup.Length+1; i++)
            {
                countMatchup[i-1]=$"{i}";
            }
            Labels = countMatchup;
        }

        private void CreateButtonTeams()
        {
            var wrapPanelTeam = new WrapPanel();
            var listTeam = App.DB.PlayerStatistics.Where(x => x.Matchup.SeasonId == 3).ToList();
            var listUniqeTeam = new List<PlayerStatistics>();
            for (int i = 0; i < listTeam.Count; i++)
            {
                var team = listTeam[i];
                for (int j = 0; j < listUniqeTeam.Count; j++)
                {
                    if (team.TeamId == listUniqeTeam[j].TeamId)
                        listUniqeTeam.Remove(listUniqeTeam[j]);
                }
                listUniqeTeam.Add(team);
            }
            LVTeams.ItemsSource = listUniqeTeam;
           
        }

        private void CreateButtonPlayers()
        {
            wrapPanelButtonPlayers.Children.Clear();

            var listPlayers = App.DB.PlayerStatistics.Where(x => x.Matchup.SeasonId == seasonId && x.TeamId == teamId).ToList();
            var listUneqePlayer=new List<PlayerStatistics>();
            for (int i = 0; i < listPlayers.Count; i++)
            {
                var player = listPlayers[i];
                for(int y = 0; y < listUneqePlayer.Count; y++)
                {
                    if (player.PlayerId == listUneqePlayer[y].PlayerId)
                        listUneqePlayer.Remove(listUneqePlayer[y]);
                }
                listUneqePlayer.Add(player);
            }
           
            for(int i = 0; i<listUneqePlayer.Count; i++)
            {
                var buttonPlayerName = new Button();
                buttonPlayerName.Click += BPlayer_Click;
                buttonPlayerName.Content = listUneqePlayer[i].Player.Name;
                wrapPanelButtonPlayers.Children.Add(buttonPlayerName);
            }

            
            
        }

       
        private void GenerateDiagramTeam(Team team)
        {
            seriesCollection = new SeriesCollection
            {
            };
            var listHomeMatchups = App.DB.Matchup.Where(x => x.Team_Home == team.TeamId).ToList();
            var listAwayMatchups = App.DB.Matchup.Where(x => x.Team_Away == team.TeamId).ToList();
            var scoreHome = new ChartValues<double>();
            var scoreAway = new ChartValues<double>();

            for(int i = 0;i < listHomeMatchups.Count; i++)
            {
                var matchup= listHomeMatchups[i];
                scoreHome.Add(matchup.Team_Home_Score);
            }
            for(int i = 0;i < listAwayMatchups.Count; i++)
            {
                var matchup= listAwayMatchups[i];
                scoreAway.Add(matchup.Team_Away_Score);
            }
            seriesCollection.Add(new LineSeries
            {
                Values = scoreHome,
                Title = "Home"
            });
            seriesCollection.Add(new LineSeries
            {
                Values = scoreAway,
                Title = "Away"
            });
            TBNameDiagram.Text = "Statistic Team";
            DataContext = null;
            DataContext = this;
        }
        private void GenerateDiagramPlayer(Player player)
        {

            List<PlayerStatistics> listPlayers = App.DB.PlayerStatistics.Where(x => x.Matchup.SeasonId == seasonId 
            && x.TeamId == teamId
            && x.PlayerId==player.PlayerId).ToList();

            seriesCollection = new SeriesCollection
            {
            };
            var points = new ChartValues<double>();
            var assist = new ChartValues<double>();

            for (int i = 0;i < listPlayers.Count; i++)
            {
                points.Add(listPlayers[i].Point);
                assist.Add(listPlayers[i].Assist);
            }

            seriesCollection.Add(new LineSeries
            {
                Values = points,
                Title = "Point"
            }) ;
            seriesCollection.Add(new LineSeries
            {
                Values = assist,
                Title="Assist"
            });
            TBNameDiagram.Text = "Statistic Player";
            DataContext = null;
            DataContext = this;
        }

       

        public SeriesCollection seriesCollection { get; set; }
        public string[] Labels { get; set; }

        private void BPlayer_Click(object sender, RoutedEventArgs e)
        {
            var playerName = ((Button)sender).Content;
            var player = App.DB.Player.FirstOrDefault(x => x.Name.ToString() == playerName.ToString());
           
            GenerateDiagramPlayer(player);
           
        }
        private void ButtonTeam_Click(object sender, RoutedEventArgs e)
        {
            var nameTeam = ((Button)sender).Content;
            var team = App.DB.Team.FirstOrDefault(x => x.TeamName.ToString() == nameTeam.ToString());
            teamId = team.TeamId;
            CreateButtonPlayers();
            GenerateDiagramTeam(team);

            
        }
    }
}
