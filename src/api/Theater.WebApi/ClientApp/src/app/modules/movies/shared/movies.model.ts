export interface IMovieModel {
    id: number;
    imagePath: string;
    description: string;
    duration: string;
    screenName: string;
    screenType: ScreenType;
    audioName: string;
    audioType: AudioType;
}

export interface IMovieCreateCommand {
    name: string;
    numberOfChairs: number;
}

export interface IMovieUpdateCommand {
    id: number;
    name: string;
    numberOfChairs: number;
}

export enum ScreenType {
    two_dimension = 1,
    three_dimension = 2
}

export enum AudioType {
    dubbed = 1,
    dubtitled = 2,
}
