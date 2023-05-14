using MathGame.Maui.Models;
using SQLite;

namespace MathGame.Maui.Data
{
    public class GameRepository
    {
        private readonly string _dbPath;
        private SQLiteConnection _conn;

        public GameRepository(string dbPath)
        {
            _dbPath = dbPath;
        }
        public void CreateTable()
        {
            try
            {
                _conn = new SQLiteConnection(_dbPath);
                _conn.CreateTable<Game>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void Init()
        {
            _conn = new SQLiteConnection(_dbPath);
            _conn.CreateTable<Game>();
        }

        public List<Game> GetAllGames()
        {
            Init();
            return _conn.Table<Game>().ToList();
        }

        public void Add(Game game)
        {
            _conn = new SQLiteConnection(_dbPath);
            _conn.Insert(game);
        }

        public void Delete(int id)
        {
            _conn = new SQLiteConnection(_dbPath);
            _conn.Delete(new Game { Id = id });
        }
    }
}
