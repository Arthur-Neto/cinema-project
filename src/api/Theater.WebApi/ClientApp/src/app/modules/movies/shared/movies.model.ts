import { ISessionDashboardModel } from '../../sessions/shared/sessions.model';

export interface IMovieModel {
    id: number;
    title: string;
    description: string;
    duration: string;
    screenName: string;
    audioName: string;
}

export interface IMovieDashboardModel {
    id: number;
    imageBase64: string;
    title: string;
    description: string;
    duration: string;
    screenType: ScreenType;
    audioType: AudioType;
    sessions: ISessionDashboardModel[];
}

export interface IMovieCreateCommand {
    title: string;
    description: string;
    duration: number;
    screenType: ScreenType;
    audioType: AudioType;
}

export interface IMovieCoverUploadCommand {
    movieId: number;
    imgFile: File;
}

export enum ScreenType {
    two_dimension = 1,
    three_dimension = 2
}

export enum AudioType {
    dubbed = 1,
    subtitled = 2,
}
