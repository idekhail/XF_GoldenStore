using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF_GoldenStore.Model;

namespace XF_GoldenStore
{
    public partial class App : Application
    {
        static DBOperations db;

        // Create the database connection as a singleton.
        public static DBOperations DBSQLite
        {
            get
            {
                if (db == null)
                {
                    db = new DBOperations(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "GoldenDB.db3"));
                }
                return db;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage =new NavigationPage(new MainGoldenStorePage());
           
        }

        protected override void OnStart()
        {
            
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
