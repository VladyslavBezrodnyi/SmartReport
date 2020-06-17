import { LoginDTO } from './account-types';
import { AppUser } from "./common-types";

export interface HeaderProps {
  auth?: {
    isAuthenticated: boolean;
    user?: AppUser;
  }
  login(loginDTO: LoginDTO): void;
  logout(): void;
  registr(): void;
}

export interface HomeProps {
}

export interface AppProps {
  locale: any,
  messages: any,
  loadDefaultLocale(): void
}

export interface SwitcherProps {
  currentLocale: string,
  locales: any,
  loadLocales(loale: string):void
}
