namespace Ships.Service;

using Ships.DB;
using Ships.Model;
using Random.Script;
public class ShipsService{

    ShipsDB DatabaseInstance = new ShipsDB();
    public ShipsService(){
        
    }
    public TableName CreateGame(string playerName, List<Point> ships){
        DatabaseInstance.ConnectToDatabase();
        try{
            TableName NewTableName = new TableName();
            NewTableName.tableName = "1";
            List<TableName> allTables = DatabaseInstance.GetAllTables();
            int playerId = 1;

            NewTableName.tableName = RandomScript.GetRandomTableName();
            
            while(allTables.Contains(NewTableName)){
                NewTableName.tableName = RandomScript.GetRandomTableName();
            }

            this.DatabaseInstance.CreateGameInfo(NewTableName.tableName);
            this.DatabaseInstance.CreateGameShipsInfo(NewTableName.tableName);
            
            this.DatabaseInstance.AddPlayer(
                NewTableName.tableName,
                playerName,
                playerId
            );

            foreach(Point ship in ships){
                DatabaseInstance.AssignShip(
                    ship,
                    playerId,
                    NewTableName.tableName
                );
            }
            
            DatabaseInstance.DisconnectDatabase();
            return NewTableName;
        }
        catch(Exception err){
            Console.WriteLine(err.ToString());
            DatabaseInstance.DisconnectDatabase();
            return new TableName();
        }
    }
    public bool ifTableExist(string tableName){
        DatabaseInstance.ConnectToDatabase();
        try{
            List<TableName> ifTableExist = DatabaseInstance.GetTable(tableName);
            if(ifTableExist.Count == 0){
                DatabaseInstance.DisconnectDatabase();
                return false;
            }

            DatabaseInstance.DisconnectDatabase();
            return true;
        }
        catch(Exception err){
            Console.WriteLine(err.ToString());
            DatabaseInstance.DisconnectDatabase();
            return false;
        }
    }
    

    public bool ifTableIsLocked(string tableName){
        DatabaseInstance.ConnectToDatabase();
        try{
            
            bool ifGameIsLocked = DatabaseInstance.ifTableIsLocked(tableName);
            DatabaseInstance.DisconnectDatabase();
            return ifGameIsLocked;
        }
        catch(Exception err){
            Console.WriteLine(err.ToString());
            DatabaseInstance.DisconnectDatabase();
            return false;
        }
    }
    public int? GetPlayerIdByName(string tableName, string playerName){
        DatabaseInstance.ConnectToDatabase();
        try{
            int[] playerIds = {1,2}; 
            int correctIndex = 0;
            
            foreach (int id in playerIds){
                List<string> ifPlayerExists = DatabaseInstance.GetPlayerById(
                    tableName,
                    playerName,
                    id
                );
                if(ifPlayerExists.Count() > 0){
                    correctIndex = id;
                }
            }


            DatabaseInstance.DisconnectDatabase();
            return correctIndex;
        }
        catch(Exception err){
            Console.WriteLine(err.ToString());
            DatabaseInstance.DisconnectDatabase();
            return null;
        }
    }

    public List<string>? GetPlayerNameById(string tableName, int playerId){
        DatabaseInstance.ConnectToDatabase();
        try{
            
            List<string> ifPlayerExists = DatabaseInstance.GetPlayerByName(
                tableName,
                playerId
            );
            
            

            DatabaseInstance.DisconnectDatabase();
            return ifPlayerExists;
        }
        catch(Exception err){
            Console.WriteLine(err.ToString());
            DatabaseInstance.DisconnectDatabase();
            return null;
        }
    }

    public List<Point>? GetPlayerAvailableShips(string tableName, int? playerId){
        DatabaseInstance.ConnectToDatabase();
        try{
            
            List<Point> listOfPoints = DatabaseInstance.GetPlayerAvailableShips(
                tableName,
                playerId
            );
            
            DatabaseInstance.DisconnectDatabase();
            return listOfPoints;
        }
        catch(Exception err){
            Console.WriteLine(err.ToString());
            DatabaseInstance.DisconnectDatabase();
            return null;
        }
    }

    public List<Point>? GetPlayerNotAvailableShips(string tableName, int? playerId){
        DatabaseInstance.ConnectToDatabase();
        try{
            
            List<Point> listOfPoints = DatabaseInstance.GetPlayerNotAvailableShips(
                tableName,
                playerId
            );
            
            DatabaseInstance.DisconnectDatabase();
            return listOfPoints;
        }
        catch(Exception err){
            Console.WriteLine(err.ToString());
            DatabaseInstance.DisconnectDatabase();
            return null;
        }
    }

    public List<Point>? GetPlayerMissedShips(string tableName, int? playerId){
        DatabaseInstance.ConnectToDatabase();
        try{
            
            List<Point> listOfPoints = DatabaseInstance.GetPlayerMissedShips(
                tableName,
                playerId
            );
            
            DatabaseInstance.DisconnectDatabase();
            return listOfPoints;
        }
        catch(Exception err){
            Console.WriteLine(err.ToString());
            DatabaseInstance.DisconnectDatabase();
            return null;
        }
    }

    public int? GetEnemyPlayer(int? playerId){
        DatabaseInstance.ConnectToDatabase();
        try{
            
            if(playerId == 1){
                DatabaseInstance.DisconnectDatabase();
                return 2;
            }
            if(playerId == 2){
                DatabaseInstance.DisconnectDatabase();
                return 1;
            }
            DatabaseInstance.DisconnectDatabase();
            return 0;
        }
        catch(Exception err){
            Console.WriteLine(err.ToString());
            DatabaseInstance.DisconnectDatabase();
            return null;
        }
    }
    public bool UpdatePlayer(string tableName, string playerName, List<Point> ships){
        DatabaseInstance.ConnectToDatabase();
        try{
            int playerId = 2;

            DatabaseInstance.UpdatePlayer(
                tableName,
                playerName, 
                playerId
            );
            
            DatabaseInstance.LockGame(
                tableName
            );

            foreach(Point ship in ships){
                DatabaseInstance.AssignShip(
                    ship,
                    2,
                    tableName
                );
            }

            DatabaseInstance.DisconnectDatabase();
            return true;
        }
        catch(Exception err){
            Console.WriteLine(err.ToString());
            DatabaseInstance.DisconnectDatabase();
            return false;
        }
    }

    public bool ? UpdatePlayerTour(string tableName, int ? playerId){
        DatabaseInstance.ConnectToDatabase();
        try{

            DatabaseInstance.UpdatePlayerTour(
                tableName,
                playerId 
            );
            

            DatabaseInstance.DisconnectDatabase();
            return true;
        }
        catch(Exception err){
            Console.WriteLine(err.ToString());
            DatabaseInstance.DisconnectDatabase();
            return null;
        }
    }

    public int ? GetPlayerTour(string tableName){
        DatabaseInstance.ConnectToDatabase();
        try{
            int playerId = DatabaseInstance.GetPlayerTour(
                tableName 
            )[0];

            DatabaseInstance.DisconnectDatabase();
            
            return playerId; 
            
        }
        catch(Exception err){
            Console.WriteLine(err.ToString());
            DatabaseInstance.DisconnectDatabase();
            return null;
        }
    }

    public int? DestroyEnemyShip(string tableName, int? enemyPlayer, Point ship){
        DatabaseInstance.ConnectToDatabase();
        try{

            int? destroyedShips = DatabaseInstance.DestroyShip(
                tableName,
                enemyPlayer,
                ship
            );

            DatabaseInstance.DisconnectDatabase();
            return destroyedShips;
        }
        catch(Exception err){
            Console.WriteLine(err.ToString());
            DatabaseInstance.DisconnectDatabase();
            return null;
        }
    }

    public bool ? AssignMissShot(string tableName, int? enemyPlayer, Point ship){
        DatabaseInstance.ConnectToDatabase();
        try{

            DatabaseInstance.AssignMissShot(
                tableName,
                enemyPlayer,
                ship
            );

            DatabaseInstance.DisconnectDatabase();
            return true;
        }
        catch(Exception err){
            Console.WriteLine(err.ToString());
            DatabaseInstance.DisconnectDatabase();
            return null;
        }
    }
}