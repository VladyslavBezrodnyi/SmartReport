export interface AppUser {
    id: string;
    roles: string;
    userName: string;
    access_token: string
}

export interface Action {
    type: string;
    payload?: any;
}