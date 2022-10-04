using Assignment_Rest.Managers;
using Assignment1_UnitTests;

namespace Rest_Tests;

[TestClass]
public class RestTest
{
    private readonly FootballPlayersManager _manager = new();

    [TestMethod]
    public void AddMethod()
    {
        //Arrange
        FootballPlayer player = new() { Id = FootballPlayersManager._nextId, Name = "Ben", Age = 21, ShirtNumber = 14 };

        //Act
        _manager.Add(player);

        //Assert
        Assert.AreEqual(player, _manager.GetById(player.Id));
        Assert.AreEqual(5, _manager.GetAll().Count);
    }

    [TestMethod]
    public void GetByIdMethod()
    {
        //Arrange
        FootballPlayer player = new()
            { Id = FootballPlayersManager._nextId, Name = "Messi", Age = 21, ShirtNumber = 30 };
        _manager.Add(player);

        //Act
        FootballPlayer? player1 = _manager.GetById(player.Id);

        //Assert
        Assert.AreEqual(player, player1);
    }
    
    [TestMethod]
    public void GetAllMethodTest()
    {
        List<FootballPlayer> footballPlayers = _manager.GetAll();
        _manager.Add(new FootballPlayer() { Id = FootballPlayersManager._nextId, Name = "Messi", Age = 21, ShirtNumber = 30 });
        Assert.AreNotEqual(footballPlayers.Count, _manager.GetAll().Count);
        Assert.AreEqual(footballPlayers.Count + 1, _manager.GetAll().Count);
    }

    [TestMethod]
    public void DeleteMethod()
    {
        //Arrange
        FootballPlayer player = new()
            { Id = FootballPlayersManager._nextId, Name = "Messi", Age = 21, ShirtNumber = 30 };
        _manager.Add(player);

        //Act
        _manager.Delete(player.Id);

        //Assert
        Assert.AreEqual(7, _manager.GetAll().Count);
        Assert.AreEqual(null, _manager.GetById(player.Id));
    }

    [TestMethod]
    public void UpdateMethod()
    {
        //Arrange
        FootballPlayer player = new()
            { Id = FootballPlayersManager._nextId, Name = "Messi", Age = 21, ShirtNumber = 30 };
        _manager.Add(player);
        
        //Act
        player.Name = "Ronaldo";
        _manager.Update(7, player);

        //Assert
        Assert.AreEqual(player.Name, _manager.GetById(player.Id)?.Name);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void ValidateName()
    {
        //Arrange
        FootballPlayer player = new FootballPlayer{ Id = 1, Name = "Messi", Age = 30, ShirtNumber = 10 };

        //Act
        player.Name = "M";
        _manager.Update(8, player);
        
        //Assert
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => player.ValidateName());
    }
}

