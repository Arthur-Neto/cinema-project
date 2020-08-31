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
    startTimes: Date[];
    roomID: number;
    roomName: string;
}
