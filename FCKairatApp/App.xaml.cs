
using SQLite;

namespace FCKairatApp
{
    public partial class App : Application
    {
        private readonly SqlConnectionBase _connectionBase;
        public App(SqlConnectionBase connectionBase)
        {
            InitializeComponent();

            MainPage = new AppShell();

            _connectionBase = connectionBase;
        }
        protected override async void OnStart()
        {
            ISQLiteAsyncConnection database = _connectionBase.CreateConnection();
            await database.CreateTablesAsync<UserDto, PlayerDto, TeamDto, GameDto, NewsDto>();

            base.OnStart();
        }
    }
}
