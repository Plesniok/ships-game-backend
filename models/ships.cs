namespace Ships.Model; 


public record CreateGame 
 { 
    public string ? playerName { get; set; }
    public List<Point> ? ships { get; set; }
 }

public record AddPlayer 
 { 
   public string ? tableName { get; set; }
   public string ? playerName { get; set; }
   public List<Point> ? ships { get; set; }
   

 }

 public record Point 
 { 
   public int ? x { get; set; }
   public int ? y { get; set; }

 }

 public record TableName 
 { 
   public string ? tableName { get; set; }
 }

 public record SetPlayerTour 
 { 
    public string ? playerName { get; set; }
   public string ? tableName { get; set; }
 }
 public record Ships 
 { 
   public int ? name { get; set; }
 }

 public class PlayerName 
 { 
   public string ? name { get; set; }
 }

 public class DestroyShip 
 { 
   public string ? tableName { get; set; }

   public string ? playerName { get; set; }
   public Point ? ship { get; set; }
   
 }