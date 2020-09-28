export interface ISessionModel {
    id: number;
    date: Date;
    movieId: number;
    movieTitle: string;
    roomId: number;
    roomName: string;
}

export interface ISessionDashboardModel {
    id: number;
    date: Date;
}

export interface ISessionCreateCommand {
    date: Date;
    movieId: number;
    roomId: number;
}

export interface ICreateOccupiedChairsCommand {
    chairsNumbers: number[];
    sessionId: number;
}

export interface IOccupiedChair {
    number: number;
    sessionId: number;
}
