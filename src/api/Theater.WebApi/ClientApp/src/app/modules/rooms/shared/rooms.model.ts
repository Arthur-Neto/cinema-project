import { ISessionDashboardModel } from '../../sessions/shared/sessions.model';

export interface IRoomsModel {
    id: number;
    name: string;
    numberOfChairs: number;
}

export interface IRoomCreateCommand {
    name: string;
    numberOfChairs: number;
}

export interface IRoomUpdateCommand {
    id: number;
    name: string;
    numberOfChairs: number;
}

export interface IAvailableRoomsCommand {
    date: Date;
    movieDuration: string;
}

export interface IRoomDashboardModel {
    id: number;
    name: string;
    numberOfChairs: number;
    sessions: ISessionDashboardModel[];
}
