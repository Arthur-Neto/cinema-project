export interface ISessionModel {
    id: number;
    date: Date;
    movieId: number;
    movieTitle: string;
    roomId: number;
    roomName: number;
}

export interface ISessionCreateCommand {
    name: string;
    numberOfChairs: number;
}

export interface ISessionUpdateCommand {
    id: number;
    name: string;
    numberOfChairs: number;
}
