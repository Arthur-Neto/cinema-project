export interface IRoomsModel {
    id: number;
    name: string;
    numberOfChairs: number;
}

export interface IRoomCreateCommand {
    name: string;
    numberOfRows: number;
}

export interface IRoomUpdateCommand {
    id: number;
    name: string;
    numberOfRows: number;
}
