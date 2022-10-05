namespace Assignment_Rest.Managers;
using Assignment1_UnitTests;


public class FootballPlayersManager
{
        public static int _nextId = 1;
        private static readonly List<FootballPlayer> Data = new()
        {
            new FootballPlayer {Id = _nextId++, Name = "Messi", Age = 21, ShirtNumber = 10},
            new FootballPlayer {Id = _nextId++, Name = "Ronaldo", Age = 30, ShirtNumber = 11},
            new FootballPlayer {Id = _nextId++, Name = "Skriniar", Age = 16, ShirtNumber = 7},
            new FootballPlayer {Id = _nextId++, Name = "Salah", Age = 18, ShirtNumber = 14}
        };

        public List<FootballPlayer> GetAll()
        {
            List<FootballPlayer> result = new List<FootballPlayer>(Data);
            return result;
        }

        public FootballPlayer? GetById(int id) => Data.Find(player => player.Id == id);

        public FootballPlayer Add(FootballPlayer newPlayer)
        {
            newPlayer.Validator();
            newPlayer.Id = _nextId++;
            Data.Add(newPlayer);
            return newPlayer;
        }

        public FootballPlayer? Delete(int id)
        {
            FootballPlayer? player = Data.Find(player1 => player1.Id == id);
            if (player == null) return null;
            Data.Remove(player);
            return player;
        }

        public FootballPlayer? Update(int id, FootballPlayer updates)
        {
            updates.Validator();
            FootballPlayer? player = Data.Find(player1 => player1.Id == id);
            if (player == null) return null;
            player.Name = updates.Name;
            player.Age = updates.Age;
            player.ShirtNumber = updates.ShirtNumber;
            return player;
        }
}
