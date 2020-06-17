import { AppUser } from "./common-types";

export interface AuthState {
    isAuthenticated: boolean;
    user?:AppUser | null;
}