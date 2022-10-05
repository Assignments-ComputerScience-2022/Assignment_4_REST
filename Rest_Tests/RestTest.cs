using Assignment_Rest.Managers;
using Assignment1_UnitTests;

namespace Rest_Tests;

[TestClass]
public class RestTest
{
    private FootballPlayersManager _manager;
    private FootballPlayer player;
    
    [TestInitialize]
    public void Setup()
    {
        _manager = new FootballPlayersManager();
    }

    [TestMethod]
    public void AddMethod()
    {
        //Arrange
         player = new FootballPlayer()
        {
            Name = "Suarez", 
            Age = 21, 
            ShirtNumber = 14
        };

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
        player = new FootballPlayer()
        {
            Name = "Messi", 
            Age = 21, 
            ShirtNumber = 30
        };
        
        //Act
        _manager.Add(player);
        FootballPlayer? player1 = _manager.GetById(player.Id);

        //Assert
        Assert.AreEqual(player, player1);
    }
    
    [TestMethod]
    public void GetAllMethodTest()
    {
        //Arrange
        List<FootballPlayer> footballPlayers = _manager.GetAll();
        player = new FootballPlayer()
        {
            Name = "Mbappe", 
            Age = 22, 
            ShirtNumber = 10
        };
        
        //Act
        _manager.Add(player);
        
        //Assert
        Assert.AreNotEqual(footballPlayers.Count, _manager.GetAll().Count);
        Assert.AreEqual(footballPlayers.Count + 1, _manager.GetAll().Count);
    }

    [TestMethod]
    public void DeleteMethod()
    {
        //Arrange
        int initialCount = _manager.GetAll().Count;
        //Act
        _manager.Delete(1);

        //Assert
        Assert.AreEqual(initialCount - 1, _manager.GetAll().Count);
        Assert.IsNull(_manager.GetById(1));
    }

    [TestMethod]
    public void UpdateMethod()
    {
        //Arrange
        player = new FootballPlayer()
        {
            Name = "Messi", 
            Age = 21, 
            ShirtNumber = 30
        };
        
        //Act
        _manager.Add(player);
        player.Name = "Ronaldo";
        _manager.Update(5, player);

        //Assert
        Assert.AreEqual(player.Name, _manager.GetById(player.Id)?.Name);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void ValidateName()
    {
        //Arrange
        player = new FootballPlayer()
        {
            Name = "Neymar", 
            Age = 30, 
            ShirtNumber = 10
        };

        //Act
        player.Name = "N";
        _manager.Update(player.Id, player);
        
        //Assert
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => player.ValidateName());
    }
}