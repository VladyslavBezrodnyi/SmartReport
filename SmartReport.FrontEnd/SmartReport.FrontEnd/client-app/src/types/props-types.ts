import { AuthState } from './state-types';
import { LoginDTO, RegistrationDTO, UserDTO, TaskDTO, UserTaskDTO, ReportDTO, VisitDateDTO } from './DTO-types';
import { AppUser } from "./common-types";

export interface HeaderProps {
  history: any;
  auth?: {
    isAuthenticated: boolean;
    user?: AppUser;
  }
  login(loginDTO: LoginDTO): void;
  logout(): void;
}

export interface HomeProps {
  auth?: AuthState;
}

export interface HomeProps {
}

export interface AppProps {
  locale: any,
  messages: any,
  auth: AuthState,
  loadDefaultLocale(): void
}

export interface SwitcherProps {
  currentLocale: string,
  locales: any,
  loadLocales(loale: string):void
}

export interface AccountCreationProps {
  register(registerDTO: RegistrationDTO):void;
  getUsers(): void;
}

export interface UsersListProps{
  usersList: UserDTO[];
  tasksList: TaskDTO[];
  getTasksList(): void;
  getUsers(): void;
  assignTask(userTaskDTO: UserTaskDTO): void;
}

export interface TaskCreationProps {
  createTask(taskDTO: TaskDTO):void;
}

export interface TasksListProps{
  tasksList: TaskDTO[];
  getTasksList(): void;
}

export interface UserTasksListProps{
  userTasksList: TaskDTO[];
  getUserTasksList(): void;
  createReport(reportDTO: ReportDTO): any;
}