namespace Response.Model;
using Ships.Model;
public record SuccessBodyResponse 
    { 
        // public string ? tableName { get; set; }
    }
public record InfoResponse 
    { 
        public string ? ErrorMessage { get; set; }
        public string ? ErrorCode { get; set; }
    }

public record SuccessCreateGameResponse 
    { 
        public string ? NewGameCode { get; set; }
    }

public record SuccessGetPlayerByName 
    { 
        public int ? playerId { get; set; }
    }
public record SuccessGetPlayerShips 
    { 
        public List<Point> ? ships { get; set; }
    }

public record SuccessGetPlayerWithTour 
    { 
        public int ? playerId { get; set; }
    }
public record SuccessGetIfGameIsLocked 
    { 
        public bool ? ifGameIsLocked { get; set; }
    }